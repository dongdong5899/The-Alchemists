using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakStatusEffect : StatusEffect
{
    public override void ApplyEffect(Entity target, float cooltime)
    {
        base.ApplyEffect(target, cooltime);

        _target.Stat.globalDamageInflict.AddModifier(-level);
    }

    public override void OnEnd()
    {
        base.OnEnd();

        _target.Stat.globalDamageInflict.RemoveModifier(-level);
    }
}
