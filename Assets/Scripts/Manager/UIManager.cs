using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIState // UI���¸� ��Ÿ���� ������
{
    Lobby,
    GamePlay,
    GameOver,
    Menu,
    SkillSelect
}

public class UIManager : MonoBehaviour // UI�� �����ϴ� Ŭ����
{

    // UI�� ������ ���� ����
    [SerializeField] private GameObject sceneUIGameObject;
    [SerializeField] private GamePlayUI gamePlayUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private LobbyUI lobbyUI;
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private SkillSelectUI skillSelectUI;


    // ���� UI ���¸� ������ ���� ����
    private UIState currentState = UIState.Lobby;

    private void Awake() // �ʱ�ȭ �Լ�
    {
        sceneUIGameObject = GameObject.FindWithTag("SceneUI"); // SceneUI �±׸� ���� ������Ʈ�� ã�Ƽ� sceneUIGameObject�� ����
        gamePlayUI = GetComponentInChildren<GamePlayUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� GamePlayUI ������Ʈ�� ã�Ƽ� gamePlayUI�� ����
        gamePlayUI.Init(this); // gamePlayUI�� Init �Լ� ȣ��
        gameOverUI = GetComponentInChildren<GameOverUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� GameOverUI ������Ʈ�� ã�Ƽ� gameOverUI�� ����
        gameOverUI.Init(this); // gameOverUI�� Init �Լ� ȣ��
        lobbyUI = GetComponentInChildren<LobbyUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� LobbyUI ������Ʈ�� ã�Ƽ� lobbyUI�� ����
        lobbyUI.Init(this); // lobbyUI�� Init �Լ� ȣ��
        skillSelectUI = GetComponentInChildren<SkillSelectUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� SkillSelectUI ������Ʈ�� ã�Ƽ� skillSelectUI�� ����
        skillSelectUI.Init(this); // skillSelectUI�� Init �Լ� ȣ��
        menuUI = GetComponentInChildren<MenuUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� MenuUI ������Ʈ�� ã�Ƽ� menuUI�� ����
        menuUI.Init(this); // menuUI�� Init �Լ� ȣ��
    }

    private void Start() // ���� �Լ�
    {
        switch (GameManager.Instance.GetCurrentSceneState())
        {
            case SceneState.Lobby: // ���� �� ���°� �κ��� ��
                ChangeState(UIState.Lobby); // UI ���¸� �κ�� ����
                break;
            case SceneState.Main: // ���� �� ���°� ������ ��
                ChangeState(UIState.GamePlay); // UI ���¸� �����÷��̷� ����
                break;
        }
    }

    public void SetActiveSkillSelect(int[] skills) // ��ų ���� Ȱ��ȭ �Լ�
    {
        skillSelectUI.RandomSkill(skills);
        skillSelectUI.SetActive(UIState.SkillSelect);
    }

    public void SetDeactiveSkillSelect() // ��ų ���� ��Ȱ��ȭ �Լ�
    {
        skillSelectUI.SetActive(UIState.GamePlay);
    }

    public void SetActiveMenu() // �޴� Ȱ��ȭ �Լ�
    {
        menuUI.SetActive(UIState.Menu);
    }

    public void ChangeGold(int gold) // ��� ���� �Լ�
    {
        gamePlayUI.UpdateGoldText(gold);
    }

    public void ChangeEXP(float currentEXP, float maxEXP) // ����ġ ���� �Լ�
    {
        gamePlayUI.UpdateEXPSlider(currentEXP / maxEXP);
    }

    public void ChangeState(UIState state) // UI ���� ���� �Լ�
    {
        currentState = state;
        gamePlayUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
        lobbyUI.SetActive(currentState);
        menuUI.SetActive(currentState);
        skillSelectUI.SetActive(currentState);

        if (state == UIState.GamePlay) // UI ���°� �����÷����� �� �޴� ��ư Ȱ��ȭ
        {
            gamePlayUI.SetActiveMenuButton();
        }

    }
}
