using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShottingSkill : ProjectileSkill
{
    Vector2 shotDir;
    // Start is called before the first frame update
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

public class StraightShotting : ShottingSkill
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
}
