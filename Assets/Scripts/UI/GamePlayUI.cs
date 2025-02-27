using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : BaseSceneUI
{
    // ��� �ؽ�Ʈ, ����ġ �Ǹ���, �޴���ư
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private Button menuButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager); // ���̽�UI�� Init�� ����
        menuButton.onClick.AddListener(OnClickMenuButton); // �޴���ư �Ҵ�
        expSlider.value = 0; // ����ġ �Ǹ��� �ʱ�ȭ
        goldText.text = "0"; // ��� �ؽ�Ʈ �ʱ�ȭ
    }

    public void OnClickMenuButton() // �޴���ư Ŭ����
    {
        uiManager.SetActiveMenu();
        menuButton.gameObject.SetActive(false);
    }

    public void SetActiveMenuButton() // �޴���ư Ȱ��ȭ
    {
        menuButton.gameObject.SetActive(true);
    }

    public void UpdateEXPSlider(float percentage) // ����ġ �Ǹ��� ����
    {
        expSlider.value = percentage;
    }

    public void UpdateGoldText(int gold) // ��� �ؽ�Ʈ ����
    {
        goldText.text = gold.ToString();
    }

    protected override UIState GetUIState() // UI���� ��ȯ
    {
        return UIState.GamePlay;
    }
}
