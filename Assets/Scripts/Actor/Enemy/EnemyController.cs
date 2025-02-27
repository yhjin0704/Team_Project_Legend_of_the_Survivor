using System;
using System.Collections;
using System.Collections.Generic;
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

<<<<<<< HEAD
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private float attackDelay = 1f;
    private float currentTime = 0f;
    private bool isDamage = false;
    [SerializeField]
    private int coin;
=======
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDelay = 1f;
    private float attackTime = 0f;
    private float hitTime = 0f;
    [SerializeField] private int coinCount;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] private int potionCount;
    [SerializeField] GameObject potionPrefab;

    [SerializeField] GameObject bullet;

    private int curPatternCount = 0;
    private int maxPatternCount = 50;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
>>>>>>> dev

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
<<<<<<< HEAD
        if (isAttacking || isDamage)
        {
            if (isAttacking && isDamage)
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
                isDamage = false;
                animationHandler.AttackEnd();
                animationHandler.InvincibilityEnd();
            }
=======
        if (!actor.IsAlive)
        {
            agent.velocity = Vector3.zero;
>>>>>>> dev
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

        if (DistanceToTarget() <= attackRange && !isDamage)
        {
            Attack();
        }
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
            }
        }
        if (isAttacking)
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

    protected override void Attack()
    {
        base.Attack();
<<<<<<< HEAD
        isAttacking = true;
        animationHandler.Attack();
=======

        SoundManager.instance.PlaySfx(SoundManager.Sfx.Arrow);

        isAttacking = true;
        animationHandler.Attack();

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
            curPatternCount = 0;
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
        if (!actor.IsAlive) return;

        GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity);
        Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * curPatternCount / maxPatternCount),
            Mathf.Sin(Mathf.PI * 2 * curPatternCount / maxPatternCount)).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.Euler(0, 0, angle);

        obj.GetComponent<Bullet>().SetDamage(actor.atk);
        obj.GetComponent<Bullet>().SetDir(direction);

        curPatternCount++;

        if (curPatternCount < maxPatternCount)
            Invoke("ArcAttack", 0.15f);
    }

    private void AroundAttack()
    {
        int roundNum = 40;

        for (int i = 0; i < roundNum; i++)
        {
            GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity);
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNum),
                                            Mathf.Sin(Mathf.PI * 2 * i / roundNum)).normalized;

            Vector3 rot = (Vector3.forward * 360 * i / roundNum) + (Vector3.forward * 90);
            obj.transform.Rotate(rot);

            obj.GetComponent<Bullet>().SetDamage(actor.atk);
            obj.GetComponent<Bullet>().SetDir(direction);
        }
>>>>>>> dev
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

<<<<<<< HEAD
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            if (actor.IsAlive && !isDamage)
            {
                actor.hp--;

                if (actor.hp <= 0)
                {
                    actor.hp = 0;
                    isDamage = true;
                    animationHandler.Dead();
                    for (int i = 0; i < coin; i++)
                    {
                        // 코인 생성
                    }
                    actor.IsAlive = false;
                }
                else
                {
                    isDamage = true;
                    animationHandler.Damage();
                }

                Destroy(collision.gameObject);
            }
        }
=======
    protected override void Dead()
    {
        base.Dead();

        actor.IsAlive = false;
        for (int i = 0; i < coinCount; i++)
        {
            float vec = Random.Range(-1f, 1f);
            float vec2 = Random.Range(-1f, 1f);
            Instantiate(coinPrefab, transform.position + new Vector3(vec, vec2, 0), Quaternion.identity);
        }
        for (int i = 0; i < potionCount; i++)
        {
            float vec = Random.Range(-1f, 1f);
            float vec2 = Random.Range(-1f, 1f);
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
>>>>>>> dev
    }
}
