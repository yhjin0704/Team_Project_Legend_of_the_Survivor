using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : SkillManager
{
    protected override void Awake()
    {
        base.Awake();

        StraightShotting straightShotting = new StraightShotting();
        AddSkill(straightShotting);
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void IncreaseHp(float _IncreaseValue)
    {
        actor.SetMaxHp(actor.GetMaxHp() + _IncreaseValue);
        actor.hp += _IncreaseValue;
    }

    public void IncreaseAtk(float _IncreaseValue)
    {
        actor.atk += _IncreaseValue;
    }

    public void ReduceAtkDelay(float _ReduceValue)// 공속 : 비율로 감소시킬 예정 1.0f 이하로 입력해야함
    {
        if (_ReduceValue > 1.0f)
        {
            Debug.Log("ReduceAtkDelay : 1.0f 이하로 입력해야함");
            return;
        }
        _ReduceValue = 1.0f - _ReduceValue;

        actor.atkDelay *= _ReduceValue;
    }

    public void IncreaseSpeed(float _IncreaseValue)
    {
        actor.speed += _IncreaseValue;
    }
}
