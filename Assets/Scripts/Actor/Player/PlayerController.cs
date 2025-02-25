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

        switch(actor.GetState())
        {
            case EState.Idle:
                _rigidbody.velocity = Vector2.zero;
                break;
            case EState.Move:
                Movement(movementDirection);
                break;
            case EState.Attack:
                Attack();
                break;
            case EState.Hit:
                break;
            case EState.Dead:
                break;
        }
        //TestCode
        EnemyController enemyControllerIns = FindObjectOfType<EnemyController>();

        target = enemyControllerIns.gameObject.transform;
    }

    private void InputMovement()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        if (movementDirection != Vector2.zero)
        {
            player.SetState(EState.Move);
        }
        else
        {
            player.SetState(EState.Idle);
        }
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
        
        _rigidbody.velocity = _direction * actor.speed;
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void UseSkills()
    {
        base.UseSkills();

        if (target == null)
        {
            Debug.LogError("Target이 null입니다.");
            return;
        }

        SetShotPos(target);

        foreach (ISkillUseDelay _shootingSkill in skillManager.GetSkillList())
        {
            _shootingSkill.Use();
        }
    }
}
