using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YH_TestPlayer : Player
{
    protected float checkDelay = 0;

    protected SkillManager skillManager;

    protected override void Awake()
    {
        base.Awake();

        skillManager = GetComponent<SkillManager>();
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();

        Attak();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Attak()
    {
        base.Attak();

        UseSkills();

        if (checkDelay > 0)
        {
            checkDelay -= Time.deltaTime;
        }
    }

    protected void UseSkills()
    {
        if (skillManager.GetPlayerSkillList() == null)
        {
            Debug.LogError("PlayerSkillList�� null�Դϴ�.");
            return;
        }

        if (checkDelay <= 0)
        {
            foreach (ISkillUseDelay _shottingSkill in skillManager.GetPlayerSkillList())
            {
                _shottingSkill.Use();
                checkDelay = atkDelay;
            }
        }
    }
}
