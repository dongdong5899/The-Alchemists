using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceEffect : Effect
{
    private float[] _durationWithLevel = { 5f, 4f, 3f };
    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                entity.ApplyStatusEffect(StatusBuffEffectEnum.Resistance, _level, _durationWithLevel[_level]);
            }
        }
    }
}
