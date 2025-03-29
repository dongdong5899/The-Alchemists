using System;
using System.Collections.Generic;
using UnityEngine;

public class EntitySkill<T> : MonoBehaviour where T : Enum
{
    public EntitySkillSO skillSO { get; private set; }
    public Dictionary<T, SkillSO> skillDic;


    private void Awake()
    {
        skillDic = new Dictionary<T, SkillSO>();
    }

    public void Init(EntitySkillSO entitySkill)
    {
        skillSO = entitySkill;

        foreach (T skillEnum in Enum.GetValues(typeof(T)))
        {
            string skillName = skillEnum.ToString();
            for (int i = 0; i < skillSO.skills.Count; i++)
            {
                if (skillSO.skills[i].skillName == skillName)
                {
                    if (skillDic.TryGetValue(skillEnum, out SkillSO value)) continue;
                    skillDic.Add(skillEnum, skillSO.skills[i]);
                }
            }
        }
    }

    public SkillSO GetSkillByEnum(T skillEnum)
    {
        if (skillDic.TryGetValue(skillEnum, out SkillSO value))
            return value;

        return null;
    }
}
