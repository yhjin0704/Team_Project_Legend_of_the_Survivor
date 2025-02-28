using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public enum UIState // UI���¸� ��Ÿ���� ������
{
    Lobby,
    GamePlay,
    GameOver,
    Menu,
    SkillSelect,
    Loading
}

public class UIManager : MonoBehaviour // UI�� �����ϴ� Ŭ����
{

    // UI�� ������ ���� ����
    [SerializeField] private GamePlayUI gamePlayUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private LobbyUI lobbyUI;
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private SkillSelectUI skillSelectUI;
    [SerializeField] private LoadingUI loadingUI;


    // ���� UI ���¸� ������ ���� ����
    private UIState currentState;

    private void Awake() // �ʱ�ȭ �Լ�
    {
        gamePlayUI = GetComponentInChildren<GamePlayUI>(true); // �ڽ� ������Ʈ �� GamePlayUI ������Ʈ�� ã�Ƽ� gamePlayUI�� ����
        gamePlayUI.Init(this); // gamePlayUI�� Init �Լ� ȣ��
        gameOverUI = GetComponentInChildren<GameOverUI>(true); // �ڽ� ������Ʈ �� GameOverUI ������Ʈ�� ã�Ƽ� gameOverUI�� ����
        gameOverUI.Init(this); // gameOverUI�� Init �Լ� ȣ��
        lobbyUI = GetComponentInChildren<LobbyUI>(true); // �ڽ� ������Ʈ �� LobbyUI ������Ʈ�� ã�Ƽ� lobbyUI�� ����
        lobbyUI.Init(this); // lobbyUI�� Init �Լ� ȣ��
        skillSelectUI = GetComponentInChildren<SkillSelectUI>(true); // �ڽ� ������Ʈ �� SkillSelectUI ������Ʈ�� ã�Ƽ� skillSelectUI�� ����
        skillSelectUI.Init(this); // skillSelectUI�� Init �Լ� ȣ��
        menuUI = GetComponentInChildren<MenuUI>(true); // �ڽ� ������Ʈ �� MenuUI ������Ʈ�� ã�Ƽ� menuUI�� ����
        menuUI.Init(this); // menuUI�� Init �Լ� ȣ��
        loadingUI = GetComponentInChildren<LoadingUI>(true); // �ڽ� ������Ʈ �� LoadingUI ������Ʈ�� ã�Ƽ� loadingUI�� ����
        loadingUI.Init(this); // loadingUI�� Init �Լ� ȣ��
        ChangeState(UIState.Lobby); // UI ���¸� �κ�� 
    }

    public void SetActiveSkillSelect() // ��ų ���� Ȱ��ȭ �Լ�
    {
        skillSelectUI.RandomSkill();
        skillSelectUI.SetSkillText();
        skillSelectUI.SetActive(UIState.SkillSelect);
        Time.timeScale = 0;
    }

    public void SetDeactiveSkillSelect() // ��ų ���� ��Ȱ��ȭ �Լ�
    {
        skillSelectUI.SetActive(UIState.GamePlay);
        Time.timeScale = 1;
    }

    public void SetActiveMenu() // �޴� Ȱ��ȭ �Լ�
    {
        menuUI.SetActive(UIState.Menu);
        Time.timeScale = 0;
    }

    public void ChangeGold(int gold) // ��� ���� �Լ�
    {
        gamePlayUI.UpdateGoldText(gold);
    }

    public void ChangeEXP(float currentEXP, float maxEXP) // ����ġ ���� �Լ�
    {
        gamePlayUI.UpdateEXPSlider(currentEXP / maxEXP);
    }

    //�ڷ�ƾ���� �ε� ȭ���� ���� �Լ�
    public IEnumerator Loading()
    {
        loadingUI.SetActive(UIState.Loading);
        yield return new WaitForSeconds(0.3f);
        loadingUI.SetActive(currentState);
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
