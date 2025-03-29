//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public class RecipeSortBtn : MonoBehaviour, IPointerClickHandler
//{
//    private RecipeSortChanger sortChanger;
//    private RectTransform rect;
//    private RectTransform sort;

//    public bool isSelected = false;
//    public bool isAscending = true;
//    //public SortMode sortMode;


//    private void Awake()
//    {
//        sortChanger = GetComponentInParent<RecipeSortChanger>();
//        rect = GetComponent<RectTransform>();
//        sort = transform.Find("Sort").GetComponent<RectTransform>();

//        if(isSelected)
//            rect.localScale = new Vector3(1.1f,1.1f, 1f);
//    }

//    public void OnClick()
//    {
//        if (isSelected == true) isAscending = !isAscending;
//        rect.localScale = new Vector3(1f, 1.2f, 1f);
//        isSelected = true;

//        //sortChanger.ChangeSortMode(sortMode, isAscending);
//        if (isAscending)
//            sort.rotation = Quaternion.identity;
//        else
//            sort.rotation = Quaternion.Euler(0,0,180);
//    }

//    public void OnClickOtherSortModeBtn()
//    {
//        isSelected = false;
//        isAscending = true;
//        rect.localScale = Vector3.one;
//        sort.rotation = Quaternion.identity;
//    }

//    public void OnPointerClick(PointerEventData eventData)
//    {
//        Debug.Log("Å¬¸¯~");
//        OnClick();
//    }
//}
