using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSkill : ProjectileSkill, ISkillUseDelay
{

}

public class StraightShotting : ShootingSkill
{
    public override void Use()
    {
        if (actor == null)
        {
            Debug.LogError("Actor가 null입니다.");
            return;
        }

        bullet = GameObject.Instantiate(actor.defaultBulletPrefab, 
            baseController.GetShotPos().position, 
            baseController.GetShotPos().rotation);
        bullet.GetComponent<Bullet>().SetDir(baseController.GetShotPos().right);
    }
}

public class SpreadShotting : ShootingSkill
{
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
