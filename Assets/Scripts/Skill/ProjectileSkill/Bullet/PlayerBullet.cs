using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private Renderer renderer;

    protected override void Awake()
    {
        base.Awake();
        renderer = GetComponent<Renderer>();
    }

    protected override void Start()
    {
        base.Start();

        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
