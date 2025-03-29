using UnityEngine;

public abstract class SkillSO : ScriptableObject
{
    public int id;
    public string skillName;

    [HideInInspector]
    public Skill skill;

    public Stat skillCoolTime;
    public Stat attackDistance;
    public Stat skillAfterDelay;
}