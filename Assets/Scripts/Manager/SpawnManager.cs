using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    private GameObject playerPrefab; // �÷��̾� �������� �Ҵ��� ����
    private GameObject[] enemyPrefabs; // ���� �������� �Ҵ��� ����
    public GameObject potPrefab; // �׾Ƹ� ������
    public Vector3Int[] potPosition; // �׾Ƹ� ��ġ

    private Tilemap SpawnEnemyTilemap; // �� ��ȯ Ÿ�ϸ��� �Ҵ��� ����
    private Transform spawnPlayerPosition; // �÷��̾��� ��ġ�� �Ҵ��� ����


    GameManager gameManager;

    private List<Vector3> spawnPositions = new List<Vector3>(); // ���� ���� ��ġ�� ������ ����Ʈ
    private List<Vector3> availablePositions; // ��� ������ ���� ��ġ�� ������ ����Ʈ


    //�÷��̾� ��ó ��ȯ X �����ʿ�

    private void Awake()
    {
        gameManager = GameManager.Instance; // ���� �Ŵ��� �Ҵ�
        playerPrefab = gameManager.PlayerPrefab; // �÷��̾� �Ҵ�
        enemyPrefabs = gameManager.EnemyPrefabs; // ���� �Ҵ�
        gameManager.FloorTilemap = GameObject.FindWithTag("Floor").GetComponent<Tilemap>(); // Ÿ�ϸ� �Ҵ�
        SpawnEnemyTilemap = GameObject.FindWithTag("EnemySpawnArea").GetComponent<Tilemap>(); // Ÿ�ϸ� �Ҵ�
        spawnPlayerPosition = GameObject.FindWithTag("PlayerSpawnArea").transform; // �÷��̾� ��ġ �Ҵ�
    }

    private void Start()
    {
        SpawnPots(); // �׾Ƹ� ����
        GetSpawnablePositions(); // ���� ������ ��ġ ��� ��������
        availablePositions = new List<Vector3>(spawnPositions);
        SpawnPlayer(); // ĳ���� ����

        int clearStageLevel = gameManager.ClearStage; // ���� ��������
        int enemyCount = enemyPrefabs.Count(); // ���� ����
        if (clearStageLevel == 0)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                SpawnEnemy(i, 1); // ���� ����
            }
        }
        else if (clearStageLevel % 2 == 0) // ¦�� ���������� ��
        {
            for(int i = 0; i < enemyCount / 2; i++)
            {
                SpawnEnemy(i, 1 + clearStageLevel / 2); // ���� ����
            }
        }
        else
        {
            for (int i = enemyCount - 1 ; i >= enemyCount / 2; i--)
            {
                SpawnEnemy(i, 2 + clearStageLevel / 2); // ���� ����
            }
        }
    }

    void GetSpawnablePositions()
    {
        spawnPositions.Clear();
        BoundsInt bounds = SpawnEnemyTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (SpawnEnemyTilemap.HasTile(pos)) // Ÿ���� �����ϴ� ��ġ�� �߰�
            {
                Vector3 worldPos = SpawnEnemyTilemap.CellToWorld(pos); // �߽� ����
                spawnPositions.Add(worldPos);
            }
        }
    }

    public void SpawnEnemy(int index, int spawnCount)// ���� ���� �޼���
    {

        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("���� ������ ��ġ�� �����ϴ�!");
            return;
        }

        for (int i = 0; i < spawnCount && availablePositions.Count > 0; i++)
        {
            int ableIIndex = UnityEngine.Random.Range(0, availablePositions.Count);
            Vector3 spawnPos = availablePositions[ableIIndex];

            GameObject enemy = Instantiate(enemyPrefabs[index], spawnPos, Quaternion.identity);
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
            availablePositions.RemoveAt(ableIIndex);
        }
    }

    public void SpawnPlayer() // �÷��̾� ���� �޼���
    {
        if(gameManager.PlayerGameObject == null)
        {
            var newObject = Instantiate(playerPrefab, spawnPlayerPosition.position, Quaternion.identity);
            newObject.name = "Archer";
            gameManager.PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
            gameManager.PlayerSkillManagerProperty = gameManager.PlayerGameObject.GetComponent<PlayerSkillManager>();
            gameManager.PlayerControllerProperty = gameManager.PlayerGameObject.GetComponent<PlayerController>();
        }
        gameManager.MainCamera = Camera.main.GetComponent<FollowCamera>();
        gameManager.MainCamera.SetTarget();
    }

    void SpawnPots()
    {
        foreach (Vector3 cell in potPosition)
        {
            Vector3 cellPosition = cell + new Vector3(0.5f, 0.5f, 0);
            Instantiate(potPrefab, cellPosition, Quaternion.identity);
        }
    }
}


