using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float baseValue;

    public List<float> modifiers = new List<float>();
    public List<float> percentModifiers = new List<float>();


    /// <summary>
    /// 값에 수정된 값까지 다 계산해서 들고옴
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
    /// 추가해둔 수정자를 제거함
    /// </summary>
    /// <param name="value"></param>
    public void RemoveModifier(float value)
    {
        if (value != 0)
            modifiers.Remove(value);
    }

    /// <summary>
    /// 수정자를 추가해줌
    /// </summary>
    /// <param name="value">얼마나 수정해줄지(값을 줄이고 싶다면 음수를 주면 됨)</param>
    public void AddModifier(float value)
    {
        if (value != 0)
            modifiers.Add(value);
    }

    /// <summary>
    /// 수정자를 값이아니라 비율로 추가해줌
    /// </summary>
    /// <param name="percentValue">음수면 줄이고 양수면 늘리는 연산으로 해줄거임</param>
    public void AddModifierByPercent(float percentValue)
    {
        if (percentValue != 0)
            percentModifiers.Add(percentValue);
    }

    /// <summary>
    /// 수정자를 값이아니라 비율로 추가해준거 제거
    /// </summary>
    /// <param name="percentValue"></param>
    public void RemoveModifierByPercent(float percentValue)
    {
        if (percentValue != 0)
            percentModifiers.Remove(percentValue);
    }

    /// <summary>
    /// 걍 모든 수정자들을 없애
    /// </summary>
    /// <param name="value"></param>
    public void SetDefaultValue(float value)
    {
        baseValue = value;
    }
}
