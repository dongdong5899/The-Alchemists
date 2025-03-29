using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DrinkPotionInfos
{
    public PotionInfo[] infos;
}

[CreateAssetMenu(menuName = "SO/Doryu/Item/DrinkPotionItem")]
public class DrinkPotionItemSO : PotionItemSO
{
    public DrinkPotionInfos[] potionInfos;

    public override PotionInfo[] GetPotionEffectInfo(int level = 0)
    {
        return potionInfos[level].infos;
    }
}
