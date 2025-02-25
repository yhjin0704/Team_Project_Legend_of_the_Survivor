using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    protected Actor actor;

    protected List<SkillBase> SkillList = new List<SkillBase>();
    public List<SkillBase> GetSkillList()
    {
        return SkillList;
    }

    protected virtual void Awake()
    {
        actor = GetComponent<Actor>();
    }

    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void AddSkill(SkillBase _skill)
    {
        if (SkillList == null)
        {
            Debug.LogError("PlayerSkillList가 null입니다.");
        }
        _skill.SetActor(actor);
        SkillList.Add(_skill);

        // 스킬 리스트에 들어갈 때 한번 OnOff되면 되는 스킬들
        if (_skill is RotationSkill)
        {
            _skill.Use();
        }
    }
}
