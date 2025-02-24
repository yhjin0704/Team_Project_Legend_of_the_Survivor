using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : BaseUI
{
    public ActorUI testActorUI;

    //�׽�Ʈ�� ��ư(���, ����ġ, �����, ��) ���� ���� �ʿ�
    public Button testGoldButton;
    public Button testExpButton;
    public Button testDamageButton;
    public Button testHealButton;
    [SerializeField] private Button menuExitButton; // �޴������� ��ư

    //�׽�Ʈ�� ����(���, ����ġ, ����� ��) ���� ���� �ʿ�
    public int testGold;
    public float testExp;
    public float testMaxExp;
    public float testMaxHP = 100;
    public float testCurrentHP = 100;
    public int testDamage = 5;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager); // ���̽�UI�� Init�� ����

        testActorUI = GameObject.FindWithTag("Player").GetComponentInChildren<ActorUI>(); // �÷��̾� �±׸� ã�Ƽ� ActorUI�� ã�Ƽ� �Ҵ�

        // �׽�Ʈ ��ư�� ã�Ƽ� �Ҵ�(���, ����ġ, �����) ���� ���� �ʿ�
        testGoldButton.onClick.AddListener(() =>
        {
            uIManager.ChangeGold(testGold);
        });
        testExpButton.onClick.AddListener(() => {
            uIManager.ChangeEXP(testExp, testMaxExp);
        });
        testDamageButton.onClick.AddListener(OnClickTestDamage);
        testHealButton.onClick.AddListener(OnClickTestHeal);
        menuExitButton.onClick.AddListener(OnClickExitButton); // �޴������� ��ư �Ҵ�
    }

    private void OnClickExitButton() // �޴������� ��ư Ŭ����
    {
        uIManager.SetDeactiveMenu();
    }

    private void OnClickTestDamage() // �׽�Ʈ ����� ��ư Ŭ����
    {
        testCurrentHP -= testDamage; // �������ŭ ����ü�� ����
        testActorUI.ChangeHPBar(testCurrentHP, testMaxHP); // ü�¹� ����
        testActorUI.ShowCombatValue(testDamage, true); // ����� �ؽ�Ʈ ���
    }

    private void OnClickTestHeal() // �׽�Ʈ �� ��ư Ŭ����
    {
        testCurrentHP += testDamage; // ����ŭ ����ü�� ����
        testActorUI.ChangeHPBar(testCurrentHP, testMaxHP); // ü�¹� ����
        testActorUI.ShowCombatValue(testDamage, false); // �� �ؽ�Ʈ ���
    }
}
