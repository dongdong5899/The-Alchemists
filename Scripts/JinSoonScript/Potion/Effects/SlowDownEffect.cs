using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownEffect : Effect
{
    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                entity.ApplyStatusEffect(StatusDebuffEffectEnum.Slowdown, _level, 5);
            }
        }
    }
}
