using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int currentMap;            // 현재 맵
    public GameObject playerPrefab;   // 플레이어 프리팹
    public GameObject monsterPrefab;  // 몬스터 프리팹
    public int minEnemies = 4;        // 몬스터 최소 수 
    public int maxEnemies = 8;        // 몬스터 최고 수
    public float spawnRadius = 5f;    // 플레이어 스폰 위치에서 몬스터 스폰 불가 거리
    public LayerMask obstacleLayer;   // 장애물 레이어
    
    public List<Transform> floorPositions;  // 포탈이 생성될 바닥 위치들

    private Dictionary<int, Vector2> playerSpawnPoints = new Dictionary<int, Vector2>(); // 맵별 플레이어 스폰 위치

    private void Start()
    {
        InitializePlayerSpawnPoints();
        SpawnPlayer();
        SpawnMonsters();
    }

    // 맵별 플레이어 스폰 위치 설정
    void InitializePlayerSpawnPoints()
    {
        playerSpawnPoints[1] = new Vector2(0, 0);
        playerSpawnPoints[2] = new Vector2(-8, -4);
        playerSpawnPoints[3] = new Vector2(0, 4);
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null && playerSpawnPoints.ContainsKey(currentMap))
        {
            Vector2 spawnPosition = playerSpawnPoints[currentMap];
            Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("플레이어 스폰 위치를 찾을 수 없습니다!");
        }
    }

    void SpawnMonsters()
    {
        int spawned = 0;
        while (spawned < maxEnemies)
        {
            Vector2 spawnPos = GetValidSpawnPosition();
            if (spawnPos != Vector2.zero)
            {
                Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
                spawned++;
            }
        }
    }

    Vector2 GetValidSpawnPosition()
    {
        for (int i = 0; i < 10; i++) // 10번 시도
        {
            Vector2 randomPos = GetRandomPosition();

            // 플레이어 스폰 지점과 너무 가까운지 체크
            if (Vector2.Distance(randomPos, playerSpawnPoints[currentMap]) < spawnRadius)
                continue;

            // 장애물과 겹치는지 체크
            Collider2D hit = Physics2D.OverlapCircle(randomPos, 0.5f, obstacleLayer);
            if (hit == null)
            {
                return randomPos;
            }
        }
        return Vector2.zero; // 유효한 위치를 찾지 못함
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(-9f, 9f); // 맵 크기에 맞게 조정
        float y = Random.Range(-4f, 4f);
        return new Vector2(x, y);
    }
}
