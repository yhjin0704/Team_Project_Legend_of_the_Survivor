using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public int level = 1;

    protected override void Awake()
    {
        base.Awake();
        PlayerSkillList = new List<SkillBase>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

