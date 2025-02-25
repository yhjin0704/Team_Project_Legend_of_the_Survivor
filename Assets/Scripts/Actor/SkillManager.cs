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
            Debug.LogError("PlayerSkillList�� null�Դϴ�.");
        }
        _skill.SetActor(actor);
        SkillList.Add(_skill);

        // ��ų ����Ʈ�� �� �� �ѹ� OnOff�Ǹ� �Ǵ� ��ų��
        if (_skill is RotationSkill)
        {
            _skill.Use();
        }
    }
}
