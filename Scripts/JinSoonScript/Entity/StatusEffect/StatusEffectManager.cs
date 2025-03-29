using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager
{
    private Entity _owner;

    private Dictionary<StatusBuffEffectEnum, StatusEffect> _statusBuffEffectDictionary;
    private Dictionary<StatusDebuffEffectEnum, StatusEffect> _statusDebuffEffectDictionary;
    private List<StatusEffect> _enableEffects;

    public StatusEffectManager(Entity owner)
    {
        _owner = owner;
        _enableEffects = new List<StatusEffect>();
        _statusBuffEffectDictionary = new Dictionary<StatusBuffEffectEnum, StatusEffect>();
        _statusDebuffEffectDictionary = new Dictionary<StatusDebuffEffectEnum, StatusEffect>();
        foreach (StatusBuffEffectEnum effectEnum in Enum.GetValues(typeof(StatusBuffEffectEnum)))
        {
            string enumName = effectEnum.ToString();
            try
            {
                Type t = Type.GetType($"{enumName}StatusEffect");
                StatusEffect effect = Activator.CreateInstance(t) as StatusEffect;
                effect.Init(effectEnum);

                _statusBuffEffectDictionary.Add(effectEnum, effect);
            }
            catch (Exception ex)
            {
#if UNITY_EDITOR
                Debug.LogError(ex);
#endif
            }
        }
        foreach (StatusDebuffEffectEnum effectEnum in Enum.GetValues(typeof(StatusDebuffEffectEnum)))
        {
            string enumName = effectEnum.ToString();
            try
            {
                Type t = Type.GetType($"{enumName}StatusEffect");
                StatusEffect effect = Activator.CreateInstance(t) as StatusEffect;
                effect.Init(effectEnum);

                _statusDebuffEffectDictionary.Add(effectEnum, effect);
            }
            catch (Exception ex)
            {
#if UNITY_EDITOR
                Debug.LogError($"{enumName}StatusEffect");
                Debug.LogError(ex);
#endif
            }
        }
    }

    public void UpdateStatusEffects()
    {
        for (int i = _enableEffects.Count - 1; i >= 0; i--)
        {
            var effect = _enableEffects[i];
            if (effect.IsCompleted())
            {
                effect.OnEnd();
                _enableEffects.RemoveAt(i);
            }
            else
            {
                effect.UpdateEffect();
            }
        }
    }

    public StatusEffect AddStatusEffect(StatusBuffEffectEnum statusEffect, int level, float cooltime)
    {
        StatusEffect effect = _statusBuffEffectDictionary[statusEffect];
        effect.SetInfo(level);
        effect.ApplyEffect(_owner, cooltime);
        _enableEffects.Add(effect);

        return effect;
    }
    public StatusEffect AddStatusEffect(StatusDebuffEffectEnum statusEffect, int level, float cooltime)
    {
        StatusEffect effect = _statusDebuffEffectDictionary[statusEffect];
        effect.SetInfo(level);
        effect.ApplyEffect(_owner, cooltime);
        _enableEffects.Add(effect);

        return effect;
    }
}
