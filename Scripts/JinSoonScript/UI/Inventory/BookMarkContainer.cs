//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BookMarkContainer : MonoBehaviour
//{
//    public BookMark drinkingPortionBookMark;
//    public BookMark throwingPortionBookMark;

//    public BookMark enableBookMark;
//    public BookMark disableBookMark;

//    public Inventory portionInventory;

//    [SerializeField] private GameObject _portionInventoryObj;
//    [SerializeField] private GameObject _recipeObj;

//    private bool _isPortionInventoryEnabled = false;
//    private bool _isDrinkingPortionInventoryEnabled = false;

//    private void OnEnable()
//    {
//        drinkingPortionBookMark.clickEvent += OnClickBookMark;
//        throwingPortionBookMark.clickEvent += OnClickBookMark;

//        enableBookMark.clickEvent += OnClickLeftBookMark;
//        disableBookMark.clickEvent += OnClickLeftBookMark;
//    }

//    private void OnDisable()
//    {
//        drinkingPortionBookMark.clickEvent -= OnClickBookMark;
//        throwingPortionBookMark.clickEvent -= OnClickBookMark;

//        enableBookMark.clickEvent -= OnClickLeftBookMark;
//        disableBookMark.clickEvent -= OnClickLeftBookMark;
//    }
//    private void OnClickBookMark(bool isEnableDrinkingPortion)
//    {
//        _isDrinkingPortionInventoryEnabled = isEnableDrinkingPortion;
//        if (_isPortionInventoryEnabled == false) return;
//    }

//    private void OnClickLeftBookMark(bool isEnableInventory)
//    {
//        InventoryManager.Instance.EnbableIgredientsInventory(isEnableInventory);
//        _isPortionInventoryEnabled = !isEnableInventory;

//        if (isEnableInventory)
//        {
//            _recipeObj.SetActive(true);
//            _portionInventoryObj.SetActive(false);
//        }
//        else
//        {
//            _recipeObj.SetActive(false);
//            _portionInventoryObj.SetActive(true);
//            OnClickBookMark(_isDrinkingPortionInventoryEnabled);
//        }
//    }
//}
