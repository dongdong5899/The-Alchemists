using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTypeEnum
{
    Damage, //데미지
    Petrification, //석화
    Growing, //성장
    Heal, //회복
    PoorRecovery, //힐 감소
    Stun, //기절
    Slowdown, //이속 감소
    Fragile, //받는 데미지 증가
    DotDeal, //도트딜
    Brainwash, //아군 공격(세뇌)
    Floating, //부유
    Weak, //데미지 감소
    IncreaseKnockback, //넉백 증가
    NatureSync, //자연동화
    Strength, //힘
    Resistance, //받는 데미지 감소
    Speed, //이속 증가
    Spike, //가시송이 스폰
    SpikeShield, //가시방패
    HornSpike, //맵에 가시생성
    Boom, //폭발
}

public static class EffectManager
{
    private static readonly Dictionary<EffectTypeEnum, Func<Effect>> _effectDictionary;

    static EffectManager()
    {
        _effectDictionary = new Dictionary<EffectTypeEnum, Func<Effect>>
        {
            { EffectTypeEnum.Damage, () => new DamageEffect() },
            { EffectTypeEnum.Petrification, () => new PetrificationEffect() },
            { EffectTypeEnum.Growing, () => new GrowingEffect() },
            { EffectTypeEnum.Heal, () => new HealEffect() },
            { EffectTypeEnum.Floating, () => new FloatingEffect() },
            { EffectTypeEnum.Weak, () => new WeakEffect() },
            { EffectTypeEnum.Slowdown, () => new SlowdownEffect() },
            { EffectTypeEnum.PoorRecovery, () => new PoorRecoveryEffect() },
            { EffectTypeEnum.Speed, () => new SpeedEffect() },
            { EffectTypeEnum.Resistance, () => new ResistanceEffect() },
            { EffectTypeEnum.NatureSync, () => new NatureSyncEffect() },
            { EffectTypeEnum.Strength, () => new StrengthEffect() },
            { EffectTypeEnum.Fragile, () => new FragileEffect() },
            { EffectTypeEnum.Spike, () => new SpikeEffect() },
            { EffectTypeEnum.HornSpike, () => new HornSpikeEffect() },
            { EffectTypeEnum.SpikeShield, () => new SpikeShieldEffect() },
            { EffectTypeEnum.Boom, () => new BoomEffect() },
        };
    }

    public static Effect GetEffect(EffectTypeEnum effectEnum)
    {
        if (_effectDictionary.TryGetValue(effectEnum, out Func<Effect> effect))
        {
            return effect.Invoke();
        }
        return null;
    }
}