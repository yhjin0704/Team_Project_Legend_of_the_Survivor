using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Player player;
    private Rigidbody2D rigidBody;
    private Animator animator;


    private Vector2 moveInput;

    protected override void Awake()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
    }

    // Update is called once per frame
    protected override void Update()
    {
        Movement(InputMovement());
    }

    protected override void FixedUpdate()
    {
        rigidBody.velocity = moveInput * player.speed;
    }

    private Vector2 InputMovement()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    protected override void Movement(Vector2 _inputDir)
    {
        if (_inputDir.x > 0)
        {
            player.GetRenderer().transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_inputDir.x < 0)
        {
            player.GetRenderer().transform.localScale = new Vector3(-1, 1, 1);
        }

        player.isMove = (_inputDir.x != 0 || _inputDir.y != 0);
        animator.SetBool("IsMove", player.isMove);
    }

    protected override void Attack()
    {
        if (lookDirection != Vector2.zero)
        { }
    }

    protected override void UseSkills()
    {
        base.UseSkills();

        

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Archer_Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            animator.SetBool("IsAttack", false);
        }

        if (checkDelay <= 0)
        {
            checkDelay = atkDelay;
            if (shotPos.transform.rotation.z >= -0.9f && shotPos.transform.rotation.z < 0.9f)
            {
                _renderer.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                _renderer.transform.localScale = new Vector3(-1, 1, 1);
            }
            animator.SetBool("IsAttack", true);

            foreach (ISkillUseDelay _shottingSkill in skillManager.GetPlayerSkillList())
            {
                _shottingSkill.Use();
            }
        }
    }
}
