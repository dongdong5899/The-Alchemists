using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusBuffEffectEnum
{
    //IncreaseKnockback = 2, //넉백 증가
    NatureSync = 16, //자연동화
    Strength = 32, //힘
    Resistance = 64, //받는 데미지 감소
    Speed = 128, //속도 감소
    SpikeShield = 256, //가시방패
}

public enum StatusDebuffEffectEnum
{
    PoorRecovery = 1, //힐 감소
    //Stun = 2, //기절*
    Petrification = 4, //석화
    Slowdown = 8, //이속 감소*
    Fragile = 16, //받는 데미지 증가*
    //DotDeal = 32, //도트딜
    //Glow = 64, //발광
    //Brainwash = 128, //아군 공격(세뇌)
    Floating = 256, //부유
    Weak = 512, //데미지 감소*
}

public enum StatusEffectType
{
    Buff,
    Debuff
}