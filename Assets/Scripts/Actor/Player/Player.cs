using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public int level = 1;
    public int maxExp;
    public int exp = 0;
    public int gold = 0;

    Transform takeItemRange;

    protected override void Awake()
    {
        base.Awake();
        if (FindObjectsOfType<Player>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        maxExp = (level * 2) + 8;
        takeItemRange = transform.Find("TakeItemRange");

        DontDestroyOnLoad(gameObject);
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

    public void CheckLevelUp()
    {
        if (exp >= maxExp)
        {
            SoundManager.instance.PlaySfx(SoundManager.Sfx.Levelup);

            exp -= maxExp;

            level++;

            maxExp = (level * 2) + 8;

            GameManager.Instance.UIManagerProperty.SetActiveSkillSelect();
        }
    }
}

