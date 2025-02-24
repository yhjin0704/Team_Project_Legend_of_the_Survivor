using UnityEngine;
using System.Collections.Generic;

public class ClosestDistance : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public List<Monster> listMonsters; // ���� ����Ʈ

    void Update()
    {
        // ���� ���: �� ���Ϳ� ���� �÷��̾���� �Ÿ� ���
        List<float> distances = new List<float>();

        foreach (Monster monster in listMonsters)
        {
            float distance = Vector2.Distance(player.position, monster.transform.position);
            distances.Add(distance);
        }

        // ���� ����� ���� ã��
        int NtMonsterIndex = GetClosestMonster(distances);

        if (closestMonsterIndex != -1)
        {
            Monster closestMonster = listMonsters[closestMonsterIndex];

            // ����� ���Ͱ� ���� ���� ���� ���� ��� �ڵ� ����
            if (Vector2.Distance(player.position, closestMonster.transform.position) <= )
            {
                
            }
        }
    }

    // ���� ����� ������ �ε����� ��ȯ
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
