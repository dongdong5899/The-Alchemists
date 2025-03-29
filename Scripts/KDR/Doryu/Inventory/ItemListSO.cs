using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Doryu/Item/ItemListSO")]
public class ItemListSO : ScriptableObject
{
    public IngredientItemSO[] ingredientItemSOList;
    public PotionItemSO[] throwPotionItemSOList;
    public PotionItemSO[] drinkPotionItemSOList;

    private Dictionary<IngredientItemType, IngredientItemSO> _ingredientItemDic = new Dictionary<IngredientItemType, IngredientItemSO>();

    private void OnEnable()
    {
        foreach (IngredientItemSO itemSO in ingredientItemSOList)
        {
            _ingredientItemDic.Add(itemSO.itemType, itemSO);
        }
    }

    public IngredientItemSO GetIngredientItemSO(IngredientItemType itemType)
    {
        return _ingredientItemDic[itemType];
    }
}