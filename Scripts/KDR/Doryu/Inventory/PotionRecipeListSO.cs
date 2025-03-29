using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Doryu/PotionRecipeList")]
public class PotionRecipeListSO : ScriptableObject
{
    public PotionRecipeSO[] potionRecipes;

    private Dictionary<int, PotionItemSO> PotionResDict = 
        new Dictionary<int, PotionItemSO>();
    private Dictionary<PotionItemSO, IngredientItemType[]> PotionRecipeDict = 
        new Dictionary<PotionItemSO, IngredientItemType[]>();

    public void OnEnable()
    {
        for (int i = 0; i < potionRecipes.Length; i++)
        {
            potionRecipes[i].Init();
            PotionResDict.Add(potionRecipes[i].needIngredientValue, potionRecipes[i].potion);
            PotionRecipeDict.Add(potionRecipes[i].potion, potionRecipes[i].needIngredients);
        }
    }

    public PotionRecipeSO GetRecipeSO(int idx)
    {
        if (potionRecipes.Length <= idx) return null;
        return potionRecipes[idx];
    }

    public IngredientItemType[] GetPotionRecipe(PotionItemSO potionItemSO)
    {
        return PotionRecipeDict[potionItemSO];
    }

    public PotionItemSO GetPotion(PotSlot[] ingredientItems, PotionType potionType)
    {
        bool isAllSameIngredient = true;
        PotSlot prevSlot = null;
        int sum = (int)potionType;
        for (int i = 0; i < ingredientItems.Length; i++)
        {
            if (ingredientItems[i].assignedItem != null)
            {
                if (prevSlot == null) prevSlot = ingredientItems[i];
                if (prevSlot.assignedItem.itemSO != ingredientItems[i].assignedItem.itemSO) isAllSameIngredient = false;

                sum *= ingredientItems[i].assignedItem.itemSO.GetItemTypeNumber();
            }
        }

        if (prevSlot == null) return null;

        if (isAllSameIngredient)
        {
            sum = (int)Mathf.Pow(prevSlot.assignedItem.itemSO.GetItemTypeNumber(), 3) * (int)potionType;
        }
        if (PotionResDict.ContainsKey(sum))
            return PotionResDict[sum];
        else
            return null;
    }
}
