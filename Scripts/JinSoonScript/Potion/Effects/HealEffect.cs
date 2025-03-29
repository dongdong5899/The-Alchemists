using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class HealEffect : Effect
{
    private int[] heal = { 1, 4, 10 };

    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                entity.healthCompo.GetHeal(heal[_level]);
                //target.ApplyEffect();
            }
        }
    }
}
