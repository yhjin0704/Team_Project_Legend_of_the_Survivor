using UnityEngine;
using System.Collections.Generic;

public class ClosestDistance : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public List<Monster> listMonsters; // 몬스터 리스트

    void Update()
    {
        // 라인 쏘기: 각 몬스터에 대해 플레이어와의 거리 계산
        List<float> distances = new List<float>();

        foreach (Monster monster in listMonsters)
        {
            float distance = Vector2.Distance(player.position, monster.transform.position);
            distances.Add(distance);
        }

        // 가장 가까운 몬스터 찾기
        int NtMonsterIndex = GetClosestMonster(distances);

        if (closestMonsterIndex != -1)
        {
            Monster closestMonster = listMonsters[closestMonsterIndex];

            // 가까운 몬스터가 공격 범위 내에 있을 경우 자동 공격
            if (Vector2.Distance(player.position, closestMonster.transform.position) <= )
            {
                
            }
        }
    }

    // 가장 가까운 몬스터의 인덱스를 반환
    int GetClosestMonster(List<float> distances)
    {
        int closestIndex = -1;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < distances.Count; i++)
        {
            if (distances[i] < minDistance)
            {
                minDistance = distances[i];
                closestIndex = i;
            }
        }
        return closestIndex;
    }

}
