using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
<<<<<<< HEAD

    public GameObject playerPrefab; // �÷��̾ �Ҵ��� ����
    public GameObject[] monsterPrefabs; // ���͸� �Ҵ��� ����

    public Tilemap spawnMonsterTilemap; // Ÿ�ϸ��� �Ҵ��� ����
    public Transform spawnPlayerPosition; // �÷��̾��� ��ġ�� �Ҵ��� ����
=======
    private GameObject playerPrefab; // �÷��̾� �������� �Ҵ��� ����
    private GameObject[] enemyPrefabs; // ���� �������� �Ҵ��� ����
    public GameObject potPrefab; // �׾Ƹ� ������

    private Tilemap SpawnEnemyTilemap; // �� ��ȯ Ÿ�ϸ��� �Ҵ��� ����
    public Tilemap SpawnPotTilemap; // �׾Ƹ� ��ȯ Ÿ�ϸ��� �Ҵ��� ����
    private Transform spawnPlayerPosition; // �÷��̾��� ��ġ�� �Ҵ��� ����


    GameManager gameManager;
>>>>>>> dev

    private List<Vector3> spawnPositions = new List<Vector3>(); // ���� ���� ��ġ�� ������ ����Ʈ
    private List<Vector3> usedSpawnPositions = new List<Vector3>(); // ���� ���� ��ġ�� ������ ����Ʈ
    private List<Vector3> availablePositions; // ��� ������ ���� ��ġ�� ������ ����Ʈ

    private void Awake()
    {
<<<<<<< HEAD
        spawnMonsterTilemap = GameObject.Find("MonsterSpawnArea").GetComponent<Tilemap>(); // Ÿ�ϸ� �Ҵ�
        spawnPlayerPosition = GameObject.Find("PlayerSpawnArea").transform; // �÷��̾� ��ġ �Ҵ�
=======
        gameManager = GameManager.Instance; // ���� �Ŵ��� �Ҵ�
        playerPrefab = gameManager.PlayerPrefab; // �÷��̾� �Ҵ�
        enemyPrefabs = gameManager.EnemyPrefabs; // ���� �Ҵ�
        gameManager.FloorTilemap = GameObject.FindWithTag("Floor").GetComponent<Tilemap>(); // Ÿ�ϸ� �Ҵ�
        SpawnEnemyTilemap = GameObject.FindWithTag("EnemySpawnArea").GetComponent<Tilemap>(); // Ÿ�ϸ� �Ҵ�
        spawnPlayerPosition = GameObject.FindWithTag("PlayerSpawnArea").transform; // �÷��̾� ��ġ �Ҵ�
>>>>>>> dev
    }

    private void Start()
    {
        SpawnPots(); // �׾Ƹ� ����
        GetSpawnablePositions(); // ���� ������ ��ġ ��� ��������
        availablePositions = new List<Vector3>(spawnPositions);
        SpawnCharacter(); // ĳ���� ����
        SpawnMonster(0, 5); // ���� ����
    }

    void GetSpawnablePositions()
    {
        spawnPositions.Clear();
        BoundsInt bounds = spawnMonsterTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (spawnMonsterTilemap.HasTile(pos)) // Ÿ���� �����ϴ� ��ġ�� �߰�
            {
                Vector3 worldPos = spawnMonsterTilemap.CellToWorld(pos); // �߽� ����
                spawnPositions.Add(worldPos);
            }
        }
    }

    public void SpawnMonster(int index, int spawnCount)// ���� ���� �Լ�
    {

        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("���� ������ ��ġ�� �����ϴ�!");
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

                if (tile != null) // Ÿ���� �����ϸ�
                {
                    Vector3 worldPosition = SpawnPotTilemap.GetCellCenterWorld(cellPosition); // ���� ��ġ ��ȯ
                    Instantiate(potPrefab, worldPosition, Quaternion.identity);
                }
            }
        }
    }
}


