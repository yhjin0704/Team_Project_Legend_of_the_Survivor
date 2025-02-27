using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public int level = 1;
    public int maxExp;
    public int exp = 0;
    public int gold = 0;
<<<<<<< HEAD
    protected override void Awake()
    {
        base.Awake();
=======

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
>>>>>>> dev
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
            exp -= maxExp;

            level++;

            maxExp = (level * 2) + 8;
        }
    }
}

