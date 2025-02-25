using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Player player;
    private Animator animator;

    private Vector2 moveInput;

    protected override void Awake()
    {
        base.Awake();
        player = actor as Player;
        animator = GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
        base.Start();

        //TestCode
        target = GameObject.Find("Orc").transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        InputMovement();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Movement(movementDirection);
    }

    private void InputMovement()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    protected override void Movement(Vector2 _direction)
    {
        base.Movement(_direction);

        if (_direction.x > 0)
        {
            player.GetRenderer().transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_direction.x < 0)
        {
            player.GetRenderer().transform.localScale = new Vector3(-1, 1, 1);
        }

        player.SetIsMove((_direction.x != 0 || _direction.y != 0));
        animator.SetBool("IsMove", player.GetIsMove());

        _rigidbody.velocity = _direction * actor.speed;
        MoveCheck();
    }

    protected override void Attack()
    {
        if (lookDirection != Vector2.zero)
        { }
    }

    protected override void UseSkills()
    {
        base.UseSkills();

        if (target == null)
        {
            Debug.LogError("Target�� null�Դϴ�.");
            return;
        }

        SetShotPos(target);

        foreach (ISkillUseDelay _shootingSkill in skillManager.GetSkillList())
        {
            _shootingSkill.Use();
        }
    }
}
