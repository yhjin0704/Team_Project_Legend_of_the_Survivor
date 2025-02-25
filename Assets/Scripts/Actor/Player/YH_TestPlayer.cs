using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        if (isMove == false)
        {
            Attak();
        }

        if (checkDelay > 0.5f && isMove == true)
        {
            checkDelay -= Time.deltaTime;
        }
        else if (checkDelay > 0.0f && isMove == false)
        {
            checkDelay -= Time.deltaTime;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Attak()
    {
        base.Attak();

        UseSkills();

       
    }

    protected void UseSkills()
    {
        if (skillManager.GetPlayerSkillList() == null)
        {
            Debug.LogError("PlayerSkillList가 null입니다.");
            return;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Archer_Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            animator.SetBool("IsAttack", false);
        }

        if (checkDelay <= 0)
        {
            checkDelay = atkDelay;
            if (shotPos.transform.rotation.z >= -0.9f && shotPos.transform.rotation.z < 0.9f)
            {
                _renderer.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                _renderer.transform.localScale = new Vector3(-1, 1, 1);
            }
            animator.SetBool("IsAttack", true);

            foreach (ISkillUseDelay _shottingSkill in skillManager.GetPlayerSkillList())
            {
                _shottingSkill.Use();
            }
        }
    }
}
