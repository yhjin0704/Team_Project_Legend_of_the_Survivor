using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private List<Enemy> enemies = new List<Enemy>(); // 적 리스트
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
        currentSceneState = SceneState.Lobby; // 초기 씬 상태는 로비
    }

    public void AddOnAllEnemiesDefeated(Action action) // 모든 적을 물리친 후 발생할 이벤트 추가 함수
    {
        OnAllEnemiesDefeated += action;
    }

    public void RemoveOnAllEnemiesDefeated(Action action) // 모든 적을 물리친 후 발생할 이벤트 제거 함수
    {
        OnAllEnemiesDefeated -= action;
    }

    public void RegisterEnemy(Enemy enemy) // 적 리스트에 몬스터 추가 함수
    {
        enemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy) // 적 리스트에서 몬스터 제거 함수
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
        IsGameOver = true;
        uiManager.ChangeState(UIState.GameOver);
    }
}
