using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState // UI상태를 나타내는 열거형
{
    Lobby,
    GamePlay,
    GameOver
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

        /* 
         gameOverUI = sceneUIGameObject.GetComponentInChildren<GameOverUI>(true);
         gameOverUI.Init(this);
         lobbyUI = sceneUIGameObject.GetComponentInChildren<LobbyUI>(true);
         lobbyUI.Init(this);
        // 테스트 씬에서 존재 하지 않아서 잠시 주석 처리
         */

        skillSelectUI = sceneUIGameObject.GetComponentInChildren<SkillSelectUI>(true); // sceneUIGameObject의 자식 오브젝트 중 SkillSelectUI 컴포넌트를 찾아서 skillSelectUI에 저장
        skillSelectUI.Init(this); // skillSelectUI의 Init 함수 호출
        menuUI = sceneUIGameObject.GetComponentInChildren<MenuUI>(true); // sceneUIGameObject의 자식 오브젝트 중 MenuUI 컴포넌트를 찾아서 menuUI에 저장
        menuUI.Init(this); // menuUI의 Init 함수 호출
    }

    public void SetActiveSkillSelect(int[] skills) // 스킬 선택 활성화 함수
    {
        skillSelectUI.RandomSkill(skills);
        skillSelectUI.gameObject.SetActive(true);
    }

    public void SetDeactiveSkillSelect() // 스킬 선택 비활성화 함수
    {
        skillSelectUI.gameObject.SetActive(false);
    }

    public void SetActiveMenu() // 메뉴 활성화 함수
    {
        menuUI.gameObject.SetActive(true);
    }

    public void SetDeactiveMenu() // 메뉴 비활성화 함수
    {
        menuUI.gameObject.SetActive(false);
        gamePlayUI.SetActiveMenuButton();
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
    }
}
