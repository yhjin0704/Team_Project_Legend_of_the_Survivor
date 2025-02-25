using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    GameObject sceneUIGameObject;
    GamePlayUI gamePlayUI;
    GameOverUI gameOverUI;
    LobbyUI lobbyUI;
    MenuUI menuUI;
    SkillSelectUI skillSelectUI;

    // ���� UI ���¸� ������ ���� ����
    private UIState currentState;

    private void Awake() // �ʱ�ȭ �Լ�
    {
        sceneUIGameObject = GameObject.FindWithTag("SceneUI"); // SceneUI �±׸� ���� ������Ʈ�� ã�Ƽ� sceneUIGameObject�� ����

        gamePlayUI = sceneUIGameObject.GetComponentInChildren<GamePlayUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� GamePlayUI ������Ʈ�� ã�Ƽ� gamePlayUI�� ����
        gamePlayUI.Init(this); // gamePlayUI�� Init �Լ� ȣ��
        gameOverUI = sceneUIGameObject.GetComponentInChildren<GameOverUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� GameOverUI ������Ʈ�� ã�Ƽ� gameOverUI�� ����
        gameOverUI.Init(this); // gameOverUI�� Init �Լ� ȣ��
        lobbyUI = sceneUIGameObject.GetComponentInChildren<LobbyUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� LobbyUI ������Ʈ�� ã�Ƽ� lobbyUI�� ����
        lobbyUI.Init(this); // lobbyUI�� Init �Լ� ȣ��
        skillSelectUI = sceneUIGameObject.GetComponentInChildren<SkillSelectUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� SkillSelectUI ������Ʈ�� ã�Ƽ� skillSelectUI�� ����
        skillSelectUI.Init(this); // skillSelectUI�� Init �Լ� ȣ��
        menuUI = sceneUIGameObject.GetComponentInChildren<MenuUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� MenuUI ������Ʈ�� ã�Ƽ� menuUI�� ����
        menuUI.Init(this); // menuUI�� Init �Լ� ȣ��

        ChangeState(UIState.Lobby); // UI ���¸� �κ�� ����
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

        if (state == UIState.GamePlay)//
        {
            gamePlayUI.SetActiveMenuButton();
        }

    }
}
