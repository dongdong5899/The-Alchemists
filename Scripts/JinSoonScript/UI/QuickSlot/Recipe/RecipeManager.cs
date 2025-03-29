//using DG.Tweening;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using UnityEngine;

//public class RecipeManager : Singleton<RecipeManager>
//{
//    public RecipeSetSO recipeSet;
//    public List<RecipeSO> curRecipe;// { get; private set; }

//    [SerializeField] private RectTransform sideBar;
//    [SerializeField] private RectTransform selectedRecipe;

//    private Sequence seq;
//    private bool isRecipeBarOpen = false;

//    private string path = "";

//    private void Awake()
//    {
//        path = Path.Combine(Application.dataPath, "SaveDatas\\OwnedRecipes.json");
//        seq = DOTween.Sequence();
//    }

//    private void Start()
//    {
//        Load();
//    }

//    private void Update()
//    {
//        //디버그용 코드임
//        if (Input.GetKeyDown(KeyCode.L))
//        {
//            if (isRecipeBarOpen == true)
//                CloseRecipeBar();
//            else
//                OpenRecipeBar();
//        }
//    }

//    /// <summary>
//    /// id로 레시피 추가 해줘
//    /// </summary>
//    /// <param name="ingredients">size must be 5</param>
//    public void AddRecipe(IngredientItemSO[] ingredients, PortionItemSO portion, bool loading = false)
//    {
//        RecipeSO recipe = ScriptableObject.CreateInstance("RecipeSO") as RecipeSO;

//        //Ascending Sort
//        for (int i = 0; i < ingredients.Length; i++)
//        {
//            for (int j = i + 1; j < ingredients.Length; j++)
//            {
//                if (ingredients[i].id > ingredients[j].id)
//                {
//                    IngredientItemSO temp = ingredients[i];
//                    ingredients[i] = ingredients[j];
//                    ingredients[j] = temp;
//                }
//            }
//        }


//        recipe.ingredients = ingredients;
//        recipe.portion = portion;
//        recipe.id = portion.id;
//        curRecipe.Add(recipe);

//        if (loading == false) Save();
//    }



//    /// <summary>
//    /// 초기화 해줄때 현재 가지고 있는 Recipe를 초기화해줘
//    /// </summary>
//    public void ResetRecipeData()
//    {
//        curRecipe.Clear();
//        Save();
//    }

//    public void OpenRecipeBar()
//    {
//        if (seq.IsActive() == true)
//            seq.Kill();

//        //QuickSlotManager.Instance.EnableQuickSlot();
//        seq = DOTween.Sequence();

//        isRecipeBarOpen = true;

//        seq.Append(sideBar.DOAnchorPosX(-50f, 0.5f))
//            .Join(selectedRecipe.DOAnchorPosY(425f, 0.5f));
//    }

//    public void CloseRecipeBar()
//    {
//        if (seq.IsActive() == true)
//            seq.Kill();

//        //QuickSlotManager.Instance.DisableQuickSlot();
//        seq = DOTween.Sequence();

//        isRecipeBarOpen = false;

//        seq.Append(sideBar.DOAnchorPosX(900f, 0.5f))
//        .Join(selectedRecipe.DOAnchorPosY(660f, 0.5f));
//    }

//    public void Save()
//    {
//        RecipeSave saves = new RecipeSave();
//        saves.recipes = new List<Recipe>();

//        for (int i = 0; i < curRecipe.Count; i++)
//        {
//            Recipe recipe = new Recipe();
//            RecipeSO temp = curRecipe[i];
//            recipe.ingredientsId = new List<int>();

//            for (int j = 0; j < temp.ingredients.Length; j++)
//                recipe.ingredientsId.Add(temp.ingredients[j].id);

//            recipe.portionId = temp.portion.id;

//            Load();
//            if (curRecipe.Contains(temp) == false)
//                saves.recipes.Add(recipe);
//        }

//        string json = JsonUtility.ToJson(saves, true);
//        File.WriteAllText(path, json);
//    }

//    public void Load()
//    {
//        string json = File.ReadAllText(path);
//        RecipeSave saves = JsonUtility.FromJson<RecipeSave>(json);
//        curRecipe.Clear();

//        for (int i = 0; i < saves.recipes.Count; i++)
//        {
//            List<IngredientItemSO> ingredients = new List<IngredientItemSO>();

//            for (int j = 0; j < saves.recipes[i].ingredientsId.Count; j++)
//                ingredients.Add(InventoryManager.Instance.ItemSet.FindItem(saves.recipes[i].ingredientsId[j]) as IngredientItemSO);

//            PortionItemSO portion = InventoryManager.Instance.ItemSet.FindItem(saves.recipes[i].portionId) as PortionItemSO;

//            AddRecipe(ingredients.ToArray(), portion, true);
//        }
//    }
//}

//[System.Serializable]
//public class RecipeSave
//{
//    public List<Recipe> recipes = new List<Recipe>();
//}


//[System.Serializable]
//public struct Recipe
//{
//    public int portionId;
//    public List<int> ingredientsId;
//}