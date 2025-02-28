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

        int clearStageLevel = gameManager.ClearStage + 1; // ���� ��������
        int enemyCount = enemyPrefabs.Count(); // ���� ����
        switch (clearStageLevel % 5)
        {
            case 0:
                SpawnEnemy(0, clearStageLevel / 5); // ���� ����
                break;
            case 1:
                SpawnEnemy(0, 2 + clearStageLevel / 5); // ���� ����
                break;
            case 2:
                SpawnEnemy(1, 1 + clearStageLevel / 5); // ���� ����
                SpawnEnemy(2, 1 + clearStageLevel / 5); // ���� ����
                break;
            case 3:
                SpawnEnemy(2, 2 + clearStageLevel / 5); // ���� ����
                break;
            case 4:
                SpawnEnemy(1, 2 + clearStageLevel / 5); // ���� ����
                SpawnEnemy(2, 1 + clearStageLevel / 5); // ���� ����
                break;
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
        if (gameManager.PlayerGameObject == null)
        {
            var newObject = Instantiate(playerPrefab, spawnPlayerPosition.position, Quaternion.identity);
            newObject.name = "Archer";
            gameManager.PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
            gameManager.PlayerSkillManagerProperty = gameManager.PlayerGameObject.GetComponent<PlayerSkillManager>();
            gameManager.PlayerControllerProperty = gameManager.PlayerGameObject.GetComponent<PlayerController>();
            gameManager.PlayerControllerProperty.isDoubleShot = false;
        }
        else
        {
            gameManager.PlayerGameObject.transform.position = spawnPlayerPosition.position;
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


