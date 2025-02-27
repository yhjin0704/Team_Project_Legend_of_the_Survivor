using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    private GameObject playerPrefab; // 플레이어 프리팹을 할당할 변수
    private GameObject[] enemyPrefabs; // 몬스터 프리팹을 할당할 변수

    private Tilemap SpawnEnemyTilemap; // 타일맵을 할당할 변수
    private Transform spawnPlayerPosition; // 플레이어의 위치를 할당할 변수

    GameManager gameManager;

    private List<Vector3> spawnPositions = new List<Vector3>(); // 몬스터 스폰 위치를 저장할 리스트
    private List<Vector3> availablePositions; // 사용 가능한 스폰 위치를 저장할 리스트


    //플레이어 근처 소환 X 수정필요

    private void Awake()
    {
        gameManager = GameManager.Instance; // 게임 매니저 할당
        playerPrefab = gameManager.PlayerPrefab; // 플레이어 할당
        enemyPrefabs = gameManager.EnemyPrefabs; // 몬스터 할당
        SpawnEnemyTilemap = GameObject.FindWithTag("EnemySpawnArea").GetComponent<Tilemap>(); // 타일맵 할당
        spawnPlayerPosition = GameObject.FindWithTag("PlayerSpawnArea").transform; // 플레이어 위치 할당
    }

    private void Start()
    {
        GetSpawnablePositions(); // 스폰 가능한 위치 목록 가져오기
        availablePositions = new List<Vector3>(spawnPositions);
        SpawnPlayer(); // 캐릭터 스폰

        int clearStageLevel = gameManager.ClearStage; // 현재 스테이지
        int enemyCount = enemyPrefabs.Count(); // 몬스터 개수
        if (clearStageLevel == 0)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                SpawnEnemy(i, 1); // 몬스터 스폰
            }
        }
        else if (clearStageLevel % 2 == 0) // 짝수 스테이지일 때
        {
            for(int i = 0; i < enemyCount / 2; i++)
            {
                SpawnEnemy(i, 1 + clearStageLevel / 2); // 몬스터 스폰
            }
        }
        else
        {
            for (int i = enemyCount - 1 ; i >= enemyCount / 2; i--)
            {
                SpawnEnemy(i, 2 + clearStageLevel / 2); // 몬스터 스폰
            }
        }
    }

    void GetSpawnablePositions()
    {
        spawnPositions.Clear();
        BoundsInt bounds = SpawnEnemyTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (SpawnEnemyTilemap.HasTile(pos)) // 타일이 존재하는 위치만 추가
            {
                Vector3 worldPos = SpawnEnemyTilemap.CellToWorld(pos); // 중심 조정
                spawnPositions.Add(worldPos);
            }
        }
    }

    public void SpawnEnemy(int index, int spawnCount)// 몬스터 스폰 메서드
    {

        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("스폰 가능한 위치가 없습니다!");
            return;
        }

        for (int i = 0; i < spawnCount && availablePositions.Count > 0; i++)
        {
            int ableIIndex = UnityEngine.Random.Range(0, availablePositions.Count);
            Vector3 spawnPos = availablePositions[ableIIndex];

            Instantiate(enemyPrefabs[index], spawnPos, Quaternion.identity);
            availablePositions.RemoveAt(ableIIndex);
        }
    }

    public void SpawnPlayer() // 플레이어 스폰 메서드
    {
        var newObject = Instantiate(playerPrefab, spawnPlayerPosition.position, Quaternion.identity);
        newObject.name = "Archer";
        gameManager.MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<FollowCamera>();
        gameManager.MainCamera.SetTarget();
    }
}


