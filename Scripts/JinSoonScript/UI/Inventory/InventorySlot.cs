//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
//{
//    protected RectTransform rect;

//    protected Image img;
//    protected GameObject selectUI;
//    protected RectTransform[] parents;
//    public Inventory inventory { get; protected set; }
//    public Item assignedItem { get; protected set; }
//    public bool isSelectedNow { get; protected set; } = false;

//    private bool acceptInsert = true;

//    public void AcceptInsert(bool accept) => acceptInsert = accept;

//    protected virtual void Awake()
//    {
//        parents = transform.GetComponentsInParent<RectTransform>();
//        rect = GetComponent<RectTransform>();
//        selectUI = transform.Find("SelectedUI").gameObject;
//        selectUI.SetActive(false);
//    }


//    public virtual void OnPointerEnter(PointerEventData eventData)
//    {

//        rect.localScale = new Vector3(1.05f, 1.05f, 1f);
//        if (acceptInsert == false) return;
//        if (assignedItem != null) return;
//        InventoryManager.Instance.CheckSlot(this);
//    }

//    public virtual void OnPointerExit(PointerEventData eventData)
//    {

//        rect.localScale = new Vector3(1f, 1f, 1f);
//        if (acceptInsert == false) return;
//        if (assignedItem != null) return;
//        InventoryManager.Instance.CheckSlot(null);
//    }

//    public virtual void OnPointerClick(PointerEventData eventData)
//    {
//        Select();

//        InventoryManager.Instance.SetExplain(assignedItem != null ? assignedItem.itemSO : null);

//        if(inventory != null)
//        {
//            inventory.SelectItem(assignedItem);
//        }
//    }

//    public virtual void InsertItem(Item item)
//    {
//        //if (acceptInsert == false) return;

//        assignedItem = item;
//        //Vector3 position = Vector3.zero;
//        //foreach (var r in parents)
//        //{
//        //    if (r.GetComponent<Canvas>() != null || r.name == "InventoryBackground") break;
//        //    position += r.localPosition;
//        //}
//        Debug.Log("Re1 : " + item.itemAmount);
//        item.GetComponent<RectTransform>().position = transform.position;
//        item.Init(item.itemAmount, this);
//    }

//    public virtual void DeleteItem()
//    {
//        assignedItem = null;
//    }

//    public void UseItem()
//    {
//        if (assignedItem != null)
//            Destroy(assignedItem.gameObject);
//        assignedItem = null;
//    }

//    public virtual void Select()
//    {
//        if (inventory != null)
//        {
//            inventory.UnSelectAllSlot();
//            if (assignedItem != null)
//                inventory.OnSelectItem?.Invoke(assignedItem.itemSO);
//            else
//                inventory.OnSelectItem?.Invoke(null);
//        }

//        selectUI.SetActive(true);
//    }

//    public void UnSelect()
//    {
//        selectUI.SetActive(false);
//    }

//    public void Init(Inventory inventory)
//    {
//        this.inventory = inventory;
//    }
//}
