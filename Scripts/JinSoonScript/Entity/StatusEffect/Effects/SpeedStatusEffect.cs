using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedStatusEffect : StatusEffect
{
    private float[] _speedWithLevel = { 0.5f, 0.7f, 1f };

    public override void ApplyEffect(Entity target, float cooltime)
    {
        base.ApplyEffect(target, cooltime);

        _target.Stat.moveSpeed.AddModifierByPercent(_speedWithLevel[level]);
        if (level > 0)
            _target.Stat.globalDamageInflict.AddModifier(0.2f);
    }

    public override void OnEnd()
    {
        base.OnEnd();

        _target.Stat.moveSpeed.RemoveModifierByPercent(_speedWithLevel[level]);
        if (level > 0)
            _target.Stat.globalDamageInflict.RemoveModifier(0.2f);
    }
}
