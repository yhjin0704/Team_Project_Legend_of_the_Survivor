using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Player player;
    private Animator animator;
    protected Transform shotPos;// 베이스로 옮겨야됨

    private Vector2 moveInput;

    protected override void Awake()
    {
        player = actor as Player;
        animator = GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
        base.Start();
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
    }

    private void InputMovement()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        int a = 0;
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

    }

    protected override void Attack()
    {
        if (lookDirection != Vector2.zero)
        { }
    }

    protected override void UseSkills()
    {
        base.UseSkills();

        foreach (ISkillUseDelay _shootingSkill in skillManager.GetSkillList())
        {
            _shootingSkill.Use();
        }
    }
}
