using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseSceneUI
{
    // 플레이 버튼, 종료 버튼
    [SerializeField] private Button playButton;
    [SerializeField] private Button ExitButton;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);
        playButton.onClick.AddListener(OnClickPlayButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickPlayButton() // 플레이 버튼 클릭 시
    {
        uIManager.ChangeState(UIState.GamePlay);
        GameManager.Instance.ChangeScene(SceneState.Main);
    }

    public void OnClickExitButton()
    {

    }

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
}
