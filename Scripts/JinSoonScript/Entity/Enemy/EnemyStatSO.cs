using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyStat")]
public class EnemyStatSO : EntityStatSO
{
    public Stat patrolTime;
    public Stat patrolDelay;
    public Stat detectingDistance;
    public Stat attackDistance;

    public List<DropItemStruct> dropItems = new List<DropItemStruct>();

    protected Dictionary<EnemyStatType, Stat> enemyStatDic;

    protected override void OnEnable()
    {
        base.OnEnable();

        enemyStatDic = new Dictionary<EnemyStatType, Stat>();

        Type agentStatType = typeof(EnemyStatSO);

        foreach (EnemyStatType enumType in Enum.GetValues(typeof(EnemyStatType)))
        {
            try
            {
                string fieldName = LowerFirstChar(enumType.ToString());
                FieldInfo statField = agentStatType.GetField(fieldName);
                enemyStatDic.Add(enumType, statField.GetValue(this) as Stat);
            }
            catch (Exception ex)
            {
                Debug.LogError($"There are no stat - {enumType.ToString()}, {ex.Message}");
            }
        }
    }

    private string LowerFirstChar(string input) => $"{char.ToLower(input[0])}{input.Substring(1)}";
}

public enum EnemyStatType
{
    PatrolTime,
    PatrolDelay,
    DetectingDistance,
}
