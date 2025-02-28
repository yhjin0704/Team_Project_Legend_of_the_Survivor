using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public enum UIState // UI상태를 나타내는 열거형
{
    Lobby,
    GamePlay,
    GameOver,
    Menu,
    SkillSelect,
    Loading
}

public class UIManager : MonoBehaviour // UI를 관리하는 클래스
{

    // UI를 저장할 변수 선언
    [SerializeField] private GamePlayUI gamePlayUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private LobbyUI lobbyUI;
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private SkillSelectUI skillSelectUI;
    [SerializeField] private LoadingUI loadingUI;


    // 현재 UI 상태를 저장할 변수 선언
    private UIState currentState;

    private void Awake() // 초기화 함수
    {
        gamePlayUI = GetComponentInChildren<GamePlayUI>(true); // 자식 오브젝트 중 GamePlayUI 컴포넌트를 찾아서 gamePlayUI에 저장
        gamePlayUI.Init(this); // gamePlayUI의 Init 함수 호출
        gameOverUI = GetComponentInChildren<GameOverUI>(true); // 자식 오브젝트 중 GameOverUI 컴포넌트를 찾아서 gameOverUI에 저장
        gameOverUI.Init(this); // gameOverUI의 Init 함수 호출
        lobbyUI = GetComponentInChildren<LobbyUI>(true); // 자식 오브젝트 중 LobbyUI 컴포넌트를 찾아서 lobbyUI에 저장
        lobbyUI.Init(this); // lobbyUI의 Init 함수 호출
        skillSelectUI = GetComponentInChildren<SkillSelectUI>(true); // 자식 오브젝트 중 SkillSelectUI 컴포넌트를 찾아서 skillSelectUI에 저장
        skillSelectUI.Init(this); // skillSelectUI의 Init 함수 호출
        menuUI = GetComponentInChildren<MenuUI>(true); // 자식 오브젝트 중 MenuUI 컴포넌트를 찾아서 menuUI에 저장
        menuUI.Init(this); // menuUI의 Init 함수 호출
        loadingUI = GetComponentInChildren<LoadingUI>(true); // 자식 오브젝트 중 LoadingUI 컴포넌트를 찾아서 loadingUI에 저장
        loadingUI.Init(this); // loadingUI의 Init 함수 호출
        ChangeState(UIState.Lobby); // UI 상태를 로비로 
    }

    public void SetActiveSkillSelect() // 스킬 선택 활성화 함수
    {
        skillSelectUI.RandomSkill();
        skillSelectUI.SetSkillText();
        skillSelectUI.SetActive(UIState.SkillSelect);
        Time.timeScale = 0;
    }

    public void SetDeactiveSkillSelect() // 스킬 선택 비활성화 함수
    {
        skillSelectUI.SetActive(UIState.GamePlay);
        Time.timeScale = 1;
    }

    public void SetActiveMenu() // 메뉴 활성화 함수
    {
        menuUI.SetActive(UIState.Menu);
        Time.timeScale = 0;
    }

    public void ChangeGold(int gold) // 골드 변경 함수
    {
        gamePlayUI.UpdateGoldText(gold);
    }

    public void ChangeEXP(float currentEXP, float maxEXP) // 경험치 변경 함수
    {
        gamePlayUI.UpdateEXPSlider(currentEXP / maxEXP);
    }

    //코루틴으로 로딩 화면을 띄우는 함수
    public IEnumerator Loading()
    {
        loadingUI.SetActive(UIState.Loading);
        yield return new WaitForSeconds(0.3f);
        loadingUI.SetActive(currentState);
    }

    public void ChangeState(UIState state) // UI 상태 변경 함수
    {
        currentState = state;
        gamePlayUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
        lobbyUI.SetActive(currentState);
        menuUI.SetActive(currentState);
        skillSelectUI.SetActive(currentState);

        if (state == UIState.GamePlay) // UI 상태가 게임플레이일 때 메뉴 버튼 활성화
        {
            gamePlayUI.SetActiveMenuButton();
        }

    }
}
