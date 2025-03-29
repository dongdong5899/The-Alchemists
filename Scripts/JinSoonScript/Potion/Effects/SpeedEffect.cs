using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffect : Effect
{
    private float[] _durationWithLevel = { 10f, 7f, 5f };
    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                entity.ApplyStatusEffect(StatusBuffEffectEnum.Speed, _level, _durationWithLevel[_level]);
            }
        }
    }
}
