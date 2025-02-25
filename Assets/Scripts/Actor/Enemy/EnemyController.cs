using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // 내가 추가

public class EnemyController : BaseController
{
    //여기부터
    NavMeshAgent agent;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.Find("Archer").transform;
    }

    protected override void Update()
    {
        base.Update();
        agent.SetDestination(GetTarget().position);
        if (agent.pathPending == false && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.velocity = Vector3.zero;
        }
    }
    //여기까지 내가 추가

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, GetTarget().position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
}
