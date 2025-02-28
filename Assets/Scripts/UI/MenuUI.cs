using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : BaseSceneUI
{
    [SerializeField] private Button gotoLobbyButton;
    [SerializeField] private Button menuExitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager); // ���̽�UI�� Init�� ����

        gotoLobbyButton.onClick.AddListener(() => { 
            uiManager.ChangeState(UIState.Lobby);
            GameManager.Instance.RestartGame();
            Time.timeScale = 1;
        });

        menuExitButton.onClick.AddListener(OnClickExitButton); // �޴������� ��ư �Ҵ�
    }

    private void OnClickExitButton() // �޴������� ��ư Ŭ����
    {
        uiManager.ChangeState(UIState.GamePlay); // �����÷��� ���·� ����
        Time.timeScale = 1;
    }

    protected override UIState GetUIState() // UI���� ��ȯ
    {
        return UIState.Menu;
    }
}
