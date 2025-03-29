using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionItemType
{
    PoisonPotion,
    WindPotion,
    HealPotion,
    BoomPotion,
    StonePotion,
    ShapeHornePotion,
    SpikePotion,
    SlapPotion,
    LightPotion,
    WeakPotion,
    GrowPotion,
}

public enum QuickSlotOutLine
{
    Bronze,
    Silver,
    Gold,
    Diamond,
    Level,
}

public class PotionItemSO : ItemSO
{
    [Space(20)]
    [Header("Info")]
    public PotionItemType itemType;
    public QuickSlotOutLine quickSlotOutLine;
    public string itemName;
    [TextArea(3, 20)]
    public string[] itemDescriptions;

    public virtual PotionInfo[] GetPotionEffectInfo(int level = 0) { return null; }

    public override string GetItemDescription(int level = 0)
    {
        return itemDescriptions[level];
    } 

    public override string GetItemName(int level = 0)
    {
        if (level != 0)
        {
            return $"{itemName}+{level}";
        }
        return itemName;
    }

    public override int GetItemTypeNumber()
    {
        return (int)itemType;
    }
}