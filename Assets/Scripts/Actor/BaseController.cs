using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    protected SkillManager skillManager;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

<<<<<<< HEAD
    //protected Vector2 lookDirection = Vector2.zero;
    //public Vector2 LookDirection { get { return lookDirection; } }
=======
    protected Vector3 lookDirection = Vector2.zero;
    public Vector3 LookDirection { get { return lookDirection; } }
>>>>>>> dev

    protected Vector2 knockback = Vector2.zero;
    protected float knockbackDuration = 0.0f;

    [SerializeField] protected bool isSkillUseActor = false;

    protected Actor actor;

    protected AnimationHandler animationHandler;

    // 공격 목표
    protected Transform target;
    public Transform GetTarget()
    {
        return target;
    }

    public float shotPosDistance;

    // 투사체 발사 위치
    protected Transform shotPos;
    public Transform GetShotPos()
    {
        return shotPos;
    }

    protected bool isHit = false;
    protected bool isAttacking;
    protected float timeSinceLastAttack = float.MaxValue;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        skillManager = GetComponent<SkillManager>();
        animationHandler = GetComponent<AnimationHandler>();
        actor = GetComponent<Actor>();

        shotPos = transform.Find("ShotPos");
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        AttackDelay();

        switch (actor.GetState())
        {
            case EState.Attack:
                if (isSkillUseActor)
                {
                    Invoke("UseSkills", 0.5f);
                }
                break;
            case EState.Dead:
                Dead();
                break;
            default:
                break;
        }

    }

    protected virtual void FixedUpdate()
    {
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.deltaTime;
        }
    }

    protected virtual void Movement(Vector2 _direction)
    {
    }

    //private void Rotate(Vector2 direction)
    //{
    //    float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    bool isLeft = Mathf.Abs(rotZ) > 90f;

    //    characterRenderer.flipX = isLeft;

    //    if (weaponPivot != null)
    //    {
    //        weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    //    }

    //    weaponHandler?.Rotate(isLeft);
    //}

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    protected void AttackDelay()
    {
        if (timeSinceLastAttack < actor.atkDelay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (timeSinceLastAttack >= actor.atkDelay)
        {
            timeSinceLastAttack = 0;
            actor.SetState(EState.Attack);
        }
    }

    protected virtual void Attack()
    {
<<<<<<< HEAD

=======
>>>>>>> dev
    }

    protected virtual void UseSkills()
    {
        if (skillManager.GetSkillList() == null)
        {
            Debug.LogError("SkillList가 null입니다.");
            return;
        }

        if (target == null)
        {
            Debug.LogError("Target이 null입니다.");
            return;
        }
    }

    protected virtual void Hit(float _damage)
    {
<<<<<<< HEAD
        actor.hp -= _damage;
=======
        if (isHit == true ||
            actor.GetState() == EState.Dead)
        {
            return;
        }
        actor.hp -= _damage;
        isHit = true;
        animationHandler.Damage();
        StartCoroutine(HitTime(0.5f));
>>>>>>> dev

        if (actor.hp <= 0)
        {
            actor.hp = 0;
            actor.SetState(EState.Dead);
        }
    }

    protected virtual void Dead()
    {
    }

    protected virtual void SetShotPos(Transform _targetPos)
    {
        if (shotPos == null)
        {
            return;
        }

        lookDirection = (_targetPos.position - transform.position).normalized;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        shotPos.rotation = Quaternion.Euler(0, 0, angle);

        shotPos.position = transform.position + lookDirection * shotPosDistance;
        shotPos.position = transform.position + lookDirection * shotPosDistance;
    }

    void OnDrawGizmos()
    {
        if (shotPos != null)
        {
            // 그려질 색상 설정
            Gizmos.color = Color.red;
            // 2D 씬에서 targetTransform의 위치에 원 그리기
            Gizmos.DrawWireSphere(shotPos.position, 0.02f);
        }
    }
<<<<<<< HEAD
=======

    IEnumerator HitTime(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        
        isHit = false;
        animationHandler.InvincibilityEnd();
    }
>>>>>>> dev
}
