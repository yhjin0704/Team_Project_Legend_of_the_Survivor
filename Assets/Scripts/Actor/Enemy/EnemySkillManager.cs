using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillManager : SkillManager
{
    protected override void Awake()
    {
        base.Awake();

        AddSkill(new StraightShotting());
        AddSkill(new MultipleShotting());
        AddSkill(new ArcShotting());
        AddSkill(new AroundShotting());
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
}
