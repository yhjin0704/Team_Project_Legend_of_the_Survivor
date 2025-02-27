using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseController
{
    private Player player;
    private Animator animator;

    private Vector2 moveInput;

    GameObject closestMonster = null;

    public bool isDoubleShot = false;

    GameManager gameManager = GameManager.Instance;

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

        UpdateMonsterList();

        switch (actor.GetState())
        {
            case EState.Idle:
                _rigidbody.velocity = Vector2.zero;
                animationHandler.Move(_rigidbody.velocity);
                break;
            case EState.Move:
                Movement(movementDirection);
                if (isHit == false)
                {
                    animationHandler.Move(_rigidbody.velocity);
                }
                else 
                {

                }
                    break;
            case EState.Dead:
                _rigidbody.velocity = Vector2.zero;
                break;
            default:
                break;
        }
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

    protected override void UseSkills()
    {
        base.UseSkills();

        if (target == null)
        {
            Debug.LogError("Target이 null입니다.");
            return;
        }

        isAttacking = true;
        animationHandler.Attack();
        LookAtTarget();
        SetShotPos(target);

        foreach (ISkillUseDelay _shootingSkill in skillManager.GetSkillList())
        {
            _shootingSkill.Use();
            if (isDoubleShot == true)
            {
                StartCoroutine(UseDoubleShot(_shootingSkill));
            }
        }
        isAttacking = false;
        StartCoroutine(AtkAnimEnd(0.75f));
    }

    void UpdateMonsterList()
    {
        List<GameObject> monsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        for (int i = monsters.Count - 1; i >= 0; i--)
        {
            Vector3 dir = monsters[i].transform.position - player.transform.position;

            RaycastHit2D ray = Physics2D.Raycast(player.transform.position, dir, dir.magnitude, 1 << LayerMask.NameToLayer("Wall"));

            if (ray.collider != null)
            {
                monsters.RemoveAt(i);
            }
        }

        if (monsters == null || monsters.Count == 0)
        {
            closestMonster = null;
            target = null;  // 몬스터가 없으면 target을 null로 설정
            return;
        }

        while (monsters.Count > 1)
        {
            float distance1 = Vector3.Distance(player.transform.position, monsters[0].transform.position);
            float distance2 = Vector3.Distance(player.transform.position, monsters[1].transform.position);

            if (distance1 < distance2)
            {
                monsters.RemoveAt(1);
            }
            else
            {
                monsters.RemoveAt(0);
            }
        }
        closestMonster = monsters[0];

        target = closestMonster.transform;
    }

    // 가장 가까운 몬스터를 바라보는 메소드
    void LookAtTarget()
    {
        if (target != null)
        {
            if (shotPos.transform.position.x <= target.position.x)
            {
                player.GetRenderer().transform.localScale = new Vector3(1, 1, 1);
            }
            else if (shotPos.transform.position.x > target.position.x)
            {
                player.GetRenderer().transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    protected override void Dead()
    {
        base.Dead();

        gameManager.GameOver();
    }

    IEnumerator AtkAnimEnd(float _delay)
    {
        yield return new WaitForSeconds(_delay);

        animationHandler.AttackEnd();
    }

    IEnumerator UseDoubleShot(ISkillUseDelay _skill)
    {
        yield return new WaitForSeconds(0.3f);

        _skill.Use();
    }
}
