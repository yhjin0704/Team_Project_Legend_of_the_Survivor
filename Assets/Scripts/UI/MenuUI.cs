using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : BaseUI
{
    public Button testGoldButton;
    public Button testExpButton;
    [SerializeField] private Button menuExitButton;

    public int testGold;
    public float testExp;
    public float testMaxExp;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager);

        testGoldButton.onClick.AddListener(() =>
        {
            uIManager.ChangeGold(testGold);
        });
        testExpButton.onClick.AddListener(() => {
            uIManager.ChangeEXP(testExp, testMaxExp);
        });
        menuExitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickExitButton()
    {
        uIManager.SetDeactiveMenu();
    }
}
