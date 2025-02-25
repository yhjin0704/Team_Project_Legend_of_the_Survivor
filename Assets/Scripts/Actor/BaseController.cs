using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    protected SkillManager skillManager;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    protected Vector2 knockback = Vector2.zero;
    protected float knockbackDuration = 0.0f;

    protected Actor actor;

    protected AnimationHandler animationHandler;

    // ���� ��ǥ
    protected Transform target;
    public Transform GetTarget()
    {
        return target;
    }

    public float shotPosDistance;

    // ����ü �߻� ��ġ
    protected Transform shotPos;
    public Transform GetShotPos()
    {
        return shotPos;
    }

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
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.deltaTime;
        }
    }

    protected virtual void Movement(Vector2 _direction)
    {
        _rigidbody.velocity = _direction * actor.speed;
        int a = 0;
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

        if (isAttacking && timeSinceLastAttack >= actor.atkDelay)
        {
            timeSinceLastAttack = 0;
            UseSkills();
        }
    }

    protected virtual void Attack()
    {

    }

    protected virtual void UseSkills()
    {
        if (skillManager.GetSkillList() == null)
        {
            Debug.LogError("SkillList�� null�Դϴ�.");
            return;
        }
    }

    protected virtual void SetShotPos(Transform _targetPos)
    {
        if (shotPos == null)
        {
            return;
        }

        Vector3 direction = (_targetPos.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shotPos.rotation = Quaternion.Euler(0, 0, angle);

        shotPos.position = transform.position + direction * shotPosDistance;
        shotPos.position = transform.position + direction * shotPosDistance;
    }

    void OnDrawGizmos()
    {
        if (shotPos != null)
        {
            // �׷��� ���� ����
            Gizmos.color = Color.red;
            // 2D ������ targetTransform�� ��ġ�� �� �׸���
            Gizmos.DrawWireSphere(shotPos.position, 0.02f);
        }
    }
}
