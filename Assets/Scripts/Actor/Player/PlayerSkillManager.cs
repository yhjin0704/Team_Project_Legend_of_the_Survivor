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

}
