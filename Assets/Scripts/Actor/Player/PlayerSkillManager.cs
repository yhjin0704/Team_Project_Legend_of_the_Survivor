using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : SkillManager
{
<<<<<<< HEAD
=======
    Player player;
    PlayerController playerController;

>>>>>>> dev
    protected override void Awake()
    {
        base.Awake();

<<<<<<< HEAD
        StraightShotting straightShotting = new StraightShotting();
        AddSkill(straightShotting);
=======
        player = actor as Player;
        playerController = baseController as PlayerController;

        AddSkill(new StraightShotting());
>>>>>>> dev
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

<<<<<<< HEAD
=======
    public void IncreaseHp(float _IncreaseValue)
    {
        player.SetMaxHp(player.GetMaxHp() + _IncreaseValue);
        player.hp += _IncreaseValue;
    }

    public void IncreaseAtk(float _IncreaseValue)
    {
        player.atk += _IncreaseValue;
    }

    public void ReduceAtkDelay(float _ReduceValue)// 공속 : 비율로 감소시킬 예정 1.0f 이하로 입력해야함
    {
        if (_ReduceValue > 1.0f)
        {
            Debug.Log("ReduceAtkDelay : 1.0f 이하로 입력해야함");
            return;
        }
        _ReduceValue = 1.0f - _ReduceValue;

        player.atkDelay *= _ReduceValue;
    }

    public void IncreaseSpeed(float _IncreaseValue)
    {
        player.speed += _IncreaseValue;
    }

    public void SelectHeal(float _heal)
    {
        playerController.Healed(_heal);
    }

    public void OnDoubleShotAblilty()
    {
        playerController.isDoubleShot = true;
    }
>>>>>>> dev
}
