using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIState // UI상태를 나타내는 열거형
{
    Lobby,
    GamePlay,
    GameOver,
    Menu,
    SkillSelect
}

public class UIManager : MonoBehaviour // UI를 관리하는 클래스
{

    // UI를 저장할 변수 선언
    [SerializeField] private GameObject sceneUIGameObject;
    [SerializeField] private GamePlayUI gamePlayUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private LobbyUI lobbyUI;
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private SkillSelectUI skillSelectUI;


    // 현재 UI 상태를 저장할 변수 선언
    private UIState currentState = UIState.Lobby;

    private void Awake() // 초기화 함수
    {
        sceneUIGameObject = GameObject.FindWithTag("SceneUI"); // SceneUI 태그를 가진 오브젝트를 찾아서 sceneUIGameObject에 저장
        gamePlayUI = GetComponentInChildren<GamePlayUI>(true); // sceneUIGameObject의 자식 오브젝트 중 GamePlayUI 컴포넌트를 찾아서 gamePlayUI에 저장
        gamePlayUI.Init(this); // gamePlayUI의 Init 함수 호출
        gameOverUI = GetComponentInChildren<GameOverUI>(true); // sceneUIGameObject의 자식 오브젝트 중 GameOverUI 컴포넌트를 찾아서 gameOverUI에 저장
        gameOverUI.Init(this); // gameOverUI의 Init 함수 호출
        lobbyUI = GetComponentInChildren<LobbyUI>(true); // sceneUIGameObject의 자식 오브젝트 중 LobbyUI 컴포넌트를 찾아서 lobbyUI에 저장
        lobbyUI.Init(this); // lobbyUI의 Init 함수 호출
        skillSelectUI = GetComponentInChildren<SkillSelectUI>(true); // sceneUIGameObject의 자식 오브젝트 중 SkillSelectUI 컴포넌트를 찾아서 skillSelectUI에 저장
        skillSelectUI.Init(this); // skillSelectUI의 Init 함수 호출
        menuUI = GetComponentInChildren<MenuUI>(true); // sceneUIGameObject의 자식 오브젝트 중 MenuUI 컴포넌트를 찾아서 menuUI에 저장
        menuUI.Init(this); // menuUI의 Init 함수 호출
    }

    private void Start() // 시작 함수
    {
        switch (GameManager.Instance.GetCurrentSceneState())
        {
            case SceneState.Lobby: // 현재 씬 상태가 로비일 때
                ChangeState(UIState.Lobby); // UI 상태를 로비로 변경
                break;
            case SceneState.Main: // 현재 씬 상태가 메인일 때
                ChangeState(UIState.GamePlay); // UI 상태를 게임플레이로 변경
                break;
        }
    }

    public void SetActiveSkillSelect(int[] skills) // 스킬 선택 활성화 함수
    {
        skillSelectUI.RandomSkill(skills);
        skillSelectUI.SetActive(UIState.SkillSelect);
    }

    public void SetDeactiveSkillSelect() // 스킬 선택 비활성화 함수
    {
        skillSelectUI.SetActive(UIState.GamePlay);
    }

    public void SetActiveMenu() // 메뉴 활성화 함수
    {
        menuUI.SetActive(UIState.Menu);
    }

    public void ChangeGold(int gold) // 골드 변경 함수
    {
        gamePlayUI.UpdateGoldText(gold);
    }

    public void ChangeEXP(float currentEXP, float maxEXP) // 경험치 변경 함수
    {
        gamePlayUI.UpdateEXPSlider(currentEXP / maxEXP);
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
