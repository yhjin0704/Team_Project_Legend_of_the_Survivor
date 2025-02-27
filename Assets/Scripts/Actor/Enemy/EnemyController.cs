using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // ���� �߰�

enum Enemy_Type
{
    Close,
    Far
}

public class EnemyController : BaseController
{
    [SerializeField]
    Enemy_Type enemy_Type;
    NavMeshAgent agent;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private float attackDelay = 1f;
    private float currentTime = 0f;
    private bool isHit = false;
    [SerializeField]
    private int coinCount;
    [SerializeField]
    GameObject coinPrefab;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        GameObject playerGB = GameObject.Find("Archer");
        target = playerGB.transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Update()
    {
        if (isAttacking || isHit)
        {
            if (isAttacking && isHit)
            {
                currentTime = 0f;
                isAttacking = false;
            }
            agent.velocity = Vector3.zero;

            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                currentTime = 0f;
                isAttacking = false;
                isHit = false;
                animationHandler.AttackEnd();
                animationHandler.InvincibilityEnd();
            }
            return;
        }

        agent.SetDestination(target.position);
        if (agent.pathPending == false && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.velocity = Vector3.zero;
        }
        animationHandler.Move(agent.velocity);

        if (DistanceToTarget() <= attackRange && !isHit)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        base.Attack();

        if (enemy_Type == Enemy_Type.Close)
            target.GetComponent<BaseController>().Hit(actor.atk);
        else if (enemy_Type == Enemy_Type.Far)
        {
            Instantiate();
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

        isHit = true;
        for (int i = 0; i < coinCount; i++)
        {
            float vec = Random.Range(-1f, 1f);
            Instantiate(coinPrefab, transform.position + new Vector3(vec, vec, 0), Quaternion.identity);
        }
        Destroy(gameObject, 1f);
    }

    public override void Hit(float _damage)
    {
        if (isHit || !actor.IsAlive)
            return;

        isHit = true;
        actor.hp -= _damage;
        if (actor.hp <= 0)
        {
            actor.hp = 0;
            actor.IsAlive = false;
            animationHandler.Dead();
        }
        else
        {
            animationHandler.Damage();
        }
        gameObject.GetComponentInChildren<ActorUI>().ShowCombatValue((int)_damage, true);
        gameObject.GetComponentInChildren<ActorUI>().ChangeHPBar(actor.hp, actor.GetMaxHp());
    }

    protected override void UseSkills()
    {
        base.UseSkills();

        if (target == null)
        {
            Debug.LogError("Target이 null입니다.");
            return;
        }

        SetShotPos(target);

        skillManager.GetSkillList()[0].Use();
    }
}
