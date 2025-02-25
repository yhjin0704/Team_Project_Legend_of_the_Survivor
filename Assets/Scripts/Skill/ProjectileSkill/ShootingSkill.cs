using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSkill : ProjectileSkill, ISkillUseDelay
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

public class StraightShotting : ShootingSkill
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

        bullet = Instantiate(actor.defaultBulletPrefab, 
            baseController.GetShotPos().position, 
            baseController.GetShotPos().rotation);
        bullet.GetComponent<Bullet>().SetDir(baseController.GetShotPos().right);
    }
}

public class SpreadShotting : ShootingSkill
{
    //protected override void Start()
    //{
    //    base.Start();
    //}
    //public override void Use()
    //{
    //    if (actor == null)
    //    {
    //        Debug.LogError("Actor가 null입니다.");
    //        return;
    //    }
    //    bullet = Instantiate(actor.defaultBulletPrefab, actor.GetShotPos().position, actor.GetShotPos().rotation);
    //    bullet.GetComponent<Bullet>().SetDir(actor.GetShotPos().right);
    //    bullet = Instantiate(actor.defaultBulletPrefab, actor.GetShotPos().position, actor.GetShotPos().rotation);
    //    //bullet.GetComponent<Bullet>().SetDir(actor.GetShotPos().right + actor.GetShotPos().up);
    //    bullet = Instantiate(actor.defaultBulletPrefab, actor.GetShotPos().position, actor.GetShotPos().rotation);
    //    //bullet.GetComponent<Bullet>().SetDir(actor.GetShotPos().right - actor.GetShotPos().up);
    //}
}
