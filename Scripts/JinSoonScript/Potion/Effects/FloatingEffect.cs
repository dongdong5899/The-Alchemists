using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : Effect
{
    private float[] _durationWithLevel = { 3, 4, 5 };

    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                StatusEffect effect = entity.ApplyStatusEffect(StatusDebuffEffectEnum.Floating, _level, _durationWithLevel[_level]);
                if (effect != null)
                {
                    effect.owner = _potion.owner;
                }
            }
        }
    }
}
