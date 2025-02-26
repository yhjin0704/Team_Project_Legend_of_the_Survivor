using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int currentMap;            // ���� ��
    public GameObject playerPrefab;   // �÷��̾� ������
    public GameObject monsterPrefab;  // ���� ������
    public int minEnemies = 4;        // ���� �ּ� �� 
    public int maxEnemies = 8;        // ���� �ְ� ��
    public float spawnRadius = 5f;    // �÷��̾� ���� ��ġ���� ���� ���� �Ұ� �Ÿ�
    public LayerMask obstacleLayer;   // ��ֹ� ���̾�
    
    public List<Transform> floorPositions;  // ��Ż�� ������ �ٴ� ��ġ��

    private Dictionary<int, Vector2> playerSpawnPoints = new Dictionary<int, Vector2>(); // �ʺ� �÷��̾� ���� ��ġ

    private void Start()
    {
        InitializePlayerSpawnPoints();
        SpawnPlayer();
        SpawnMonsters();
    }

    // �ʺ� �÷��̾� ���� ��ġ ����
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
            Debug.LogError("�÷��̾� ���� ��ġ�� ã�� �� �����ϴ�!");
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
        for (int i = 0; i < 10; i++) // 10�� �õ�
        {
            Vector2 randomPos = GetRandomPosition();

            // �÷��̾� ���� ������ �ʹ� ������� üũ
            if (Vector2.Distance(randomPos, playerSpawnPoints[currentMap]) < spawnRadius)
                continue;

            // ��ֹ��� ��ġ���� üũ
            Collider2D hit = Physics2D.OverlapCircle(randomPos, 0.5f, obstacleLayer);
            if (hit == null)
            {
                return randomPos;
            }
        }
        return Vector2.zero; // ��ȿ�� ��ġ�� ã�� ����
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(-9f, 9f); // �� ũ�⿡ �°� ����
        float y = Random.Range(-4f, 4f);
        return new Vector2(x, y);
    }
}
