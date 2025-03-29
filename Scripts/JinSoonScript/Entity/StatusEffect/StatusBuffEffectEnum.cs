using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusBuffEffectEnum
{
    //IncreaseKnockback = 2, //�˹� ����
    NatureSync = 16, //�ڿ���ȭ
    Strength = 32, //��
    Resistance = 64, //�޴� ������ ����
    Speed = 128, //�ӵ� ����
    SpikeShield = 256, //���ù���
}

public enum StatusDebuffEffectEnum
{
    PoorRecovery = 1, //�� ����
    //Stun = 2, //����*
    Petrification = 4, //��ȭ
    Slowdown = 8, //�̼� ����*
    Fragile = 16, //�޴� ������ ����*
    //DotDeal = 32, //��Ʈ��
    //Glow = 64, //�߱�
    //Brainwash = 128, //�Ʊ� ����(����)
    Floating = 256, //����
    Weak = 512, //������ ����*
}

public enum StatusEffectType
{
    Buff,
    Debuff
}