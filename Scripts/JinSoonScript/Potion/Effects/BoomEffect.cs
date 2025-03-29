using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : Effect
{
    private float[] _delayWithLevel = { 10, 0.3f, 0.2f };
    private int[] _damageWithLevel = { 10, 12, 15 };
    private Player _player;
    private ThrowPotion _throwPotion;
    public override void ApplyEffect()
    {
        _player = _potion.owner as Player;
        _throwPotion = _potion as ThrowPotion;
        foreach (var target in _affectedTargets)
        {
            if (target is Entity entity)
            {
                Boom boom = GameObject.Instantiate(_player.boom, _potion.transform.position, Quaternion.identity);
                boom.Init(_throwPotion.range, _damageWithLevel[_level], _delayWithLevel[_level], _level + 1, _potion.owner);
            }
        }
    }
}
