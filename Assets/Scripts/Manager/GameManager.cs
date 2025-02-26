using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneState // UI���¸� ��Ÿ���� ������
{
    Lobby,
    Main,
    Play
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // �̱����� �Ҵ��� ���� ����

    [SerializeField]private UIManager uiManager; // UIManager�� �Ҵ��� ����

    [SerializeField]private SceneState currentSceneState; // ���� �� ���¸� ������ ����

    private void Awake()
    {
        // �̱��� �Ҵ�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // UIManager �Ҵ�
        uiManager = GetComponentInChildren<UIManager>();
        currentSceneState = SceneState.Lobby; // �ʱ� �� ���´� �κ�
    }

    public SceneState GetCurrentSceneState() // ���� �� ���¸� ��ȯ�ϴ� �Լ�
    {
        return currentSceneState;
    }

    public void ChangeScene(SceneState sceneState) // �� ���¸� �����ϴ� �Լ�
    {
        currentSceneState = sceneState;

        // �� ���¿� ���� ���� ����
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
