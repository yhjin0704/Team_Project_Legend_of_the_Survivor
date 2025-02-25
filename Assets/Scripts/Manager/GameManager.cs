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
    public static GameManager Instance; // 싱글톤을 할당할 전역 변수

    [SerializeField]private UIManager uiManager; // UIManager를 할당할 변수

    [SerializeField]private SceneState currentSceneState; // 현재 씬 상태를 저장할 변수

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
}
