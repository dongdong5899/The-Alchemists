using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEffect : Effect
{
    private Player _player;
    private int[] _damageWithLevel = { 5, 8, 15 };
    private int[] _countWithLevel = { 2, 3, 3 };
    public override void ApplyEffect()
    {
        _player = _potion.owner as Player;
        for (int i = 0; i < _countWithLevel[_level]; i++)
        {
            Spike spike = GameObject.Instantiate
                (_player.spike, _potion.transform.position, Quaternion.identity);
            spike.Init(new Vector2(Random.Range(-1f, 1f), 10).normalized, 
                _damageWithLevel[_level], _player, _level >= 2 ? 20 : 10, 
                _level >= 2 ? 2.4f : 1.2f);
        }
    }
}
