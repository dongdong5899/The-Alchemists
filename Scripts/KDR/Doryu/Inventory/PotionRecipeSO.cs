using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Doryu/PotionRecipe")]
public class PotionRecipeSO : ScriptableObject
{
    [Header("NeedIngredient")]
    public IngredientItemType[] needIngredients;
    [HideInInspector] public PotionType potionType;
    [HideInInspector] public int needIngredientValue;

    [Header("ReturnPotion")]
    public PotionItemSO potion;

    public void Init()
    {
        potionType = (potion is ThrowPotionItemSO) ? PotionType.Throw : PotionType.Drink;
        needIngredientValue = (int)potionType;
        for (int i = 0; i < needIngredients.Length; i++)
        {
            needIngredientValue *= (int)needIngredients[i];
        }
    }
}
