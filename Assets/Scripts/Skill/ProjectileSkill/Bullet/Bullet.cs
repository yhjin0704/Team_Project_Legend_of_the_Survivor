using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 10.0f;

    protected Vector2 dir;

    protected Rigidbody2D _rigidbody;

    protected float damage;
    public float GetDamage()
    {
        return this.damage;
    }
    public void SetDamage(float _damage)
    {
        this.damage = _damage;
    }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        _rigidbody.velocity = dir * speed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void Move()
    {
        
    }
    
    public void SetDir(Vector2 _dir)
    {
        dir = _dir.normalized;
    }

    protected virtual void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Wall"))
        {
            _rigidbody.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;

            Destroy(gameObject, 0.5f);
        }
    }
}
