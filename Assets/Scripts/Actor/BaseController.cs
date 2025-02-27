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

    protected Vector3 lookDirection = Vector2.zero;
    public Vector3 LookDirection { get { return lookDirection; } }

    protected Vector2 knockback = Vector2.zero;
    protected float knockbackDuration = 0.0f;

    [SerializeField] protected bool isSkillUseActor = false;

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
                if (isSkillUseActor && _rigidbody.velocity == Vector2.zero)
                {
                    timeSinceLastAttack = 0;
                    Attack();
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
        else
        {
            actor.SetState(EState.Attack);
        }
    }

    protected virtual void Attack()
    {
    }

    protected virtual void UseSkills()
    {
        if (skillManager.GetSkillList() == null)
        {
            Debug.Log("SkillList�� null�Դϴ�.");
            return;
        }

        if (target == null)
        {
            Debug.Log("Target�� null�Դϴ�.");
            return;
        }
    }

    public virtual void Hit(float _damage)
    {
        if (isHit == true ||
            actor.GetState() == EState.Dead)
        {
            return;
        }
        actor.hp -= _damage;
        isHit = true;
        animationHandler.Damage();
        StartCoroutine(HitTime(0.5f));

        if (actor.hp <= 0)
        {
            actor.hp = 0;
            actor.SetState(EState.Dead);
        }
        gameObject.GetComponentInChildren<ActorUI>().ShowCombatValue((int)_damage, true);
        gameObject.GetComponentInChildren<ActorUI>().ChangeHPBar(actor.hp, actor.GetMaxHp());
    }

    public virtual void Healed(float _heal)
    {
        if (actor.hp + _heal <= actor.GetMaxHp())
        {
            actor.hp += _heal;
        }
        else
        {
            actor.hp = actor.GetMaxHp();
        }

        gameObject.GetComponentInChildren<ActorUI>().ShowCombatValue((int)_heal, false);
        gameObject.GetComponentInChildren<ActorUI>().ChangeHPBar(actor.hp, actor.GetMaxHp());
    }

    protected virtual void Dead()
    {
        actor.hp = 0;
        actor.SetState(EState.Dead);
        animationHandler.Dead();
        actor.GetComponent<Collider2D>().enabled = false;
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
            // �׷��� ���� ����
            Gizmos.color = Color.red;
            // 2D ������ targetTransform�� ��ġ�� �� �׸���
            Gizmos.DrawWireSphere(shotPos.position, 0.05f);
        }
    }

    IEnumerator HitTime(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        
        isHit = false;
        animationHandler.InvincibilityEnd();
    }
}
