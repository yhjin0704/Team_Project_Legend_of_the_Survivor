using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseSceneUI
{
    // �÷��� ��ư, ���� ��ư
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button ExitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        RestartButton.onClick.AddListener(OnClickRestartButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton() // ����� ��ư Ŭ�� ��
    {
        GameManager.Instance.ChangeScene(SceneState.Main);
        uiManager.ChangeState(UIState.GamePlay);
    }

    public void OnClickExitButton() // ���� ��ư Ŭ�� ��
    {
        GameManager.Instance.ChangeScene(SceneState.Lobby);
        uiManager.ChangeState(UIState.Lobby);
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
