using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : Effect
{
    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                Vector2 dir = (_potion.transform.position - entity.transform.position).normalized;
                entity.healthCompo.TakeDamage(_level, dir * 4.5f, _potion.owner);
            }
        }
    }
}
