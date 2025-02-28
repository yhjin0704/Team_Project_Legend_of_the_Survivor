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

    private const int MAXSKILLCOUNT = 9;
    GameManager gameManager;
    int[] selectSkillIndexs;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        gameManager = GameManager.Instance;
        selectSkillIndexs = new int[3];
        skillButton1.onClick.AddListener(OnSelectSkillButton1);
        skillButton2.onClick.AddListener(OnSelectSkillButton2);
        skillButton3.onClick.AddListener(OnSelectSkillButto3);
    }

    public void OnSelectSkillButton1()
    {
        gameManager.SelectSkill(selectSkillIndexs[0]);
        uiManager.SetDeactiveSkillSelect();
    }
    public void OnSelectSkillButton2()
    {
        gameManager.SelectSkill(selectSkillIndexs[1]);
        uiManager.SetDeactiveSkillSelect();
    }
    public void OnSelectSkillButto3()
    {
        gameManager.SelectSkill(selectSkillIndexs[2]);
        uiManager.SetDeactiveSkillSelect();
    }

    public void SetSkillText()
    {
        skillName1.text = SkillText(selectSkillIndexs[0]);
        skillName2.text = SkillText(selectSkillIndexs[1]);
        skillName3.text = SkillText(selectSkillIndexs[2]);
    }

    public string SkillText(int selectNum)
    {
        switch (selectNum)
        {
            case 0:
                return "MaxHP  +30";
            case 1:
                return "ATK  +5";
            case 2:
                return "ATKDelay  -0.1";
            case 3:
                return "Speed  +1";
            case 4:
                return "Heal  +50";
            case 5:
                if (!gameManager.isSpreadShotting)
                {
                    return "SpreadShotting";
                }
                else if (!gameManager.isSideShotting)
                {
                    return "SideShotting";
                }
                else if (!gameManager.isBackShotting)
                {
                    return "BackShotting";
                }
                else
                {
                    return "DoubleShotting";
                }
            case 6:
                if (!gameManager.isSideShotting)
                {
                    return "SideShotting";
                }
                else if (!gameManager.isBackShotting)
                {
                    return "BackShotting";
                }
                else
                {
                    return "DoubleShotting";
                }
            case 7:
                if (!gameManager.isBackShotting)
                {
                    return "BackShotting";
                }
                else
                {
                    return "DoubleShotting";
                }
            case 8:
                return "DoubleShotting";
            default:
                return "error";
        }
    }

    public void RandomSkill()
    {
        int skillCount = gameManager.PlayerSkillManagerProperty.GetSkillList().Count;
        if (gameManager.PlayerControllerProperty.isDoubleShot)
        {
            skillCount++;
        }
        for (int count  = 0; count < 3;)
        {
            int index = Random.Range(0, MAXSKILLCOUNT - (skillCount - 1)); // 0~4 사이의 랜덤 인덱스 생성

            // 중복 체크
            bool isDuplicate = false;
            for (int i = 0; i < count; i++)
            {
                if (selectSkillIndexs[i] == index)
                {
                    isDuplicate = true;
                    break;
                }
            }

            // 중복이 없으면 배열에 추가
            if (!isDuplicate)
            {
                selectSkillIndexs[count] = index;
                count++;
            }
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.SkillSelect;
    }
}
