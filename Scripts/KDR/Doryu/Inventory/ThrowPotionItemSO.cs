using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ThrowPotionInfos
{
    public PotionInfo[] infos;
    public int maxDetactEntity;
    public LayerMask whatIsEnemy;
    public float range;
}

[CreateAssetMenu(menuName = "SO/Doryu/Item/ThrowPotionItem")]
public class ThrowPotionItemSO : PotionItemSO
{
    public ThrowPotionInfos[] potionInfos;

    public override PotionInfo[] GetPotionEffectInfo(int level = 0)
    {
        return potionInfos[level].infos;
    }

    public ThrowPotionInfos GetPotionInfo(int level = 0)
    {
        return potionInfos[level];
    }
}
