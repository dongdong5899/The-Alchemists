using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemySkill<T> : MonoBehaviour where T : Enum
{
    public EntitySkill<T> enemySkill{ get; protected set; }

    #region SkillSection

    public Stack<SkillSO> readySkill = new Stack<SkillSO>();
    public List<Tuple<SkillSO, float>> notReady = new List<Tuple<SkillSO, float>>();

    private Entity entity;
    private EntitySkillSO skillSO;
    private float attackDistance;

    #endregion


    private void Update()
    {
        for (int i = 0; i < notReady.Count; ++i)
        {
            var item = notReady[i];
            if (item.Item2 + item.Item1.skillCoolTime.GetValue() < Time.time)
            {
                notReady.Remove(item);
                if (readySkill.Count <= 0) attackDistance = item.Item1.attackDistance.GetValue();
                readySkill.Push(item.Item1);
                --i;
            }
        }
    }

    private void ShuffleSkillStack()
    {
        List<SkillSO> skills = skillSO.skills;
        for (int i = 0; i < 10; i++)
        {
            int a = UnityEngine.Random.Range(0, skills.Count);
            int b = UnityEngine.Random.Range(0, skills.Count);

            SkillSO temp = skills[a];
            skills[a] = skills[b];
            skills[b] = temp;
        }

        //우선 공격스택을 다 비우고
        readySkill.Clear();
        for (int i = 0; i < skills.Count; i++)
        {
            //쿨타임중이면 공격스택에 넣지말고
            if (readySkill.Contains(skills[i])) continue;

            //쿨타임이 아닌 녀석들만 스택에 넣어두어라
            readySkill.Push(skills[i]);
        }
        if (readySkill.Peek() == null) return;

        attackDistance = readySkill.Peek().attackDistance.GetValue();
    }

    public void Init(Entity entity, EntitySkillSO skillSO, float attackDistance)
    {
        this.skillSO = skillSO;
        this.attackDistance = attackDistance;
        this.entity = entity;

        enemySkill = gameObject.AddComponent<EntitySkill<T>>();
        enemySkill.Init(skillSO);

        foreach (var item in skillSO.skills)
        {
            item.skill.SetOwner(entity);
            Type type = item.skill.GetType();
            gameObject.AddComponent(type);
        }
    }
}
