using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : BaseSceneUI
{
    [SerializeField] private Button skillButton1;
    [SerializeField] private Button skillButton2;
    [SerializeField] private Button skillButton3;
    [SerializeField] private TextMeshProUGUI skillName1;
    [SerializeField] private TextMeshProUGUI skillName2;
    [SerializeField] private TextMeshProUGUI skillName3;


    GameManager gameManager;
    int[] testSkillIndexs;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        gameManager = GameManager.Instance;
        testSkillIndexs = new int[3];
        skillButton1.onClick.AddListener(OnSelectSkillButton1);
        skillButton2.onClick.AddListener(OnSelectSkillButton2);
        skillButton3.onClick.AddListener(OnSelectSkillButto3);
    }

    public void OnSelectSkillButton1()
    {
        gameManager.SelectSkill(testSkillIndexs[0]);
        uiManager.SetDeactiveSkillSelect();
    }
    public void OnSelectSkillButton2()
    {
        gameManager.SelectSkill(testSkillIndexs[1]);
        uiManager.SetDeactiveSkillSelect();
    }
    public void OnSelectSkillButto3()
    {
        gameManager.SelectSkill(testSkillIndexs[2]);
        uiManager.SetDeactiveSkillSelect();
    }

    public void SetSkillText()
    {
        skillName1.text = SkillText(testSkillIndexs[0]);
        skillName2.text = SkillText(testSkillIndexs[1]);
        skillName3.text = SkillText(testSkillIndexs[2]);
    }

    public string SkillText(int selectNum)
    {
        switch (selectNum)
        {
            case 0:
                return "MaxHP 30 increase";
                break;
            case 1:
                return "ATK 5 increase";
                break;
            case 2:
                return "ATKDelay 0.1 decrease";
                break;
            case 3:
                return "speed 1 increase";
                break;
            case 4:
                return "50 heal";
                break;
            default:
                return "Error";
        }
    }

    public void RandomSkill()
    {
        for (int count  = 0; count < 3;)
        {
            int index = Random.Range(0, 5); // 0~4 사이의 랜덤 인덱스 생성

            // 중복 체크
            bool isDuplicate = false;
            for (int i = 0; i < count; i++)
            {
                if (testSkillIndexs[i] == index)
                {
                    isDuplicate = true;
                    break;
                }
            }

            // 중복이 없으면 배열에 추가
            if (!isDuplicate)
            {
                testSkillIndexs[count] = index;
                count++;
            }
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.SkillSelect;
    }
}
