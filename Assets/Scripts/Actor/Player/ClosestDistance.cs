using UnityEngine;
using System.Collections.Generic;

public class ClosestDistance : MonoBehaviour
{
    public transform player; // 플레이어 transform
    public list<monster> listmonsters = new list<monster>(); // 몬스터 리스트
    private monster target;

    void updateclosestmonster()
    {
        if (listmonsters == null || listmonsters.count == 0)
        {
            target = null;
            return;
        }

        float mindistance = mathf.infinity;
        monster closestmonster = null;

        foreach (monster monster in listmonsters)
        {
            float distance = vector2.distance(player.position, monster.transform.position);
            if (distance < mindistance)
            {
                mindistance = distance;
                closestmonster = monster;
            }
        }

        target = closestmonster;
    }

    private void start()
    {
        updateclosestmonster();
    }
}