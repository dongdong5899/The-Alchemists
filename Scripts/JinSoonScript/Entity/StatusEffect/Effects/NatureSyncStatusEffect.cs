using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureSyncStatusEffect : StatusEffect
{
    private Player _player;
    private int _timeUpCnt = 0;
    public override void ApplyEffect(Entity target, float cooltime)
    {
        base.ApplyEffect(target, cooltime);

        _player = target as Player;
        _timeUpCnt = 0;

        _player.visualCompo.SetAlpha(0.5f);

        _player.isNatureSync = true;
        if (level >= 1)
            _player.canAttackWithNatureSync = true;
        if (level >= 2)
            _player.OnKilled += HandleKilledEvent;
    }

    private void HandleKilledEvent(Entity entity)
    {
        if (_timeUpCnt >= 10) return;
        _timeUpCnt++;
        _cooltime += 0.2f;
    }

    public override void UpdateEffect()
    {
        base.UpdateEffect();

        if (_player.isNatureSync == false)
        {
            _cooltime = 0;
        }
    }

    public override void OnEnd()
    {
        base.OnEnd();

        _player.visualCompo.SetAlpha(1f);

        if (level >= 2)
            _player.OnKilled -= HandleKilledEvent;

        _player.isNatureSync = false;
        _player.canAttackWithNatureSync = false;
    }
}
