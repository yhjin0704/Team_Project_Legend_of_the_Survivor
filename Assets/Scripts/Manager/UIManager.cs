using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Lobby,
    GamePlay,
    GameOver
}

public class UIManager : MonoBehaviour
{
    GameObject sceneUIGameObject;
    GamePlayUI gamePlayUI;
    GameOverUI gameOverUI;
    LobbyUI lobbyUI;
    MenuUI menuUI;

    private UIState currentState;

    private void Awake()
    {
        sceneUIGameObject = GameObject.FindWithTag("SceneUI");

        gamePlayUI = sceneUIGameObject.GetComponentInChildren<GamePlayUI>(true);
        gamePlayUI.Init(this);
        gameOverUI = sceneUIGameObject.GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);
        lobbyUI = sceneUIGameObject.GetComponentInChildren<LobbyUI>(true);
        lobbyUI.Init(this);
        menuUI = sceneUIGameObject.GetComponentInChildren<MenuUI>(true);
        menuUI.Init(this);
    }

    public void SetActiveMenu()
    {
        menuUI.gameObject.SetActive(true);
    }

    public void SetDeactiveMenu()
    {
        menuUI.gameObject.SetActive(false);
        gamePlayUI.SetActiveMenuButton();
    }

    public void ChangeGold(int gold)
    {
        gamePlayUI.UpdateGoldText(gold);
    }

    public void ChangeEXP(float currentEXP, float maxEXP)
    {
        gamePlayUI.UpdateEXPSlider(currentEXP / maxEXP);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        gamePlayUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
        lobbyUI.SetActive(currentState);
    }
}
