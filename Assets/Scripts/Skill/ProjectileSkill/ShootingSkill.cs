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

        damage = actor.atk * skillMagnification;

        GameObject bullet = GameObject.Instantiate(actor.defaultBulletPrefab, 
            baseController.GetShotPos().position, 
            baseController.GetShotPos().rotation);
        bullet.GetComponent<Bullet>().SetDir(baseController.GetShotPos().right);
        bullet.GetComponent<Bullet>().SetDamage(damage);
    }
}

public class SpreadShotting : ShootingSkill
{
    public override void Use()
    {
        if (actor == null)
        {
            Debug.LogError("Actor가 null입니다.");
            return;
        }

        damage = actor.atk * skillMagnification;

        Transform shotPos = baseController.GetShotPos();
        Vector3 shotPosMove = shotPos.position - actor.transform.position;

        {
            Vector3 rotatedMove = Quaternion.Euler(0, 0, 45f) * shotPosMove;
            float angle = Mathf.Atan2(rotatedMove.y, rotatedMove.x) * Mathf.Rad2Deg;

            GameObject bullet = GameObject.Instantiate(actor.defaultBulletPrefab,
                actor.transform.position + rotatedMove,
                Quaternion.Euler(0, 0, angle));

            bullet.GetComponent<Bullet>().SetDir(rotatedMove);
            bullet.GetComponent<Bullet>().SetDamage(damage);
        }
        {
            Vector3 rotatedMove = Quaternion.Euler(0, 0, -45f) * shotPosMove;
            float angle = Mathf.Atan2(rotatedMove.y, rotatedMove.x) * Mathf.Rad2Deg;

            GameObject bullet = GameObject.Instantiate(actor.defaultBulletPrefab,
                actor.transform.position + rotatedMove,
                Quaternion.Euler(0, 0, angle));

            bullet.GetComponent<Bullet>().SetDir(rotatedMove);
            bullet.GetComponent<Bullet>().SetDamage(damage);
        }
    }
}

public class SideShotting : ShootingSkill
{
    public override void Use()
    {
        if (actor == null)
        {
            Debug.LogError("Actor가 null입니다.");
            return;
        }

        damage = actor.atk * skillMagnification;

        Transform shotPos = baseController.GetShotPos();
        Vector3 shotPosMove = shotPos.position - actor.transform.position;

        {
            Vector3 rotatedMove = Quaternion.Euler(0, 0, 90f) * shotPosMove;
            float angle = Mathf.Atan2(rotatedMove.y, rotatedMove.x) * Mathf.Rad2Deg;

            GameObject bullet = GameObject.Instantiate(actor.defaultBulletPrefab,
                actor.transform.position + rotatedMove,
                Quaternion.Euler(0, 0, angle));

            bullet.GetComponent<Bullet>().SetDir(rotatedMove);
            bullet.GetComponent<Bullet>().SetDamage(damage);
        }
        {
            Vector3 rotatedMove = Quaternion.Euler(0, 0, -90f) * shotPosMove;
            float angle = Mathf.Atan2(rotatedMove.y, rotatedMove.x) * Mathf.Rad2Deg;

            GameObject bullet = GameObject.Instantiate(actor.defaultBulletPrefab,
                actor.transform.position + rotatedMove,
                Quaternion.Euler(0, 0, angle));

            bullet.GetComponent<Bullet>().SetDir(rotatedMove);
            bullet.GetComponent<Bullet>().SetDamage(damage);
        }
    }
}

public class BackShotting : ShootingSkill
{
    public override void Use()
    {
        if (actor == null)
        {
            Debug.LogError("Actor가 null입니다.");
            return;
        }

        damage = actor.atk * skillMagnification;

        Transform shotPos = baseController.GetShotPos();
        Vector3 shotPosMove = shotPos.position - actor.transform.position;

        {
            Vector3 rotatedMove = Quaternion.Euler(0, 0, 180f) * shotPosMove;
            float angle = Mathf.Atan2(rotatedMove.y, rotatedMove.x) * Mathf.Rad2Deg;

            GameObject bullet = GameObject.Instantiate(actor.defaultBulletPrefab,
                actor.transform.position + rotatedMove,
                Quaternion.Euler(0, 0, angle));

            bullet.GetComponent<Bullet>().SetDir(rotatedMove);
            bullet.GetComponent<Bullet>().SetDamage(damage);
        }
    }
}