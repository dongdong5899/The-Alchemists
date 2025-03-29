using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTypeEnum
{
    Damage, //������
    Petrification, //��ȭ
    Growing, //����
    Heal, //ȸ��
    PoorRecovery, //�� ����
    Stun, //����
    Slowdown, //�̼� ����
    Fragile, //�޴� ������ ����
    DotDeal, //��Ʈ��
    Brainwash, //�Ʊ� ����(����)
    Floating, //����
    Weak, //������ ����
    IncreaseKnockback, //�˹� ����
    NatureSync, //�ڿ���ȭ
    Strength, //��
    Resistance, //�޴� ������ ����
    Speed, //�̼� ����
    Spike, //���ü��� ����
    SpikeShield, //���ù���
    HornSpike, //�ʿ� ���û���
    Boom, //����
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