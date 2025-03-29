//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class RecipeUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
//{
//    public RecipeSO recipe;
//    private RectTransform background;
//    private RecipeSelect recipeSelect;
//    private RecipeParent recipeParent;

//    [SerializeField] private GameObject plusUI;
//    [SerializeField] private GameObject arrowUI;

//    [Header("IsThisObjectIsForMakingPortion")]
//    public bool isRecipeOnPharmaceuticalTable = true;

//    [HideInInspector]
//    public bool isCurSelectedRecipe = false;

//    private void Awake()
//    {
//        background = GetComponent<RectTransform>();
//    }

//    public void Init(RecipeSO recipe, bool isRecipeOnPharmaceuticalTable)
//    {
//        this.recipe = recipe;
//        this.isRecipeOnPharmaceuticalTable = isRecipeOnPharmaceuticalTable;
//        recipeSelect = FindAnyObjectByType<RecipeSelect>();

//        #region SetRecipeImageByRecipeSO
//        Image img;
//        for (int i = 0; i < recipe.ingredients.Length - 1; ++i) 
//        {
//            img = Instantiate(plusUI, transform).GetComponent<Image>();
//            img.sprite = recipe.ingredients[i].itemImage;
//            Instantiate(plusUI, transform);
//        }

//        img = Instantiate(plusUI, transform).GetComponent<Image>();
//        img.sprite = recipe.ingredients[recipe.ingredients.Length - 1].itemImage;

//        Instantiate(arrowUI, transform);

//        img = Instantiate(plusUI, transform).GetComponent<Image>();
//        img.sprite = recipe.portion.itemImage;
//        #endregion
//    }

//    public void OnPointerClick(PointerEventData eventData)
//    {
//        if (isRecipeOnPharmaceuticalTable == false) return;

//        recipeSelect.SelectRecipe(this);
//    }

//    public void OnPointerEnter(PointerEventData eventData)
//    {
//        if (isRecipeOnPharmaceuticalTable == false) return;

//        background.localScale = new Vector3(1.05f, 1.05f, 1.05f);
//    }

//    public void OnPointerExit(PointerEventData eventData)
//    {
//        if (isRecipeOnPharmaceuticalTable == false) return;

//        background.localScale = new Vector3(1f, 1f, 1f);
//    }
//}
