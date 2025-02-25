using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseSceneUI
{
    // 플레이 버튼, 종료 버튼
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button ExitButton;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);
        RestartButton.onClick.AddListener(OnClickRestartButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton() // 재시작 버튼 클릭 시
    {
        uIManager.ChangeState(UIState.GamePlay);
        GameManager.Instance.ChangeScene(SceneState.Main);
    }

    public void OnClickExitButton() // 종료 버튼 클릭 시
    {
        uIManager.ChangeState(UIState.Lobby);
        GameManager.Instance.ChangeScene(SceneState.Lobby);
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
