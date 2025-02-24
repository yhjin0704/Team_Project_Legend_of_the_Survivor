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
            Debug.LogError("PlayerSkillList�� null�Դϴ�.");
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

        // ��ų ����Ʈ�� �� �� �ѹ� OnOff�Ǹ� �Ǵ� ��ų��
        if (_skill is RotationSkill)
        {
            _skill.Use();
        }
    }
}
