using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player player;

    private List<SkillBase> PlayerSkillList;
    public List<SkillBase> GetPlayerSkillList()
    {
        return PlayerSkillList;
    }

    void Awake()
    {
        player = GetComponent<Player>();
    }   

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayerSkill(SkillBase _skill)
    {
        if (PlayerSkillList == null)
        {
            PlayerSkillList = new List<SkillBase>();
        }

        PlayerSkillList.Add(_skill);

        // 스킬 리스트에 들어갈 때 한번 OnOff되면 되는 스킬들
        if (_skill is RotationSkill)
        {
            _skill.Use();
        }
    }
}
