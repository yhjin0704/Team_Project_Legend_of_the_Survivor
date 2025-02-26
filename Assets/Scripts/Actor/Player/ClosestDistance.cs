using UnityEngine;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;

public class ClosestDistance : PlayerController
{
    public Transform player; // 플레이어의 Transform
    public List<GameObject> listMonsters = new List<GameObject>();
    private GameObject target; // 가장 가까운 몬스터

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
            float distance = Vector2.Distance(player.position, monster.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestMonster = monster;
            }
        }

        target = closestMonster;
    }

    public void LookColsestMonster()
    {
        
    }

    private void start()
    {
        UpdateClosestMonster();
    }

    private void Update()
    {
        UpdateClosestMonster();

 
    }
}