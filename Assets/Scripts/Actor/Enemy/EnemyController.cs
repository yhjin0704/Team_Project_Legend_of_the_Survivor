using System;
using System.Collections;
using System.Collections.Generic;
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
    private float currentTime = 0f;
    private bool isHit = false;
    [SerializeField] private int coinCount;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] GameObject bullet;

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
            GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity);
            Vector2 direction = (target.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.Euler(0, 0, angle);

            obj.GetComponent<Bullet>().SetDamage(actor.atk);
            obj.GetComponent<Bullet>().SetDir(direction);
        }
        else if (enemy_Type == Enemy_Type.Boss)
        {
            int index = Random.Range(0, 4);
            index = 1;
            switch (index)
            {
                case 0:
                    NormalAttack();
                    break;
                case 1:
                    MultipleAttack();
                    break;
                case 2:
                    ArcAttack();
                    break;
                case 3:
                    AroundAttack();
                    break;
            }
        }
    }

    private void NormalAttack()
    {
        GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity);
        Vector2 direction = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.Euler(0, 0, angle);

        obj.GetComponent<Bullet>().SetDamage(actor.atk);
        obj.GetComponent<Bullet>().SetDir(direction);
    }

    private void MultipleAttack()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 vec = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            GameObject obj = Instantiate(bullet, vec, Quaternion.identity);
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.Euler(0, 0, angle);
            obj.GetComponent<Bullet>().SetDamage(actor.atk);
            obj.GetComponent<Bullet>().SetDir(direction);
        }
    }

    private void ArcAttack()
    {

    }

    private void AroundAttack()
    {

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
