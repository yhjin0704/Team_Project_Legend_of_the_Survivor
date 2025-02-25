using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    protected Actor actor;
    public void SetActor(Actor _actor)
    {
        this.actor = _actor;
    }

    protected BaseController baseController;
    public void SetBaseController(BaseController _baseController)
    {
        this.baseController = _baseController;
    }

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
    { 
    }
}

public interface ISkillUseDelay
{
    void Use();
}