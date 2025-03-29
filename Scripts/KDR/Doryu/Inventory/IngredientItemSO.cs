using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum IngredientItemType
{
    RedMushroom = 2,
    Y_BounceMushroom = 3,
    TrunkFruit = 5,
    ArmoryMushroom = 7,
    HornPigHorn = 11,
    SpikeMushroom = 13,
    StickyGrass = 17,
    ShinyFruit = 19,
    SlimeCore = 23,
    YoungBirldWing = 29,
    GoatHorn = 31,
}

[CreateAssetMenu(menuName = "SO/Doryu/Item/IngredientItem")]
public class IngredientItemSO : ItemSO
{
    [Space(20)]
    [Header("Info")]
    public IngredientItemType itemType;
    public string itemName;
    [TextArea(3, 20)]
    public string itemDescription;
    public float gatheringTime;

    public override string GetItemDescription(int level = 0)
    {
        return itemDescription;
    }

    public override string GetItemName(int level = 0)
    {
        return itemName;
    }

    public override int GetItemTypeNumber()
    {
        return (int)itemType;
    }
}