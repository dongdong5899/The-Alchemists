using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthStatusEffect : StatusEffect
{
    private float[] _damageWithLevel = { 0.7f, 1f, 1.5f };

    public override void ApplyEffect(Entity target, float cooltime)
    {
        base.ApplyEffect(target, cooltime);

        _target.Stat.physicalDamageInflict.AddModifier(_damageWithLevel[level]);
    }

    public override void OnEnd()
    {
        base.OnEnd();

        _target.Stat.physicalDamageInflict.RemoveModifier(_damageWithLevel[level]);
    }
}
