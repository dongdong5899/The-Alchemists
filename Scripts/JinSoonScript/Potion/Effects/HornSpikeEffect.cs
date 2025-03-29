using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornSpikeEffect : Effect
{
    private int[] _damageWithLevel = { 5, 8, 15 };
    private float[] _sizeWithLevel = { 1f, 1f, 2f };
    private float[] _durationWithLevel = { 15f, 20f, 25f };

    public override void ApplyEffect()
    {
        HornSpike hornSpike = GameObject.Instantiate(PlayerManager.Instance.Player.hornSpike, _potion.transform.position, Quaternion.identity);
        ThrowPotion throwPotion = _potion as ThrowPotion;
        hornSpike.Init(_potion.owner, throwPotion.GetVelocity(), throwPotion.GetSize(), _damageWithLevel[_level], _sizeWithLevel[_level], _durationWithLevel[_level], _level >= 1);
    }
}
