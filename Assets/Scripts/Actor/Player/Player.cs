using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public float atk;
    public float atkDelay;

    private List<SkillBase> PlayerSkillList;

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
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
