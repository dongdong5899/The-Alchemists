using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileStatusEffect : StatusEffect
{
    private float[] _damageReceivWithLevel = { 0.3f, 0.6f, 1f };
    public override void ApplyEffect(Entity target, float cooltime)
    {
        base.ApplyEffect(target, cooltime);
        _target.Stat.damageReceivPercent.AddModifier(_damageReceivWithLevel[level]);
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _target.Stat.damageReceivPercent.RemoveModifier(_damageReceivWithLevel[level]);
    }
}
