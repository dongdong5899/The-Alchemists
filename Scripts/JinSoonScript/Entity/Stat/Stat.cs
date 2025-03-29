using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float baseValue;

    public List<float> modifiers = new List<float>();
    public List<float> percentModifiers = new List<float>();


    /// <summary>
    /// ���� ������ ������ �� ����ؼ� ����
    /// </summary>
    /// <returns></returns> 
    public float GetValue()
    {
        float finalValue = baseValue;

        foreach (float value in modifiers)
            finalValue += value;

        float totalPercent = 0;
        foreach (float value in percentModifiers)
            totalPercent += value;

        finalValue += finalValue * totalPercent;

        return finalValue;
    }

    /// <summary>
    /// �߰��ص� �����ڸ� ������
    /// </summary>
    /// <param name="value"></param>
    public void RemoveModifier(float value)
    {
        if (value != 0)
            modifiers.Remove(value);
    }

    /// <summary>
    /// �����ڸ� �߰�����
    /// </summary>
    /// <param name="value">�󸶳� ����������(���� ���̰� �ʹٸ� ������ �ָ� ��)</param>
    public void AddModifier(float value)
    {
        if (value != 0)
            modifiers.Add(value);
    }

    /// <summary>
    /// �����ڸ� ���̾ƴ϶� ������ �߰�����
    /// </summary>
    /// <param name="percentValue">������ ���̰� ����� �ø��� �������� ���ٰ���</param>
    public void AddModifierByPercent(float percentValue)
    {
        if (percentValue != 0)
            percentModifiers.Add(percentValue);
    }

    /// <summary>
    /// �����ڸ� ���̾ƴ϶� ������ �߰����ذ� ����
    /// </summary>
    /// <param name="percentValue"></param>
    public void RemoveModifierByPercent(float percentValue)
    {
        if (percentValue != 0)
            percentModifiers.Remove(percentValue);
    }

    /// <summary>
    /// �� ��� �����ڵ��� ����
    /// </summary>
    /// <param name="value"></param>
    public void SetDefaultValue(float value)
    {
        baseValue = value;
    }
}
