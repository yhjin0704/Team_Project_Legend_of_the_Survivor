using UnityEngine;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;

public class ClosestDistance : PlayerController
{
    public Transform player;
    public List<GameObject> listMonsters = new List<GameObject>();

    void UpdateClosestMonster()
    {
        if (listMonsters == null || listMonsters.Count == 0)
        {
            target = null;
            return;
        }

        float minDistance = Mathf.Infinity;
        GameObject closestMonster = null;

        foreach (GameObject monster in listMonsters)
        {
            if (monster == null) continue;

            float distance = Vector2.Distance(player.position, monster.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestMonster = monster;
            }
        }

        if (closestMonster != null)
        {
            target = closestMonster.transform;
        }
        else
        {
            target = null; // closestMonster가 null이면 target도 null로 설정
        }
    }
    void LookAtClosestMonster()
    {
        if (target != null)
        {
            Vector2 directionToMonster = target.position - player.position;
            float angle = Mathf.Atan2(directionToMonster.y, directionToMonster.x) * Mathf.Rad2Deg;
            player.rotation = Quaternion.Euler(0, 0, angle); // Z축을 기준으로 회전한다
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        UpdateClosestMonster();
        LookAtClosestMonster();
    }
}
