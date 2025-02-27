using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : Bullet
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

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
<<<<<<< HEAD
=======
    protected override void OnTriggerEnter2D(Collider2D _collision)
    {
        base.OnTriggerEnter2D(_collision);

        if (_collision.CompareTag("Enemy"))
        {
            _collision.gameObject.GetComponent<EnemyController>().Hit(damage);
            Destroy(gameObject);
        }
    }
>>>>>>> dev
}
