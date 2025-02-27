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
        base.Init(uiManager); // 배이스UI의 Init을 실행

        gotoLobbyButton.onClick.AddListener(() => { 
            uiManager.ChangeState(UIState.Lobby);
            GameManager.Instance.RestartGame();
            Time.timeScale = 1;
        });

        menuExitButton.onClick.AddListener(OnClickExitButton); // 메뉴나가기 버튼 할당
    }

    private void OnClickExitButton() // 메뉴나가기 버튼 클릭시
    {
        uiManager.ChangeState(UIState.GamePlay); // 게임플레이 상태로 변경
        Time.timeScale = 1;
    }

    protected override UIState GetUIState() // UI상태 반환
    {
        return UIState.Menu;
    }
}
