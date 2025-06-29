using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random; // ���� �߰�

enum Enemy_Type
{
    Close,
    Far,
    Boss
}

public class EnemyController : BaseController
{
    [SerializeField] Enemy_Type enemy_Type;
    NavMeshAgent agent;
    SpriteRenderer spriteRenderer;

    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDelay = 1f;
    private float attackTime = 0f;
    private float hitTime = 0f;
    [SerializeField] private int coinCount;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] private int potionCount;
    [SerializeField] GameObject potionPrefab;

    [SerializeField] GameObject bullet;

    bool CanAttak = false;

    private int curPatternCount = 0;
    private int maxPatternCount = 50;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.RegisterEnemy(this);
        GameObject playerGB = GameObject.Find("Archer");
        target = playerGB.transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Update()
    {
        if (!actor.IsAlive)
        {
            agent.velocity = Vector3.zero;
            return;
        }

        if (isAttacking || isHit)
        {
            StopPlayer();

            return;
        }

        agent.SetDestination(target.position);
        if (agent.pathPending == false && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.velocity = Vector3.zero;
        }
        animationHandler.Move(agent.velocity);

        if (DistanceToTarget() <= attackRange && !isHit && CanAttak == true)
        {
            Attack();
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        CheakWall();
    }

    private void StopPlayer()
    {
        agent.velocity = Vector3.zero;

        if (isHit)
        {
            hitTime += Time.deltaTime;
            if (hitTime > 1)
            {
                hitTime = 0f;
                isHit = false;
                animationHandler.InvincibilityEnd();
                actor.GetComponent<Collider2D>().enabled = true;
            }
        }
        //if (isAttacking && skillManager.GetSkillList()[2].IsFinish)
        if(isAttacking)
        {
            attackTime += Time.deltaTime;
            if (attackTime > attackDelay)
            {
                attackTime = 0f;
                isAttacking = false;
                animationHandler.AttackEnd();
            }
        }
    }

    protected void CheakWall()
    {
        Vector3 dir = target.position - actor.transform.position;
        RaycastHit2D ray = Physics2D.Raycast(actor.transform.position, dir, dir.magnitude, 1 << LayerMask.NameToLayer("Wall"));
        if (ray.collider == null)
        {
            CanAttak = true;
        }
        else
        {
            CanAttak = false;
        }
    }

    protected override void Attack()
    {
        base.Attack();

        isAttacking = true;
        animationHandler.Attack();

        if (enemy_Type == Enemy_Type.Close)
            target.GetComponent<BaseController>().Hit(actor.atk);
        else if (enemy_Type == Enemy_Type.Far)
        {
            UseSkills(0);
        }
        else if (enemy_Type == Enemy_Type.Boss)
        {
            int index = Random.Range(0, 4);
            UseSkills(index);
        }
    }

    private void LateUpdate()
    {
        spriteRenderer.flipX = target.position.x < transform.position.x;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, GetTarget().position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    protected override void Dead()
    {
        base.Dead();

        actor.IsAlive = false;
        for (int i = 0; i < coinCount; i++)
        {
            float vec;
            float vec2;
            do
            {
                vec = Random.Range(-1f, 1f);
                vec2 = Random.Range(-1f, 1f);
            } 
            while (!GameManager.Instance.IsOnTilemap(transform.position + new Vector3(vec, vec2, 0)));
            Instantiate(coinPrefab, transform.position + new Vector3(vec, vec2, 0), Quaternion.identity);
        }
        for (int i = 0; i < potionCount; i++)
        {
            float vec;
            float vec2;
            do
            {
                vec = Random.Range(-1f, 1f);
                vec2 = Random.Range(-1f, 1f);
            }
            while (!GameManager.Instance.IsOnTilemap(transform.position + new Vector3(vec, vec2, 0)));
            Instantiate(potionPrefab, transform.position + new Vector3(vec, vec2, 0), Quaternion.identity);
        }
        GameManager.Instance.UnregisterEnemy(this);
        Destroy(gameObject, 2f);
    }

    public override void Hit(float _damage)
    {
        if (isHit || !actor.IsAlive)
            return;

        SoundManager.instance.PlaySfx(SoundManager.Sfx.Hit);

        isHit = true;
        actor.hp -= _damage;
        if (actor.hp <= 0)
        {
            Dead();
        }
        else
        {
            actor.GetComponent<Collider2D>().enabled = false;
            animationHandler.Damage();
        }
        gameObject.GetComponentInChildren<ActorUI>().ShowCombatValue((int)_damage, true);
        gameObject.GetComponentInChildren<ActorUI>().ChangeHPBar(actor.hp, actor.GetMaxHp());
    }

    protected override void UseSkills(int _index)
    {
        base.UseSkills();

        if (target == null)
        {
            Debug.LogError("Target이 null입니다.");
            return;
        }

        SetShotPos(target);

        skillManager.GetSkillList()[_index].Use();
    }
}
