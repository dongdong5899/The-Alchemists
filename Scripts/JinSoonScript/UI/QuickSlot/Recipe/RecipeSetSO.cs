//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//[CreateAssetMenu(menuName = "SO/Recipe/RecipeSet")]
//public class RecipeSetSO : ScriptableObject
//{
//    public List<RecipeSO> recipes;

//    public void AddItem(RecipeSO recipe)
//    {
//        if(recipes.Contains(recipe) == false)
//            recipes.Add(recipe);

//#if UNITY_EDITOR
//        EditorUtility.SetDirty(this);
//#endif
//    }
//}
