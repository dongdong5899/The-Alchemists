using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeShieldStatusEffect : StatusEffect
{
    private int[] _resistanceWithLevel = { -1, -2, -4 };


    public override void ApplyEffect(Entity target, float cooltime)
    {
        base.ApplyEffect(target, cooltime);

        _target.Stat.damageReceiv.AddModifier(_resistanceWithLevel[level]);
        _target.IsConstParrying = true;
        _target.parryingLevel = level;
        _target.OnShieldEffect(true);
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _target.Stat.damageReceiv.RemoveModifier(_resistanceWithLevel[level]);
        _target.IsConstParrying = false;
        _target.parryingLevel = -1;
        _target.OnShieldEffect(false);
    }

    public override void UpdateEffect()
    {
        base.UpdateEffect();
    }
}
