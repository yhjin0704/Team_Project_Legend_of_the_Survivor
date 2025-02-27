using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
public enum SceneState // UI상태를 나타내는 열거형
{
    Lobby,
    Main,
    Play
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // 싱글톤을 할당할 전역 변수

    private UIManager uiManager; // UIManager를 할당할 변수

    private SceneState currentSceneState; // 현재 씬 상태를 저장할 변수

    public GameObject[] Maps { get; private set; } // 맵을 할당할 변수
    public GameObject PlayerPrefab { get; private set; } // 플레이어를 할당할 변수
    public GameObject PlayerGameObject { get; set; } // 플레이어 게임 오브젝트를 할당할 변수
    public PlayerSkillManager PlayerSkillManagerProperty { get; set; } // 플레이어 스킬 매니저를 할당할 변수
    public GameObject[] EnemyPrefabs { get; private set; } // 몬스터를 할당할 변수
    public FollowCamera MainCamera { get; set; } // 메인 카메라를 할당할 변수
    public Tilemap FloorTilemap { get; set; } // 타일맵을 할당할 변수

    private List<EnemyController> enemies = new List<EnemyController>(); // 적 리스트
    private event Action OnAllEnemiesDefeated; // 모든 적을 물리친 후 발생할 이벤트
    public int ClearStage { get; private set; } // 클리어한 스테이지를 저장할 변수

    public bool IsGameOver { get; private set; } // 게임 오버 상태를 저장할 변수

    private void Awake()
    {
        // 싱글톤 할당
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // UIManager 할당
        uiManager = GetComponentInChildren<UIManager>();

        Maps = Resources.LoadAll<GameObject>("Prefabs/Map"); // 맵 할당
        PlayerPrefab = Resources.Load<GameObject>("Prefabs/Player/Archer"); // 플레이어 할당
        EnemyPrefabs = Resources.LoadAll<GameObject>("Prefabs/Enemy"); // 몬스터 할당

        currentSceneState = SceneState.Lobby; // 초기 씬 상태는 로비
        ClearStage = 0; // 클리어한 스테이지 초기화
    }

    public void SelectSkill(int selectNum)
    {
        switch (selectNum)
        {
            case 0:
                PlayerSkillManagerProperty.IncreaseHp(30);
                break;
            case 1:
                PlayerSkillManagerProperty.IncreaseAtk(5);
                break;
            case 2:
                PlayerSkillManagerProperty.ReduceAtkDelay(0.1f);;
                break;
            case 3:
                PlayerSkillManagerProperty.IncreaseSpeed(1);
                break;
            case 4:
                PlayerSkillManagerProperty.SelectHeal(50);
                break;
        }

    }

    public void AddOnAllEnemiesDefeated(Action action) // 모든 적을 물리친 후 발생할 이벤트 추가 함수
    {
        OnAllEnemiesDefeated += action;
    }
    public void RemoveOnAllEnemiesDefeated(Action action) // 모든 적을 물리친 후 발생할 이벤트 제거 함수
    {
        OnAllEnemiesDefeated -= action;
    }

    public void RegisterEnemy(EnemyController enemy) // 적 리스트에 몬스터 추가 함수
    {
        enemies.Add(enemy);
    }
    public void UnregisterEnemy(EnemyController enemy) // 적 리스트에서 몬스터 제거 함수
    {
        ClearStage++;
        enemies.Remove(enemy);

        if (enemies.Count == 0) // 모든 적이 죽었을 때
        {
            OnAllEnemiesDefeated?.Invoke(); // 이벤트 발생
        }
    }

    public SceneState GetCurrentSceneState() // 현재 씬 상태를 반환하는 함수
    {
        return currentSceneState;
    }

    public void ChangeScene(SceneState sceneState) // 씬 상태를 변경하는 함수
    {
        currentSceneState = sceneState;

        // 씬 상태에 따라 씬을 변경
        switch (currentSceneState)
        {
            case SceneState.Lobby:
                SceneManager.LoadScene("LobbyScene");
                break;
            case SceneState.Main:
                SceneManager.LoadScene("MainScene");
                break;
            case SceneState.Play:
                SceneManager.LoadScene("PlayScene");
                break;
        }
    }

    public void GameOver() // 게임 오버 메서드
    {
        if (PlayerGameObject != null)
        {
            Destroy(PlayerGameObject.gameObject); // 플레이어 게임 오브젝트 삭제
        }
        IsGameOver = true;
        uiManager.ChangeState(UIState.GameOver);
    }

    public bool IsGoldOnTilemap(Vector3 position)
    {
        if (FloorTilemap == null)
        {
            Debug.LogError("Tilemap is null");
            return false;
        }

        // 골드 오브젝트의 월드 좌표를 타일 좌표로 변환
        Vector3Int cellPosition = FloorTilemap.WorldToCell(position);

        // 해당 위치에 타일이 있는지 확인
        return FloorTilemap.HasTile(cellPosition);
    }
}
