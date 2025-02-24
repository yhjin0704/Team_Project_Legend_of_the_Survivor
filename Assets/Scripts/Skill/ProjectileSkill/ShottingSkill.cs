using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShottingSkill : ProjectileSkill, ISkillUseDelay
{
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

    public override void Use()
    {
        if (actor == null)
        {
            Debug.LogError("Actor가 null입니다.");
            return;
        }

        bullet = Instantiate(actor.defaultBulletPrefab, actor.transform.position, actor.transform.rotation);

    }
}
