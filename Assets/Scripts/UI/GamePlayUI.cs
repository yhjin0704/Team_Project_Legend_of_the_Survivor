using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : BaseSceneUI
{
    // 골드 텍스트, 경험치 실린더, 메뉴버튼
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private Button menuButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager); // 배이스UI의 Init을 실행
        menuButton.onClick.AddListener(OnClickMenuButton); // 메뉴버튼 할당
        expSlider.value = 0; // 경험치 실린더 초기화
        goldText.text = "0"; // 골드 텍스트 초기화
    }

    public void OnClickMenuButton() // 메뉴버튼 클릭시
    {
        uiManager.SetActiveMenu();
        menuButton.gameObject.SetActive(false);
    }

    public void SetActiveMenuButton() // 메뉴버튼 활성화
    {
        menuButton.gameObject.SetActive(true);
    }

    public void UpdateEXPSlider(float percentage) // 경험치 실린더 갱신
    {
        expSlider.value = percentage;
    }

    public void UpdateGoldText(int gold) // 골드 텍스트 갱신
    {
        goldText.text = gold.ToString();
    }

    protected override UIState GetUIState() // UI상태 반환
    {
        return UIState.GamePlay;
    }
}
