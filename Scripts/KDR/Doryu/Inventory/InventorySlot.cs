using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Inventory inventory;

    public bool isLocked = false;

    public int maxMergeAmount = 5;
    public bool isFull => assignedItem.amount == maxMergeAmount;

    private Item _assignedItem;
    public Item assignedItem
    {
        get => _assignedItem;
        private set
        {
            _assignedItem = value;
            _assignedItem?.SetSlot(transform);
        }
    }

    public int assignedItemAmount
    {
        get => _assignedItem.amount;
        private set
        {
            _assignedItem.amount = value;
            if (_assignedItem.amount == 0)
            {
                Destroy(_assignedItem.gameObject);
                _assignedItem = null;
                Save();
            }
        }
    }

    public bool isSelected;

    private GameObject _selectVolumObj;

    private void Update()
    {
        if (_assignedItem != null && _assignedItem.amount == 0)
        {
            Destroy(_assignedItem.gameObject);
            _assignedItem = null;
            Save();
        }
    }

    public void Init(Inventory inventory)
    {
        _selectVolumObj = transform.Find("SelectedUI").gameObject;
        this.inventory = inventory;
    }

    public void Save()
    {
        inventory.Save();
    }

    public bool TrySwapSlotData(InventorySlot slot)
    {
        if (slot == this || (slot.assignedItem != null && inventory.CanEnterInven(slot.assignedItem.itemSO) == false) ||
            slot.inventory.CanEnterInven(assignedItem.itemSO) == false)
            return false;

        if (slot.assignedItem != null && assignedItem != null && 
            slot.assignedItem.itemSO == assignedItem.itemSO &&
            slot.assignedItem.level == assignedItem.level)
        {
            slot.assignedItem.amount += assignedItem.amount;
            if (slot.assignedItem.amount > slot.maxMergeAmount)
            {
                assignedItem.amount = slot.assignedItem.amount - slot.maxMergeAmount;
                slot.assignedItem.amount = slot.maxMergeAmount;
                assignedItem.SetSlot();
            }
            else
            {
                Destroy(assignedItem.gameObject);
                SetItem(null);
            }
        }
        else
        {

            Item temp = assignedItem;
            assignedItem = slot.assignedItem;
            slot.assignedItem = temp;

            if (slot.assignedItem != null && slot.assignedItem.amount > slot.maxMergeAmount)
            {
                OverAmount(slot, this);
            }
            else if (assignedItem != null && assignedItem.amount > maxMergeAmount)
            {
                OverAmount(this, slot);
            }
        }

        slot.Save();
        Save();
        return true;
    }

    private void OverAmount(InventorySlot overedSlot, InventorySlot slot)
    {
        Item item = Instantiate(InventoryManager.Instance.itemPrefab);
        item.Init();
        item.itemSO = overedSlot.assignedItem.itemSO;
        item.amount = overedSlot.assignedItem.amount - overedSlot.maxMergeAmount;
        overedSlot.assignedItem.amount = overedSlot.maxMergeAmount;
        if (slot.assignedItem == null)
        {
            slot.SetItem(item);
        }
        else if (slot.assignedItem.itemSO == overedSlot.assignedItem.itemSO)
        {
            slot.assignedItem.amount += item.amount;
            if (slot.assignedItem.amount > slot.maxMergeAmount)
            {
                int remain = slot.assignedItem.amount - slot.maxMergeAmount;
                slot.assignedItem.amount = slot.maxMergeAmount;
                if (remain != 0)
                {
                    Item item2 = Instantiate(InventoryManager.Instance.itemPrefab);
                    item2.Init();
                    item2.itemSO = slot.assignedItem.itemSO;
                    item2.amount = remain;
                    if (slot.inventory.AddItem(item2) == false)
                    {
                        Destroy(item2.gameObject);
                    }
                }
            }
        }
        else if (slot.inventory.AddItem(item) == false)
        {
            Destroy(item.gameObject);
        }
    }

    public void SetItem(Item item)
    {
        assignedItem = item;
    }

    public bool TryAddAmount(int amount = 1)
    {
        if (assignedItemAmount + amount > maxMergeAmount)
            return false;
        assignedItemAmount += amount;
        return true;
    }
    public bool TrySubAmount(int amount = 1)
    {
        if (assignedItemAmount - amount < 0)
            return false;
        assignedItemAmount -= amount;
        Save();
        return true;
    }
    public bool TrySetAmount(int amount = 0)
    {
        if (amount > maxMergeAmount || amount < 0)
            return false;
        assignedItemAmount = amount;
        return true;
    }
    public int AddAmount(int amount = 1)
    {
        int remain = 0;
        if (TryAddAmount(amount) == false)
        {
            remain = assignedItemAmount + amount - maxMergeAmount;
            TrySetAmount(maxMergeAmount);
        }

        return remain;
    }
    public int SubAmount(int amount = 1)
    {
        int over = 0;
        if (TrySubAmount(amount) == false)
        {
            over = amount - assignedItemAmount;
            TrySetAmount(0);
            Save();
        }

        return over;
    }

    public void OnMouse(bool isOnMouse)
    {
        if (_selectVolumObj == null) return;

        _selectVolumObj.SetActive(isOnMouse);
    } 
    public void OnSelect(bool isSelected)
    {
        if (_selectVolumObj == null) return;

        _selectVolumObj.transform.localScale = Vector3.one * (isSelected ? 1.05f : 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isLocked) return;

        InventoryManager.Instance.stayMouseSlot = this;

        if (isSelected) return;
        OnMouse(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isLocked) return;

        InventoryManager.Instance.stayMouseSlot = null;

        if (isSelected) return;
        OnMouse(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLocked) return;

        inventory.SetSelected(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        if (assignedItem != null)
            assignedItem.transform.SetParent(inventory.itemStorage);

        InventoryManager.Instance.dragItemSlot = this;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        if (assignedItem != null)
            assignedItem.transform.SetParent(transform);

        InventorySlot slot = InventoryManager.Instance.stayMouseSlot;
        if (slot == null || assignedItem == null)
            assignedItem?.SetSlot();
        else
        {
            if (TrySwapSlotData(slot))
            {
                inventory.SetSelected(null);
                slot.inventory.SetSelected(slot);
            }
            else
            {
                assignedItem.SetSlot();
            }
        }
        InventoryManager.Instance.dragItemSlot = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;
    }
}

