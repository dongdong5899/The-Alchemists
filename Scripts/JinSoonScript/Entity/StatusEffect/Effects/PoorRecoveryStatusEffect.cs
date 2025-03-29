using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoorRecoveryStatusEffect : StatusEffect
{
    private float[] _poorRecoveryWithLevel = { -0.2f, -0.3f, -0.5f };

    public override void ApplyEffect(Entity target, float cooltime)
    {
        base.ApplyEffect(target, cooltime);

        _target.Stat.recoveryReceive.AddModifier(_poorRecoveryWithLevel[level]);
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _target.Stat.recoveryReceive.RemoveModifier(_poorRecoveryWithLevel[level]);
    }
}
