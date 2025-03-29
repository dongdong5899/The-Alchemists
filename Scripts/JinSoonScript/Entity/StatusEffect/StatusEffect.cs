using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    private float _startTime;
    protected float _cooltime;
    protected Entity _target;
    private bool _isBuffEffect;
    private StatusBuffEffectEnum _buffEnum;
    private StatusDebuffEffectEnum _debuffEnum;
    public int level;
    public Entity owner;

    public void Init(StatusBuffEffectEnum statusEnum)
    {
        _isBuffEffect = true;
        _buffEnum = statusEnum;
    }
    public void Init(StatusDebuffEffectEnum statusEnum)
    {
        _isBuffEffect = false;
        _debuffEnum = statusEnum;
    }

    public void SetInfo(int level)
    {
        this.level = level;
    }

    public virtual void ApplyEffect(Entity target, float cooltime)
    {
        _target = target;
        _startTime = Time.time;
        _cooltime = cooltime;
    }

    public virtual void UpdateEffect() { }
    public virtual void OnEnd()
    {
        if (_isBuffEffect)
            _target.RemoveStatusEffect(_buffEnum);
        else
            _target.RemoveStatusEffect(_debuffEnum);
    }

    public bool IsCompleted() 
        =>_startTime + _cooltime < Time.time;
}
