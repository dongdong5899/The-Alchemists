//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RecipeSortChanger : MonoBehaviour
//{
//    private RecipeSortBtn[] recipeSortBtns;
//    [SerializeField] private RecipeParent recipeParent;

//    private void Awake()
//    {
//        recipeSortBtns = GetComponentsInChildren<RecipeSortBtn>();

//        ChangeSortMode(SortMode.ById, true);
//    }

//    public void ChangeSortMode(SortMode sortMode, bool isAscending)
//    {
//        recipeParent.ChangeSortMode(sortMode, isAscending);

//        for(int i =0; i < recipeSortBtns.Length; i++)
//        {
//            if (recipeSortBtns[i].sortMode == sortMode) continue;
//            recipeSortBtns[i].OnClickOtherSortModeBtn();
//        }
//    }
//}
