using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileEffect : Effect
{
    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                entity.ApplyStatusEffect(StatusDebuffEffectEnum.Fragile, _level, 5);
            }
        }

        GameObject.Instantiate(EffectInstantiateManager.Instance.weakEffect, _potion.transform.position, Quaternion.identity);
    }
}
