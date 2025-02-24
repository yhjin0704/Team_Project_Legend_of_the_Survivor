using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : BaseUI
{
    public ActorUI testActorUI;

    //테스트용 버튼(골드, 경험치, 대미지, 힐) 추후 삭제 필요
    public Button testGoldButton;
    public Button testExpButton;
    public Button testDamageButton;
    public Button testHealButton;
    [SerializeField] private Button menuExitButton; // 메뉴나가기 버튼

    //테스트용 변수(골드, 경험치, 대미지 등) 추후 삭제 필요
    public int testGold;
    public float testExp;
    public float testMaxExp;
    public float testMaxHP = 100;
    public float testCurrentHP = 100;
    public int testDamage = 5;

    public override void Init(UIManager uIManager)
    {
        base.Init(uIManager); // 배이스UI의 Init을 실행

        testActorUI = GameObject.FindWithTag("Player").GetComponentInChildren<ActorUI>(); // 플레이어 태그를 찾아서 ActorUI를 찾아서 할당

        // 테스트 버튼을 찾아서 할당(골드, 경험치, 대미지) 추후 삭제 필요
        testGoldButton.onClick.AddListener(() =>
        {
            uIManager.ChangeGold(testGold);
        });
        testExpButton.onClick.AddListener(() => {
            uIManager.ChangeEXP(testExp, testMaxExp);
        });
        testDamageButton.onClick.AddListener(OnClickTestDamage);
        testHealButton.onClick.AddListener(OnClickTestHeal);
        menuExitButton.onClick.AddListener(OnClickExitButton); // 메뉴나가기 버튼 할당
    }

    private void OnClickExitButton() // 메뉴나가기 버튼 클릭시
    {
        uIManager.SetDeactiveMenu();
    }

    private void OnClickTestDamage() // 테스트 대미지 버튼 클릭시
    {
        testCurrentHP -= testDamage; // 대미지만큼 현재체력 감소
        testActorUI.ChangeHPBar(testCurrentHP, testMaxHP); // 체력바 갱신
        testActorUI.ShowCombatValue(testDamage, true); // 대미지 텍스트 출력
    }

    private void OnClickTestHeal() // 테스트 힐 버튼 클릭시
    {
        testCurrentHP += testDamage; // 힐만큼 현재체력 증가
        testActorUI.ChangeHPBar(testCurrentHP, testMaxHP); // 체력바 갱신
        testActorUI.ShowCombatValue(testDamage, false); // 힐 텍스트 출력
    }
}
