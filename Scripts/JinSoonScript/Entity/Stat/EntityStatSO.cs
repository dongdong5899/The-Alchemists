using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat")]
public class EntityStatSO : ScriptableObject
{
    public Stat maxHp;
    public Stat recoveryReceive;
    public Stat armor;
    public Stat physicalDamageInflict;
    public Stat globalDamageInflict;
    public Stat damageReceivPercent;
    public Stat damageReceiv;
    public Stat moveSpeed;
    public Stat jumpForce;
    public float weight = 1;
    [Tooltip("Percentage-2ndDecimalPlace")]public Stat criticalChance;
    [Tooltip("Percentage-2ndDecimalPlace")]public Stat criticalDamage;

    protected Entity owner;
    //protected Dictionary<StatType, Stat> statDic;

    protected virtual void OnEnable()
    {
        recoveryReceive.SetDefaultValue(1);
        damageReceivPercent.SetDefaultValue(1);
        physicalDamageInflict.SetDefaultValue(1);
        globalDamageInflict.SetDefaultValue(1);

        //statDic = new Dictionary<StatType, Stat>();

        //Type agentStatType = typeof(EntityStatSO);

        //foreach (StatType enumType in Enum.GetValues(typeof(StatType)))
        //{
        //    try
        //    {
        //        string fieldName = LowerFirstChar(enumType.ToString());
        //        FieldInfo statField = agentStatType.GetField(fieldName);
        //        statDic.Add(enumType, statField.GetValue(this) as Stat);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.LogError($"There are no stat - {enumType.ToString()}, {ex.Message}");
        //    }
        //}
    }

    public virtual void SetOwner(Entity owner)
    {
        this.owner = owner;
    }

    public float ArmoredDamage(float incomingDamage)
    {
        //아머수치 1당 0.2데미지 감소
        return Mathf.Max(1f, incomingDamage - (armor.GetValue() * 0.5f));
    }

    public bool IsCritical(ref float damage)
    {
        bool isCritical = IsHItPercent(criticalChance.GetValue());

        if (isCritical)
            damage = Mathf.FloorToInt(damage * criticalDamage.GetValue());

        return isCritical;
    }

    protected bool IsHItPercent(float statValue) => UnityEngine.Random.Range(1, 10000) < statValue * 100;

    public virtual void IncreaseStatFor(int modifyValue, float duration, Stat statToModify)
    {
        owner.StartCoroutine(StatModifyCoroutine(modifyValue, duration, statToModify));
    }

    private IEnumerator StatModifyCoroutine(float modifyValue, float duration, Stat statToModify)
    {
        statToModify.AddModifier(modifyValue);
        yield return new WaitForSeconds(duration);
        statToModify.RemoveModifier(modifyValue);
    }

    //public Stat GetStatByEnum(StatType type) => statDic[type]; 

    private string LowerFirstChar(string input) => $"{char.ToLower(input[0])}{input.Substring(1)}";
}

[Serializable]
public struct DropItemStruct
{
    public GameObject dropItemPf;
    public float appearChance;
}

//public enum StatType
//{
//    MaxHp,
//    Armor,
//    CriticalChance,
//    CriticalDamage,
//    Damage,
//    MoveSpeed,
//    JumpForce
//}