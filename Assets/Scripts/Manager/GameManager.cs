using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
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
    public GameObject PlayerGameObject { get; set; } // �÷��̾� ���� ������Ʈ�� �Ҵ��� ����
    public PlayerSkillManager PlayerSkillManagerProperty { get; set; } // �÷��̾� ��ų �Ŵ����� �Ҵ��� ����
    public GameObject[] EnemyPrefabs { get; private set; } // ���͸� �Ҵ��� ����
    public FollowCamera MainCamera { get; set; } // ���� ī�޶� �Ҵ��� ����
    public Tilemap FloorTilemap { get; set; } // Ÿ�ϸ��� �Ҵ��� ����

    private List<EnemyController> enemies = new List<EnemyController>(); // �� ����Ʈ
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

    public void AddOnAllEnemiesDefeated(Action action) // ��� ���� ����ģ �� �߻��� �̺�Ʈ �߰� �Լ�
    {
        OnAllEnemiesDefeated += action;
    }
    public void RemoveOnAllEnemiesDefeated(Action action) // ��� ���� ����ģ �� �߻��� �̺�Ʈ ���� �Լ�
    {
        OnAllEnemiesDefeated -= action;
    }

    public void RegisterEnemy(EnemyController enemy) // �� ����Ʈ�� ���� �߰� �Լ�
    {
        enemies.Add(enemy);
    }
    public void UnregisterEnemy(EnemyController enemy) // �� ����Ʈ���� ���� ���� �Լ�
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
        if (PlayerGameObject != null)
        {
            Destroy(PlayerGameObject.gameObject); // �÷��̾� ���� ������Ʈ ����
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

        // ��� ������Ʈ�� ���� ��ǥ�� Ÿ�� ��ǥ�� ��ȯ
        Vector3Int cellPosition = FloorTilemap.WorldToCell(position);

        // �ش� ��ġ�� Ÿ���� �ִ��� Ȯ��
        return FloorTilemap.HasTile(cellPosition);
    }
}
