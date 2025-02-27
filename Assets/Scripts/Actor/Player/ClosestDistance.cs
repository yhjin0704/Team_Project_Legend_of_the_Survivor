using UnityEngine;
using System.Collections.Generic;

public class ClosestDistance : PlayerController
{
    public List<GameObject> listMonsters = new List<GameObject>();
    private Player player;  // Player 클래스를 사용

    GameObject closestMonster = null;

        // 가장 가까운 몬스터를 리스트의 앞에 넣기
        void UpdateMonsterList()
    {
        // "Monster" 태그를 가진 게임 오브젝트만 찾기
        listMonsters.Clear();

        // "Monster" 태그를 가진 게임 오브젝트들만 찾음
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

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
    void LookAtClosestMonster()
    {
        if (target != null) // target이 null이 아닌 경우 (가장 가까운 몬스터가 있으면)
        {
            // 몬스터 방향 계산
            Vector3 directionToMonster = target.position - player.transform.position;

            // y축 회전은 고려하지 않으므로 방향을 2D로 처리
            directionToMonster.y = 0;  // y축을 무시하고, 수평 방향만 사용

            // 플레이어의 현재 위치와 target의 방향을 비교하여 회전
            if (directionToMonster.x > 0 && player.transform.localScale.x < 0)
            {
                // 타겟이 오른쪽에 있을 때, 플레이어가 왼쪽을 보고 있으면 방향 전환
                player.transform.localScale = new Vector3(Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z);
            }
            else if (directionToMonster.x < 0 && player.transform.localScale.x > 0)
            {
                // 타겟이 왼쪽에 있을 때, 플레이어가 오른쪽을 보고 있으면 방향 전환
                player.transform.localScale = new Vector3(-Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z);
            }

        }
        else
        {
            // 몬스터가 없을 때, 플레이어의 이동 방향에 따라 시점 바꾸기
            Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

            if (movementDirection.x > 0)
            {
                player.transform.localScale = new Vector3(Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z);
            }
            else if (movementDirection.x < 0)
            {
                player.transform.localScale = new Vector3(-Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // player가 null인지 확인하고, null일 경우 처리를 추가할 수도 있습니다.
        // if (player == null) return;  // player가 null이면 실행하지 않도록 추가

        UpdateMonsterList();   // 몬스터 리스트 갱신
        FindClosestMonster();  // 가장 가까운 몬스터 찾기
        LookAtClosestMonster(); // 가장 가까운 몬스터를 바라보는 동작
    }
}
