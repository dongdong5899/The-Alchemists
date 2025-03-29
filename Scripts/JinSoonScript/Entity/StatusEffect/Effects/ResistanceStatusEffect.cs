using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceStatusEffect : StatusEffect
{
    private int[] _resistanceWithLevel = { -2, -4, -6 };
    public override void ApplyEffect(Entity target, float cooltime)
    {
        base.ApplyEffect(target, cooltime);

        _target.Stat.damageReceivPercent.AddModifier(_resistanceWithLevel[level]);
    }

    public override void OnEnd()
    {
        base.OnEnd();

        _target.Stat.damageReceivPercent.RemoveModifier(_resistanceWithLevel[level]);
    }
}
