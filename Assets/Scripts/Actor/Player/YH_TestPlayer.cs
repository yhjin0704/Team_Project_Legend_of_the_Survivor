using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class YH_TestPlayer : Player
{
    protected float checkDelay = 0;


    protected override void Awake()
    {
        base.Awake();
        
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

    

//        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Archer_Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime< 1.0f)
//        {
//            animator.SetBool("IsAttack", false);
//        }

//if (checkDelay <= 0)
//{
//    checkDelay = atkDelay;
//    if (shotPos.transform.rotation.z >= -0.9f && shotPos.transform.rotation.z < 0.9f)
//    {
//        _renderer.transform.localScale = new Vector3(1, 1, 1);
//    }
//    else
//    {
//        _renderer.transform.localScale = new Vector3(-1, 1, 1);
//    }
//    animator.SetBool("IsAttack", true);
}
