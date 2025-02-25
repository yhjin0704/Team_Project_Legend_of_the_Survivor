using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseSceneUI
{
    // �÷��� ��ư, ���� ��ư
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button ExitButton;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);
        RestartButton.onClick.AddListener(OnClickRestartButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton() // ����� ��ư Ŭ�� ��
    {
        uIManager.ChangeState(UIState.GamePlay);
        GameManager.Instance.ChangeScene(SceneState.Main);
    }

    public void OnClickExitButton() // ���� ��ư Ŭ�� ��
    {
        uIManager.ChangeState(UIState.Lobby);
        GameManager.Instance.ChangeScene(SceneState.Lobby);
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
