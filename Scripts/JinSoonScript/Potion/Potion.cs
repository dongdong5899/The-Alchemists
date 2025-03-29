using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType
{
    Drink = 31,
    Throw = 37
}

[Serializable]
public struct PotionInfo
{
    public EffectTypeEnum effectEnum;
    public int level;
}

public abstract class Potion : MonoBehaviour
{
    public PotionItemSO potionItemSO;
    public List<Effect> effects;
    public Entity owner;
    public int level;

    public virtual void Init(QuickSlot slot, Entity owner, Vector2 movement = default, float rotatPow = 0)
    {
        this.owner = owner;
        potionItemSO = slot.assignedItem.itemSO as PotionItemSO;
        effects = new List<Effect>();
        level = slot.assignedItem.level;
        PotionInfo[] potionInfos = potionItemSO.GetPotionEffectInfo(level);
        for (int i = 0; i < potionInfos.Length; i++)
        {
            Effect effect = EffectManager.GetEffect(potionInfos[i].effectEnum);
            if (effect == null) Debug.Log(potionInfos[i].effectEnum);
            effect.Initialize(this, potionInfos[i].level);
            effects.Add(effect);
        }
        slot.TryUsePotion();
    }
    public abstract void UsePotion();
}
