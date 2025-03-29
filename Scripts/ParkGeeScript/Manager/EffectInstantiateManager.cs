using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInstantiateManager : Singleton<EffectInstantiateManager>
{
    public ParticleSystem stonHitEffect;
    public ParticleSystem spikeHitEffect;
    public ParticleSystem sliceEffect;
    public ParticleSystem healDamageEffect;
    public ParticleSystem weakEffect;
    public ParticleSystem growEffect;
    public ParticleSystem boomEffect;
    [Header("StatusEffect")]
    public ParticleSystem statusEffect;

    public Dictionary<StatusDebuffEffectEnum, Color> statusDebuffEffectColor = new Dictionary<StatusDebuffEffectEnum, Color>();
    private void Awake()
    {
        statusDebuffEffectColor.Add(StatusDebuffEffectEnum.Floating, Color.white);
        statusDebuffEffectColor.Add(StatusDebuffEffectEnum.Slowdown, Color.green);
        statusDebuffEffectColor.Add(StatusDebuffEffectEnum.Fragile, Color.yellow);
        statusDebuffEffectColor.Add(StatusDebuffEffectEnum.Weak, Color.blue);
    }
}
 