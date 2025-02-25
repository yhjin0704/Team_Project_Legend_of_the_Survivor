using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Player player;
    private Rigidbody2D rigidBody;
    private Animator animator;


    private Vector2 moveInput;

    void Awake()
    {
        player = GetComponent<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        rigidBody.velocity = moveInput * player.speed;
    }

    private void Move()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (moveInput.x > 0)
        {
            player.GetRenderer().transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            player.GetRenderer().transform.localScale = new Vector3(-1, 1, 1);
        }

        player.isMove = (moveInput.x != 0 || moveInput.y != 0);
        animator.SetBool("IsMove", player.isMove);
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
