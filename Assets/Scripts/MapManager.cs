using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    private GameObject portalInstance;
    public GameObject potPrefab;        // �׾Ƹ� ������
    public Vector2[] potPositions;      // �ν����Ϳ��� ������ ��ǥ �迭
    public int currentMap;            // ���� ��
    public int minEnemies = 4;        // ���� �ּ� �� 
    public int maxEnemies = 8;        // ���� �ְ� ��





    void Start()
    {
        SpawnPots();
    }

    void SpawnPots()
    {
        foreach (Vector2 position in potPositions)
        {
            Instantiate(potPrefab, position, Quaternion.identity);
        }
    }
}
