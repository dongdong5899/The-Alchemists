using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthEffect : Effect
{
    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                entity.ApplyStatusEffect(StatusBuffEffectEnum.Strength, _level, 10f);
            }
        }
    }
}
