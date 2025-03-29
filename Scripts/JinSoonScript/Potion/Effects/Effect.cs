using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    protected List<IAffectable> _affectedTargets;
    protected int _level;
    protected Potion _potion;

    public void Initialize(Potion potion, int level)
    {
        _potion = potion;
        _level = level;
    }

    public void SetAffectedTargets(List<IAffectable> targets)
    {
        _affectedTargets = targets;
    }

    public abstract void ApplyEffect();
}