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

        base.Update();
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
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<BaseController>().Hit(actor.atk);
        }
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
        base.Hit(_damage);

        isHit = true;
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
