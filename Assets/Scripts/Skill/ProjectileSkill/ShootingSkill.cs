using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSkill : ProjectileSkill, ISkillUseDelay
{
    public bool isFinish = true;

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
        isFinish = false;

        damage = actor.atk * skillMagnification;

        GameObject bullet = GameObject.Instantiate(actor.defaultBulletPrefab,
            baseController.GetShotPos().position,
            baseController.GetShotPos().rotation);
        bullet.GetComponent<Bullet>().SetDir(baseController.GetShotPos().right);
        bullet.GetComponent<Bullet>().SetDamage(damage);

        isFinish = true;
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
        isFinish = false;

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
        isFinish = true;
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
        isFinish = false;

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
        isFinish = true;
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
        isFinish = false;

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
        isFinish = true;
    }
}

public class MultipleShotting : ShootingSkill
{
    public override void Use()
    {
        if (actor == null)
        {
            Debug.LogError("Actor가 null입니다.");
            return;
        }
        isFinish = false;

        damage = actor.atk * skillMagnification;

        Transform shotPos = baseController.GetShotPos();
        Vector3 shotPosMove = shotPos.position - actor.transform.position;

        for (int i = 0; i < 5; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            Vector3 spawnPos = actor.transform.position + randomOffset;
            float angle = Mathf.Atan2(shotPosMove.y, shotPosMove.x) * Mathf.Rad2Deg;

            GameObject bullet = GameObject.Instantiate(actor.defaultBulletPrefab,
                spawnPos,
                Quaternion.Euler(0, 0, angle));

            bullet.GetComponent<Bullet>().SetDir(baseController.GetShotPos().right);
            bullet.GetComponent<Bullet>().SetDamage(damage);
        }
        isFinish = true;
    }
}

public class ArcShotting : ShootingSkill
{

    int curPatternCount = 0;
    int maxPatternCount = 50;

    public override void Use()
    {
        if (actor == null)
        {
            Debug.LogError("Actor가 null입니다.");
            return;
        }
        isFinish = false;

        damage = actor.atk * skillMagnification;

        Transform shotPos = baseController.GetShotPos();
        Vector3 shotPosMove = shotPos.position - actor.transform.position;

        if (actor.IsAlive && curPatternCount < maxPatternCount)
        {
            Vector3 rotatedMove = new Vector2(Mathf.Cos(Mathf.PI * 2 * curPatternCount / maxPatternCount),
                Mathf.Sin(Mathf.PI * 2 * curPatternCount / maxPatternCount));
            float angle = Mathf.Atan2(rotatedMove.y, rotatedMove.x) * Mathf.Rad2Deg;

            GameObject bullet = GameObject.Instantiate(actor.defaultBulletPrefab,
                actor.transform.position,
                Quaternion.Euler(0, 0, angle));

            bullet.GetComponent<Bullet>().SetDir(rotatedMove);
            bullet.GetComponent<Bullet>().SetDamage(damage);

            curPatternCount++;

            CoroutineRunner.Run(UseArcShot(0.15f));
        }
        else
        {
            curPatternCount = 0;
            isFinish = true;
        }
    }

    IEnumerator UseArcShot(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        Use();
    }
}

public class AroundShotting : ShootingSkill
{
    int roundNum = 40;

    public override void Use()
    {
        if (actor == null)
        {
            Debug.LogError("Actor가 null입니다.");
            return;
        }
        isFinish = false;

        damage = actor.atk * skillMagnification;

        for (int i = 0; i < roundNum; i++)
        {
            Vector3 rotatedMove = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNum), Mathf.Sin(Mathf.PI * 2 * i / roundNum));
            float angle = (360f * i / roundNum) + 90f;

            GameObject bullet = GameObject.Instantiate(actor.defaultBulletPrefab,
                actor.transform.position,
                Quaternion.Euler(0, 0, angle));

            bullet.GetComponent<Bullet>().SetDir(rotatedMove);
            bullet.GetComponent<Bullet>().SetDamage(damage);
        }
        isFinish = true;
    }
}

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;
    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                // 새로운 게임 오브젝트를 만들어 인스턴스 할당
                GameObject obj = new GameObject("CoroutineRunner");
                _instance = obj.AddComponent<CoroutineRunner>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public static void Run(IEnumerator _coroutine)
    {
        Instance.StartCoroutine(_coroutine);
    }
}