using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 10.0f;

    private Vector2 dir;

    private Rigidbody2D rigidbody;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        rigidbody.velocity = dir * speed;
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
}
