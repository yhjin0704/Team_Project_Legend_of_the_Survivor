using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
<<<<<<< HEAD

    public GameObject playerPrefab; // 플레이어를 할당할 변수
    public GameObject[] monsterPrefabs; // 몬스터를 할당할 변수

    public Tilemap spawnMonsterTilemap; // 타일맵을 할당할 변수
    public Transform spawnPlayerPosition; // 플레이어의 위치를 할당할 변수
=======
    private GameObject playerPrefab; // 플레이어 프리팹을 할당할 변수
    private GameObject[] enemyPrefabs; // 몬스터 프리팹을 할당할 변수
    public GameObject potPrefab; // 항아리 프리팹

    private Tilemap SpawnEnemyTilemap; // 적 소환 타일맵을 할당할 변수
    public Tilemap SpawnPotTilemap; // 항아리 소환 타일맵을 할당할 변수
    private Transform spawnPlayerPosition; // 플레이어의 위치를 할당할 변수


    GameManager gameManager;
>>>>>>> dev

    private List<Vector3> spawnPositions = new List<Vector3>(); // 몬스터 스폰 위치를 저장할 리스트
    private List<Vector3> usedSpawnPositions = new List<Vector3>(); // 사용된 스폰 위치를 저장할 리스트
    private List<Vector3> availablePositions; // 사용 가능한 스폰 위치를 저장할 리스트

    private void Awake()
    {
<<<<<<< HEAD
        spawnMonsterTilemap = GameObject.Find("MonsterSpawnArea").GetComponent<Tilemap>(); // 타일맵 할당
        spawnPlayerPosition = GameObject.Find("PlayerSpawnArea").transform; // 플레이어 위치 할당
=======
        gameManager = GameManager.Instance; // 게임 매니저 할당
        playerPrefab = gameManager.PlayerPrefab; // 플레이어 할당
        enemyPrefabs = gameManager.EnemyPrefabs; // 몬스터 할당
        gameManager.FloorTilemap = GameObject.FindWithTag("Floor").GetComponent<Tilemap>(); // 타일맵 할당
        SpawnEnemyTilemap = GameObject.FindWithTag("EnemySpawnArea").GetComponent<Tilemap>(); // 타일맵 할당
        spawnPlayerPosition = GameObject.FindWithTag("PlayerSpawnArea").transform; // 플레이어 위치 할당
>>>>>>> dev
    }

    private void Start()
    {
        SpawnPots(); // 항아리 스폰
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

<<<<<<< HEAD
            Instantiate(monsterPrefabs[index], spawnPos, Quaternion.identity);
=======
            GameObject enemy = Instantiate(enemyPrefabs[index], spawnPos, Quaternion.identity);
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
>>>>>>> dev
            availablePositions.RemoveAt(ableIIndex);
        }
    }

    public void SpawnCharacter()
    {
<<<<<<< HEAD
        var newObject = Instantiate(playerPrefab, spawnPlayerPosition.position, Quaternion.identity);
        newObject.name = "Archer";
=======
        if(gameManager.PlayerGameObject == null)
        {
            var newObject = Instantiate(playerPrefab, spawnPlayerPosition.position, Quaternion.identity);
            newObject.name = "Archer";
            gameManager.PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
            gameManager.PlayerSkillManagerProperty = gameManager.PlayerGameObject.GetComponent<PlayerSkillManager>();
        }
        gameManager.MainCamera = Camera.main.GetComponent<FollowCamera>();
        gameManager.MainCamera.SetTarget();
>>>>>>> dev
    }

    void SpawnPots()
    {
        BoundsInt bounds = SpawnPotTilemap.cellBounds;
        TileBase[] allTiles = SpawnPotTilemap.GetTilesBlock(bounds);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                TileBase tile = SpawnPotTilemap.GetTile(cellPosition);

                if (tile != null) // 타일이 존재하면
                {
                    Vector3 worldPosition = SpawnPotTilemap.GetCellCenterWorld(cellPosition); // 월드 위치 변환
                    Instantiate(potPrefab, worldPosition, Quaternion.identity);
                }
            }
        }
    }
}


