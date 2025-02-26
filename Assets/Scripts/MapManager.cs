using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public GameObject portalPrefab;     // 생성할 포탈 프리팹
    private Collider2D portalCollider;
    private SpriteRenderer portalRenderer;

    private void Awake()
    {
        portalCollider = GetComponent<Collider2D>();
        portalRenderer = GetComponent<SpriteRenderer>();

        SetPortalActive(false); // 시작 시 포탈 비활성화
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextRoom();
        }
    }

    // 포탈 활성화/비활성화 함수
    public void SetPortalActive(bool isActive)
    {
        portalCollider.enabled = isActive;
        portalRenderer.enabled = isActive;
    }

    // 다음 방(씬) 로드
    private void LoadNextRoom()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void Update()
    {
        if (AllEnemiesDefeated() == null)
        {
            SpawnPortal();
        }
    }

    // 모든 적이 죽었는지 확인하는 함수
    private bool AllEnemiesDefeated()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    // 포탈 생성 함수
    private void SpawnPortal()
    {
        Vector2 spawnPosition = new Vector2(-9, -4);                                                // 바닥에 포탈 생성 (위치 조정 가능)
        GameObject portalObject = Instantiate(portalPrefab, spawnPosition, Quaternion.identity);
        portalObject.GetComponent<SpawnManager>();
        SetPortalActive(true); // 포탈 활성화
    }
}
