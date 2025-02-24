using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YH_TestPlayer : Player
{
    [SerializeField] private GameObject bulletPrefab { get; }

    private float checkDelay = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        UseSkills();

        if (checkDelay > 0)
        {
            checkDelay -= Time.deltaTime;
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void UseSkills()
    {
        if (PlayerSkillList == null)
        {
            Debug.LogError("PlayerSkillList가 null입니다.");
            return;
        }

        if (checkDelay <= 0)
        {
            foreach (ISkillUseDelay _shottingSkill in PlayerSkillList)
            {
                _shottingSkill.Use();
                checkDelay = atkDelay;
            }
        }
    }

    public void AddPlayerSkill(SkillBase _skill)
    {
        if (PlayerSkillList == null)
        {
            PlayerSkillList = new List<SkillBase>();
        }

        PlayerSkillList.Add(_skill);

        // 스킬 리스트에 들어갈 때 한번 OnOff되면 되는 스킬들
        if (_skill is RotationSkill)
        {
            _skill.Use();
        }
    }
}
