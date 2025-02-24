using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState // UI���¸� ��Ÿ���� ������
{
    Lobby,
    GamePlay,
    GameOver
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

        /* 
         gameOverUI = sceneUIGameObject.GetComponentInChildren<GameOverUI>(true);
         gameOverUI.Init(this);
         lobbyUI = sceneUIGameObject.GetComponentInChildren<LobbyUI>(true);
         lobbyUI.Init(this);
        // �׽�Ʈ ������ ���� ���� �ʾƼ� ��� �ּ� ó��
         */

        skillSelectUI = sceneUIGameObject.GetComponentInChildren<SkillSelectUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� SkillSelectUI ������Ʈ�� ã�Ƽ� skillSelectUI�� ����
        skillSelectUI.Init(this); // skillSelectUI�� Init �Լ� ȣ��
        menuUI = sceneUIGameObject.GetComponentInChildren<MenuUI>(true); // sceneUIGameObject�� �ڽ� ������Ʈ �� MenuUI ������Ʈ�� ã�Ƽ� menuUI�� ����
        menuUI.Init(this); // menuUI�� Init �Լ� ȣ��
    }

    public void SetActiveSkillSelect(int[] skills) // ��ų ���� Ȱ��ȭ �Լ�
    {
        skillSelectUI.RandomSkill(skills);
        skillSelectUI.gameObject.SetActive(true);
    }

    public void SetDeactiveSkillSelect() // ��ų ���� ��Ȱ��ȭ �Լ�
    {
        skillSelectUI.gameObject.SetActive(false);
    }

    public void SetActiveMenu() // �޴� Ȱ��ȭ �Լ�
    {
        menuUI.gameObject.SetActive(true);
    }

    public void SetDeactiveMenu() // �޴� ��Ȱ��ȭ �Լ�
    {
        menuUI.gameObject.SetActive(false);
        gamePlayUI.SetActiveMenuButton();
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
    }
}
