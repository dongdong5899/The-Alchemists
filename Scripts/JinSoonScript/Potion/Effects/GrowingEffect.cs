using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingEffect : Effect
{
    public override void ApplyEffect()
    {
        foreach (var target in _affectedTargets)
        {
            if (target is GrowingGrass)
            {
                target.ApplyEffect();
            }
            else if(target is GrowingBush)
            {
                target.ApplyEffect();
            }
            else if(target is BlockVine)
            {
                target.ApplyEffect();
            }
        }

        GameObject.Instantiate(EffectInstantiateManager.Instance.growEffect, _potion.transform.position, Quaternion.identity);
    }
}
