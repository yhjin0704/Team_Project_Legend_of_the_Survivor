using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Actor : MonoBehaviour
{
    [Range(1, 100)]public int hp = 100;
    [Range(1f, 20f)]public float speed = 3;
    public float atk = 1;
    public int gold = 0;
    public bool IsAlive = true; 
    
    protected virtual void Awake()
    {
     
    }

    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void Attak()
    {
    }

    protected virtual void Hit()
    {
    }

    protected virtual void Dead()
    {
    }
}