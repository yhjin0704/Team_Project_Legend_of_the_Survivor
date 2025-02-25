using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : BaseSceneUI
{
    [SerializeField] private Button skillButton1;
    [SerializeField] private Button skillButton2;
    [SerializeField] private Button skillButton3;
    int[] testSkillIndexs;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        skillButton1.onClick.AddListener(() =>
        {
            Debug.Log(testSkillIndexs[0] + "test");
            uiManager.SetDeactiveSkillSelect();
        });
        skillButton2.onClick.AddListener(() =>
        {
            Debug.Log(testSkillIndexs[1] + "test");
            uiManager.SetDeactiveSkillSelect();
        });
        skillButton3.onClick.AddListener(() => 
        {
            Debug.Log(testSkillIndexs[2] + "test"); 
            uiManager.SetDeactiveSkillSelect(); 
        });
        testSkillIndexs = new int[3];
    }

    public void OnClickSkillButton()
    {
        uiManager.SetDeactiveSkillSelect();
    }

    public void RandomSkill(int[] testSkillIndexs)
    {
        int random1, random2;
        int temp;

        for (int i = 0; i < testSkillIndexs.Length; ++i)
        {
            random1 = Random.Range(0, testSkillIndexs.Length);
            random2 = Random.Range(0, testSkillIndexs.Length);

            temp = testSkillIndexs[random1];
            testSkillIndexs[random1] = testSkillIndexs[random2];
            testSkillIndexs[random2] = temp;
        }
        for (int i = 0; i < 3; i++)
        {
            this.testSkillIndexs[i] = testSkillIndexs[i];
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.SkillSelect;
    }
}
