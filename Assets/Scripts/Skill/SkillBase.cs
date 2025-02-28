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

    protected int skillLevel = 1;

    protected float damage;
    public float GetDamage()
    {
        return this.damage;
    }
    public void SetDamage(float _damage)
    {
        this.damage = _damage;
    }

    protected float skillMagnification = 1.0f;
    public void SetSkillMagnification(float _skillMagnification)
    {
        this.skillMagnification = _skillMagnification;
    }

    protected BaseController baseController;
    public void SetBaseController(BaseController _baseController)
    {
        this.baseController = _baseController;
    }

    protected bool isFinish = true;
    public bool IsFinish { get { return isFinish; } }

    public virtual void Use()
    { 
    }
}

public interface ISkillUseDelay
{
    void Use();
}

