using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int OnDead = Animator.StringToHash("OnDead");

    protected Animator animator;
    protected Actor actor;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        actor = GetComponent<Actor>();
    }

    protected virtual void Update()
    {
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > .5f);
    }
    public void Move(bool _boolValue)
    {
        animator.SetBool(IsMoving, _boolValue);
    }

    public void Attack()
    {
        animator.SetBool(IsAttack, true);
    }

    public void AttackEnd()
    {
        animator.SetBool(IsAttack, false);
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
    }

    public void Dead()
    {
        animator.SetTrigger(OnDead);
    }
}
