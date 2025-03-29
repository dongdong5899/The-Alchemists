//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;


//public class QuickSlotInserter : InventorySlot
//{
//    private QuickSlotInserterSet inserterSet;
//    private int slotIdx = 0;

//    protected override void Awake()
//    {
//        base.Awake();
//        inserterSet = transform.parent.GetComponent<QuickSlotInserterSet>();
//        //selectedUI = transform.Find("SelectedUI").gameObject;
//    }

//    public override void InsertItem(Item item)
//    {
//        if (item.itemSO.itemType != ItemType.Portion) return;

//        QuickSlotManager.Instance.InsertItem(inserterSet.SlotIdx, slotIdx, item.itemSO);
//        base.InsertItem(item);
//    }

//    public override void DeleteItem()
//    {
//        if (assignedItem == null) return;

//        QuickSlotManager.Instance.RemoveItem(inserterSet.SlotIdx, slotIdx, false);
//    }

//    public void RemoveItem(bool removeInstance)
//    {
//        if (assignedItem == null) return;

//        if (removeInstance)
//            Destroy(assignedItem.gameObject);
//        assignedItem = null;
//    }

//    public override void Select()
//    {
//        for (int i = 0; i < 5; i++)
//            inserterSet.inserter[i].UnSelect();

//        selectUI.SetActive(true);
//    }

//    public override void OnPointerClick(PointerEventData eventData)
//    {
//        Item item = InventoryManager.Instance.curMovingItem;
//        if (item != null && item.itemSO.itemType != ItemType.Portion) return;
//        base.OnPointerClick(eventData);
//    }

//    public override void OnPointerEnter(PointerEventData eventData)
//    {
//        Item item = InventoryManager.Instance.curMovingItem;
//        if (item != null && item.itemSO.itemType != ItemType.Portion) return;
//        base.OnPointerEnter(eventData);
//    }

//    public override void OnPointerExit(PointerEventData eventData)
//    {
//        Item item = InventoryManager.Instance.curMovingItem;
//        if (item != null && item.itemSO.itemType != ItemType.Portion) return;
//        base.OnPointerExit(eventData);
//    }

//    public void Init(int idx)
//    {
//        slotIdx = idx;
//    }
//}
