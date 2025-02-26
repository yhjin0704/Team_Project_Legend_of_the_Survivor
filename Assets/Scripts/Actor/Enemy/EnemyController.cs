using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // ���� �߰�

public class EnemyController : BaseController
{
    NavMeshAgent agent;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private float attackDelay = 1f;
    private float currentTime = 0f;
    private bool isDamage = false;
    [SerializeField]
    private int coin;

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
            return;
        }

        base.Update();
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

    protected override void Attack()
    {
        base.Attack();
        isAttacking = true;
        animationHandler.Attack();

        //target.GetComponent<PlayerController>().Damage();
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
    }
}
