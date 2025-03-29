//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public enum SortMode
//{
//    ById,
//    ByString,
//    ByIngredientsNum
//}

//public class RecipeParent : MonoBehaviour
//{
//    [SerializeField] private GameObject recipeTemplate;
//    [SerializeField] private bool isRecipeOnPharmaceuticalTable;

//    private VerticalLayoutGroup vlg;
//    private RectTransform rect;
//    private float recipeHeight;

//    private SortMode curSortMode = SortMode.ById;
//    private bool isAscending;

//    private void Awake()
//    {
//        rect = GetComponent<RectTransform>();
//        vlg = GetComponent<VerticalLayoutGroup>();
//        recipeHeight = recipeTemplate.GetComponent<RectTransform>().rect.height;
//    }

//    private void Start()
//    {
//        List<RecipeSO> recipes = RecipeManager.Instance.curRecipe;

//        for (int i = 0; i < recipes.Count; i++)
//        {
//            RecipeUI r = Instantiate(recipeTemplate, transform).GetComponent<RecipeUI>();
//            r.Init(recipes[i], isRecipeOnPharmaceuticalTable);
//        }

//        Resize();
//    }

//    public void Resize()
//    {
//        rect.sizeDelta =
//            new Vector2(rect.sizeDelta.x, recipeHeight * transform.childCount + vlg.spacing * (transform.childCount - 1));

//        SortChild();
//    }

//    public void ChangeSortMode(SortMode sortMode, bool isAscending)
//    {
//        curSortMode = sortMode;
//        this.isAscending = isAscending;
//        SortChild();
//    }

//    public void SortChild()
//    {
//        for (int i = 0; i < transform.childCount; ++i)
//        {
//            RecipeUI recipeUI = transform.GetChild(i).GetComponent<RecipeUI>();

//            for (int j = i + 1; j < transform.childCount; ++j)
//            {
//                RecipeUI comp = transform.GetChild(j).GetComponent<RecipeUI>();
//                bool changeParent = false;

//                switch (curSortMode)
//                {
//                    case SortMode.ById:
//                        changeParent = 
//                            (comp.recipe.id < recipeUI.recipe.id);
//                        break;
//                    case SortMode.ByString:
//                        //왼쪽게 더 먼저 나온다면임 ㅇㅋ?
//                        changeParent =
//                            (string.Compare(comp.recipe.portion.itemName, recipeUI.recipe.portion.itemName) == -1);
//                        break;
//                    case SortMode.ByIngredientsNum:
//                        changeParent =
//                            (comp.recipe.ingredients.Length < recipeUI.recipe.ingredients.Length);
//                        break;
//                }

//                if (changeParent == isAscending)
//                {
//                    ChangeParent(recipeUI.transform, comp.transform, i, j);
//                    recipeUI = transform.GetChild(i).GetComponent<RecipeUI>();
//                }
//            }
//        }
//    }

//    public void ChangeParent(Transform a, Transform b, int f, int s)
//    {
//        a.SetSiblingIndex(s);
//        b.SetSiblingIndex(f);
//    }
//}
