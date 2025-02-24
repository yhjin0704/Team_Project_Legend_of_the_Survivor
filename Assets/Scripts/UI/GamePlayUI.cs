using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : BaseSceneUI
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private Button menuButton;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);
        menuButton.onClick.AddListener(OnClickMenuButton);
    }

    public void OnClickMenuButton()
    {
        uIManager.SetActiveMenu();
        menuButton.gameObject.SetActive(false);
    }

    public void SetActiveMenuButton()
    {
        menuButton.gameObject.SetActive(true);
    }

    public void UpdateEXPSlider(float percentage)
    {
        expSlider.value = percentage;
    }

    public void UpdateGoldText(int gold)
    {
        goldText.text = gold.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.GamePlay;
    }
}
