//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RecipeSelect : MonoBehaviour
//{
//    [SerializeField] private RecipeParent recipeParent;

//    public Transform curSelectedRecipe { get; private set; }
//    public RecipeSO curRecipe { get; private set; }

//    public void SelectRecipe(RecipeUI recipe)
//    {
//        if (recipe.recipe == curRecipe)
//        {
//            curSelectedRecipe = null;
//            curRecipe = null;
//            recipe.transform.SetParent(recipeParent.transform); 

//            recipeParent.Resize();
//            return;
//        }

//        if (curSelectedRecipe != null) 
//        {
//            curSelectedRecipe.transform.SetParent(recipeParent.transform);
//            curSelectedRecipe = null;
//        }

//        curSelectedRecipe = recipe.transform;
//        curRecipe = recipe.recipe;
//        curSelectedRecipe.SetParent(transform);

//        recipeParent.Resize();
//    }


//    public void MakePortion()
//    {

//    }
//}
