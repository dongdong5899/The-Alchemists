using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/EntitySkillSO")]
public class EntitySkillSO : ScriptableObject
{
    public List<SkillSO> skills;

    public SkillSO GetSkillSO(string skillName)
    {
        foreach (SkillSO skill in skills)
        {
            if(skill.skillName == skillName)
            {
                return skill;
            }
        }

        return null;
    }
}
