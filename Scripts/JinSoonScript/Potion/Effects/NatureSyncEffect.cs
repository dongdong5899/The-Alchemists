using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureSyncEffect : Effect
{
    private float[] _durationWithLevel = { 8f, 10f, 8f };

    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                entity.ApplyStatusEffect(StatusBuffEffectEnum.NatureSync, _level, _durationWithLevel[_level]);
            }
        }
    }
}
