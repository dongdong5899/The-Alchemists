using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoorRecoveryEffect : Effect
{
    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                entity.ApplyStatusEffect(StatusDebuffEffectEnum.PoorRecovery, _level, 5f);
            }
        }
        GameObject.Instantiate(EffectInstantiateManager.Instance.healDamageEffect, _potion.transform.position, Quaternion.identity);
    }
}
