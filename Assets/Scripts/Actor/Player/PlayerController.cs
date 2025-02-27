using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Player player;
    private Animator animator;

    private Vector2 moveInput;

    public List<GameObject> listMonsters = new List<GameObject>();
    GameObject closestMonster = null;

    public bool isDoubleShot = true;

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
        //TestCode
        UpdateMonsterList();   // 몬스터 리스트 갱신
        FindClosestMonster();  // 가장 가까운 몬스터 찾기
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
        listMonsters.Clear();

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");

        int count = 0;

        foreach (var monster in monsters)
        {
            // 몬스터를 리스트에 추가
            listMonsters.Add(monster);
            count++;
        }

        // 거리 기준으로 리스트 정렬 (가장 가까운 몬스터가 맨 앞에 오도록)
        listMonsters.Sort((monster1, monster2) =>
        {
            float distance1 = Vector3.Distance(player.transform.position, monster1.transform.position);
            float distance2 = Vector3.Distance(player.transform.position, monster2.transform.position);
            return distance1.CompareTo(distance2);  // 거리가 가까운 순으로 정렬
        });
    }

    void FindClosestMonster()
    {
        if (listMonsters == null || listMonsters.Count == 0)
        {
            closestMonster = null;
            target = null;  // 몬스터가 없으면 target을 null로 설정
            return;
        }

        // 가장 가까운 몬스터는 이미 리스트의 앞에 위치하므로
        closestMonster = listMonsters[0];

        // 가장 가까운 몬스터의 Transform을 target에 할당
        target = closestMonster != null ? closestMonster.transform : null;
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
