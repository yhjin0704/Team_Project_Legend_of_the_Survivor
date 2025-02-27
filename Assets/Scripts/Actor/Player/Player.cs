using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public int level = 1;

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
}

