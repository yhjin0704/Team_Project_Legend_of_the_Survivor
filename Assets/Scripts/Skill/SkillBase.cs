using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase
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

    public virtual void Use()
    { 
    }
}

public interface ISkillUseDelay
{
    void Use();
}