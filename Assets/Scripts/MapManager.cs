using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public GameObject portalPrefab;      // ������ ��Ż ������
    private Collider2D portalCollider;
    private SpriteRenderer portalRenderer;
    public GameObject potPrefab;        // �׾Ƹ� ������
    public Vector2[] potPositions;      // �ν����Ϳ��� ������ ��ǥ �迭
    public int currentMap;            // ���� ��
    public int minEnemies = 4;        // ���� �ּ� �� 
    public int maxEnemies = 8;        // ���� �ְ� ��

    private void Awake()
    {
        portalCollider = GetComponent<Collider2D>();
        portalRenderer = GetComponent<SpriteRenderer>();

        SetPortalActive(false); // ���� �� ��Ż ��Ȱ��ȭ
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextRoom();
        }
    }

    // ��Ż Ȱ��ȭ/��Ȱ��ȭ �Լ�
    public void SetPortalActive(bool isActive)
    {
        portalCollider.enabled = isActive;
        portalRenderer.enabled = isActive;
    }

    // ���� ��(��) �ε�
    private void LoadNextRoom()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }


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



        void Update()
        {
            if (AllEnemiesDefeated() == null)
            {
                SpawnPortal();
            }
        }
    }

    // ��� ���� �׾����� Ȯ���ϴ� �Լ�
    private bool AllEnemiesDefeated()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    // ��Ż ���� �Լ�
    private void SpawnPortal()
    {
        Vector2 spawnPosition = new Vector2(-9, -4);                                                // �ٴڿ� ��Ż ���� (��ġ ���� ����)
        GameObject portalObject = Instantiate(portalPrefab, spawnPosition, Quaternion.identity);
        portalObject.GetComponent<SpawnManager>();
        SetPortalActive(true); // ��Ż Ȱ��ȭ
    }


}
