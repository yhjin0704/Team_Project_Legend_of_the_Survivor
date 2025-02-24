using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
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

    protected virtual void fixedUpdate()
    {

    }

    public virtual void Use()
    { }
}

public interface ISkillUseDelay
{
    void Use();
}