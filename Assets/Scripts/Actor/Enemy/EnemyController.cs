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
        if (isAttacking)
        {
            agent.velocity = Vector3.zero;
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                currentTime = 0f;
                isAttacking = false;
                animationHandler.AttackEnd();
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

        if (DistanceToTarget() <= attackRange)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        base.Attack();
        isAttacking = true;
        animationHandler.Attack();
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
            if (actor.IsAlive)
            {
                actor.hp--;

                if (actor.hp <= 0)
                {
                    actor.hp = 0;
                    animationHandler.Dead();
                    actor.IsAlive = false;
                }
                else
                {
                    animationHandler.Damage();
                }
            }
        }
    }
}
