using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyPotionRecipe : MonoBehaviour
{
    [SerializeField] private Image _potionIcon;
    [SerializeField] private Image _potionType;
    [SerializeField] private Image[] _potionRecipes;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Sprite[] _potionTypeSprite;

    public void SetData(PotionRecipeListSO potionRecipeListSO = null, ItemListSO itemListSO = null, PotionItemSO potionItemSO = null)
    {
        if (potionItemSO == null)
        {
            _potionIcon.color = new Color(1, 1, 1, 0);
            _potionType.color = new Color(1, 1, 1, 0);
            for (int i = 0; i < 3; i++)
            {
                Image image = _potionRecipes[i].transform.GetChild(0).GetComponent<Image>();
                image.color = new Color(1, 1, 1, 0);
                _potionRecipes[i].color = new Color(1, 1, 1, 0);
            }
            _description.SetText("");
        }
        else
        {
            _potionIcon.color = Color.white;
            _potionType.color = Color.white;

            _potionIcon.sprite = potionItemSO.image;
            if (potionItemSO is ThrowPotionItemSO)
                _potionType.sprite = _potionTypeSprite[0];
            else
                _potionType.sprite = _potionTypeSprite[1];
            IngredientItemType[] ingredientItemType = potionRecipeListSO.GetPotionRecipe(potionItemSO);
            for (int i = 0; i < 3; i++)
            {
                Image image = _potionRecipes[i].transform.GetChild(0).GetComponent<Image>();
                _potionRecipes[i].color = Color.white;
                image.color = Color.white;
                image.sprite = itemListSO.GetIngredientItemSO(ingredientItemType[i]).image;
                if (i == 0)
                    image.color = new Color(1, 1, 1, 1);
                else
                    image.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            _description.SetText(potionItemSO.GetItemDescription(2));
        }
    }
}
