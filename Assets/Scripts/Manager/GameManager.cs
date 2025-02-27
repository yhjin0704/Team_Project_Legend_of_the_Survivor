using System;
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
    public static GameManager Instance { get; private set; } // �̱����� �Ҵ��� ���� ����

    private UIManager uiManager; // UIManager�� �Ҵ��� ����

    private SceneState currentSceneState; // ���� �� ���¸� ������ ����

    public GameObject[] Maps { get; private set; } // ���� �Ҵ��� ����
    public GameObject PlayerPrefab { get; private set; } // �÷��̾ �Ҵ��� ����
    public GameObject[] EnemyPrefabs { get; private set; } // ���͸� �Ҵ��� ����
    public FollowCamera MainCamera { get; set; } // ���� ī�޶� �Ҵ��� ����

    private List<Enemy> enemies = new List<Enemy>(); // �� ����Ʈ
    private event Action OnAllEnemiesDefeated; // ��� ���� ����ģ �� �߻��� �̺�Ʈ
    public int ClearStage { get; private set; } // Ŭ������ ���������� ������ ����

    public bool IsGameOver { get; private set; } // ���� ���� ���¸� ������ ����

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

        Maps = Resources.LoadAll<GameObject>("Prefabs/Map"); // �� �Ҵ�
        PlayerPrefab = Resources.Load<GameObject>("Prefabs/Player/Archer"); // �÷��̾� �Ҵ�
        EnemyPrefabs = Resources.LoadAll<GameObject>("Prefabs/Enemy"); // ���� �Ҵ�

        currentSceneState = SceneState.Lobby; // �ʱ� �� ���´� �κ�
        ClearStage = 0; // Ŭ������ �������� �ʱ�ȭ
    }

    public void AddOnAllEnemiesDefeated(Action action) // ��� ���� ����ģ �� �߻��� �̺�Ʈ �߰� �Լ�
    {
        OnAllEnemiesDefeated += action;
    }

    public void RemoveOnAllEnemiesDefeated(Action action) // ��� ���� ����ģ �� �߻��� �̺�Ʈ ���� �Լ�
    {
        OnAllEnemiesDefeated -= action;
    }

    public void RegisterEnemy(Enemy enemy) // �� ����Ʈ�� ���� �߰� �Լ�
    {
        enemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy) // �� ����Ʈ���� ���� ���� �Լ�
    {
        ClearStage++;
        enemies.Remove(enemy);

        if (enemies.Count == 0) // ��� ���� �׾��� ��
        {
            OnAllEnemiesDefeated?.Invoke(); // �̺�Ʈ �߻�
        }
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

    public void GameOver() // ���� ���� �޼���
    {
        IsGameOver = true;
        uiManager.ChangeState(UIState.GameOver);
    }
}
