using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEffect : Effect
{
    private float[] _durationWithLevel = { 3f, 5f, 8f };
    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                entity.ApplyStatusEffect(StatusDebuffEffectEnum.Weak, _level, _durationWithLevel[_level]);
            }
        }
    }
}
