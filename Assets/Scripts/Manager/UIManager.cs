using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    GameObject sceneUIGameObject;
    GamePlayUI gamePlayUI;
    GameOverUI gameOverUI;
    LobbyUI lobbyUI;
    MenuUI menuUI;
    SkillSelectUI skillSelectUI;

    // 현재 UI 상태를 저장할 변수 선언
    private UIState currentState;

    private void Awake() // 초기화 함수
    {
        sceneUIGameObject = GameObject.FindWithTag("SceneUI"); // SceneUI 태그를 가진 오브젝트를 찾아서 sceneUIGameObject에 저장

        gamePlayUI = sceneUIGameObject.GetComponentInChildren<GamePlayUI>(true); // sceneUIGameObject의 자식 오브젝트 중 GamePlayUI 컴포넌트를 찾아서 gamePlayUI에 저장
        gamePlayUI.Init(this); // gamePlayUI의 Init 함수 호출
        gameOverUI = sceneUIGameObject.GetComponentInChildren<GameOverUI>(true); // sceneUIGameObject의 자식 오브젝트 중 GameOverUI 컴포넌트를 찾아서 gameOverUI에 저장
        gameOverUI.Init(this); // gameOverUI의 Init 함수 호출
        lobbyUI = sceneUIGameObject.GetComponentInChildren<LobbyUI>(true); // sceneUIGameObject의 자식 오브젝트 중 LobbyUI 컴포넌트를 찾아서 lobbyUI에 저장
        lobbyUI.Init(this); // lobbyUI의 Init 함수 호출
        skillSelectUI = sceneUIGameObject.GetComponentInChildren<SkillSelectUI>(true); // sceneUIGameObject의 자식 오브젝트 중 SkillSelectUI 컴포넌트를 찾아서 skillSelectUI에 저장
        skillSelectUI.Init(this); // skillSelectUI의 Init 함수 호출
        menuUI = sceneUIGameObject.GetComponentInChildren<MenuUI>(true); // sceneUIGameObject의 자식 오브젝트 중 MenuUI 컴포넌트를 찾아서 menuUI에 저장
        menuUI.Init(this); // menuUI의 Init 함수 호출

        ChangeState(UIState.Lobby); // UI 상태를 로비로 변경
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

        if (state == UIState.GamePlay)//
        {
            gamePlayUI.SetActiveMenuButton();
        }

    }
}
