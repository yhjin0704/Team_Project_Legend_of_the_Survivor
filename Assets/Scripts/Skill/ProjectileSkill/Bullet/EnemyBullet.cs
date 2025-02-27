using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private Renderer _renderer;

    protected override void Awake()
    {
        base.Awake();
        _renderer = GetComponent<Renderer>();
    }

    protected override void Start()
    {
        base.Start();

        Destroy(gameObject, 10.0f);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter2D(Collider2D _collision)
    {
        base.OnTriggerEnter2D(_collision);

        if (_collision.CompareTag("Player"))
        {
            _collision.gameObject.GetComponent<BaseController>().Hit(damage);
            Destroy(gameObject);
        }
    }
}
