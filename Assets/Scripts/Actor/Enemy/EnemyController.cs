using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    [SerializeField] private Transform target;
    [SerializeField] private float followRange = 15f;
    [SerializeField] private float attackRange = 1f;



    protected override void Movement(Vector2 direction)
    {
        direction = direction * actor.speed;
        if (knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, GetTarget().position);
    }

    protected void HandleAction()
    {
        if (target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;
        if (distance <= followRange)
        {
            lookDirection = direction;

            if (distance <= attackRange)
            {
                int layerMaskTarget = target.gameObject.layer;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }

                movementDirection = Vector2.zero;
                return;
            }

            movementDirection = direction;
        }
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

}
