using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PetrificationStatusEffect : StatusEffect
{
    private float[] _damageReceivWithLevel = { 0f, 0.2f, 0.5f };
    public override void ApplyEffect(Entity target, float cooltime)
    {
        base.ApplyEffect(target, cooltime);
        _target.Stone(cooltime);
        if (level > 0)
            _target.Stat.damageReceivPercent.AddModifier(_damageReceivWithLevel[level]);
    }

    public override void OnEnd()
    {
        base.OnEnd();
        if (level > 0)
            _target.Stat.damageReceivPercent.RemoveModifierByPercent(_damageReceivWithLevel[level]);
    }
}
