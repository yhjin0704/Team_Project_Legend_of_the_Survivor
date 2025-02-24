using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private Player player;

    private List<SkillBase> PlayerSkillList = new List<SkillBase>();
    public List<SkillBase> GetPlayerSkillList()
    {
        return PlayerSkillList;
    }

    private Transform target;
    public Transform GetTarget()
    {
        return target;
    }

    void Awake()
    {
        player = GetComponent<Player>();

        StraightShotting straightShotting = new StraightShotting();
        AddPlayerSkill(straightShotting);
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
            Debug.LogError("PlayerSkillList가 null입니다.");
        }
        _skill.SetActor(player);
        PlayerSkillList.Add(_skill);

        // 스킬 리스트에 들어갈 때 한번 OnOff되면 되는 스킬들
        if (_skill is RotationSkill)
        {
            _skill.Use();
        }
    }
}
