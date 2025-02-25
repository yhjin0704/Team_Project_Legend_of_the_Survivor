using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseSceneUI
{
    // �÷��� ��ư, ���� ��ư
    [SerializeField] private Button playButton;
    [SerializeField] private Button ExitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        playButton.onClick.AddListener(OnClickPlayButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickPlayButton() // �÷��� ��ư Ŭ�� ��
    {
        GameManager.Instance.ChangeScene(SceneState.Main);
        uiManager.ChangeState(UIState.GamePlay);
        uiManager.StartCoroutine(uiManager.Loading()); // �ε� �ڷ�ƾ ����
    }

    public void OnClickExitButton()
    {

    }

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
}
