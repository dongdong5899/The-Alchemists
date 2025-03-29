using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeShieldEffect : Effect
{
    private float[] _durationWithLevel = { 2f, 2f, 1f };
    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                entity.ApplyStatusEffect(StatusBuffEffectEnum.SpikeShield, _level, _durationWithLevel[_level]);
            }
        }
    }
}
