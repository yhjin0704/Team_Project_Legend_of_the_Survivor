using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int currentMap;            // 현재 맵
    public int minEnemies = 4;        // 몬스터 최소 수 
    public int maxEnemies = 8;        // 몬스터 최고 수

    private void Start()
    {
    }
}
