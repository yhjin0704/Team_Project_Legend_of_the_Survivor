using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{

    public GameObject playerPrefab; // 플레이어를 할당할 변수
    public GameObject[] monsterPrefabs; // 몬스터를 할당할 변수

    public Tilemap spawnMonsterTilemap; // 타일맵을 할당할 변수
    public Transform spawnPlayerPosition; // 플레이어의 위치를 할당할 변수

    private List<Vector3> spawnPositions = new List<Vector3>(); // 몬스터 스폰 위치를 저장할 리스트
    private List<Vector3> usedSpawnPositions = new List<Vector3>(); // 사용된 스폰 위치를 저장할 리스트
    private List<Vector3> availablePositions; // 사용 가능한 스폰 위치를 저장할 리스트

    private void Awake()
    {
        spawnMonsterTilemap = GameObject.Find("MonsterSpawnArea").GetComponent<Tilemap>(); // 타일맵 할당
        spawnPlayerPosition = GameObject.Find("PlayerSpawnArea").transform; // 플레이어 위치 할당
    }

    private void Start()
    {
        GetSpawnablePositions(); // 스폰 가능한 위치 목록 가져오기
        availablePositions = new List<Vector3>(spawnPositions);
        SpawnCharacter(); // 캐릭터 스폰
        SpawnMonster(0, 5); // 몬스터 스폰
    }

    void GetSpawnablePositions()
    {
        spawnPositions.Clear();
        BoundsInt bounds = spawnMonsterTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (spawnMonsterTilemap.HasTile(pos)) // 타일이 존재하는 위치만 추가
            {
                Vector3 worldPos = spawnMonsterTilemap.CellToWorld(pos); // 중심 조정
                spawnPositions.Add(worldPos);
            }
        }
    }

    public void SpawnMonster(int index, int spawnCount)// 몬스터 스폰 함수
    {

        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("스폰 가능한 위치가 없습니다!");
            return;
        }

        for (int i = 0; i < spawnCount && availablePositions.Count > 0; i++)
        {
            int ableIIndex = Random.Range(0, availablePositions.Count);
            Vector3 spawnPos = availablePositions[ableIIndex];

            Instantiate(monsterPrefabs[index], spawnPos, Quaternion.identity);
            availablePositions.RemoveAt(ableIIndex);
        }
    }

    public void SpawnCharacter()
    {
        var newObject = Instantiate(playerPrefab, spawnPlayerPosition.position, Quaternion.identity);
        newObject.name = "Archer";
    }
}


