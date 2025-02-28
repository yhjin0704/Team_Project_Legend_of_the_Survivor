using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseSceneUI
{
    // �÷��� ��ư, ���� ��ư
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button ExitButton;
    private GameManager gameManager;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        gameManager = GameManager.Instance;
        RestartButton.onClick.AddListener(OnClickRestartButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton() // ����� ��ư Ŭ�� ��
    {
        gameManager.ChangeScene(SceneState.Play);
        gameManager.ResetGame();
        uiManager.ChangeState(UIState.GamePlay);
    }

    public void OnClickExitButton() // ���� ��ư Ŭ�� ��
    {
        gameManager.RestartGame();
        uiManager.ChangeState(UIState.Lobby);
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
