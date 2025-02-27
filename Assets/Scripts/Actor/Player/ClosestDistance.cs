using UnityEngine;
using System.Collections.Generic;

public class ClosestDistance : PlayerController
{
    public List<GameObject> listMonsters = new List<GameObject>();
    private Player player;

    GameObject closestMonster = null;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>(); // Player 타입의 객체를 찾아서 할당
    }

    // 가장 가까운 몬스터를 리스트의 앞에 넣기
    void UpdateMonsterList()
    {
        // "Monster" 태그를 가진 게임 오브젝트만 찾기
        listMonsters.Clear();
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        if (monsters.Length == 0)
        {
            // 몬스터가 없으면 리스트를 비우고 종료
            return;
        }

        foreach (var monster in monsters)
        {
            listMonsters.Add(monster);
        }

        // 가장 가까운 몬스터가 맨 앞에 오도록 List 정렬
        listMonsters.Sort((monster1, monster2) =>
        {
            Vector3 playerPos = player.transform.position;
            float distance1 = Vector3.Distance(playerPos, monster1.transform.position);
            float distance2 = Vector3.Distance(playerPos, monster2.transform.position);
            return distance1.CompareTo(distance2);  // 거리가 가까운 순으로 정렬
        });
    }

    void FindClosestMonster()
    {
        if (listMonsters.Count == 0)
        {
            closestMonster = null;
            target = null;  // 몬스터가 없으면 target을 null로 설정
            return;
        }

        closestMonster = listMonsters[0];
        target = closestMonster.transform;
    }

    void LookAtClosestMonster()
    {
        if (listMonsters.Count > 0) // 몬스터 리스트가 비어있지 않으면 가장 가까운 몬스터를 찾기
        {
            if (target != null) // 가장 가까운 target이 존재하는 경우
            {
                // 몬스터 방향 계산
                Vector3 playerPos = player.transform.position;
                Vector3 targetPos = target.position;
                Vector3 directionToMonster = targetPos - playerPos;

                // y축 회전 무시하고 x, z축만 반전
                directionToMonster.y = 0;

                // 플레이어 방향 전환
                if (directionToMonster.x > 0)
                {
                    // 오른쪽 보기
                    player.transform.localScale = new Vector3(Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z);
                }
                else if (directionToMonster.x < 0)
                {
                    // 왼쪽 보기
                    player.transform.localScale = new Vector3(-Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z);
                }
            }
        }
        else
        {
            // 몬스터 리스트가 비어있을 경우, 플레이어 입력 방향으로 회전
            float horizontalInput = Input.GetAxis("Horizontal");

            if (horizontalInput > 0)
            {
                // 오른쪽 보기
                player.transform.localScale = new Vector3(Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z);
            }
            else if (horizontalInput < 0)
            {
                // 왼쪽 보기
                player.transform.localScale = new Vector3(-Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        UpdateMonsterList();   // 몬스터 리스트 갱신
        FindClosestMonster();  // 가장 가까운 몬스터 찾기
        LookAtClosestMonster(); // 가장 가까운 몬스터를 바라보는 동작
    }
}
