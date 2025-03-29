using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotSlot : MonoBehaviour
{
    public InventorySlot inventorySlot;
    public Action SlotChanged;

    public Item assignedItem;

    private void Awake()
    {
        inventorySlot = GetComponent<InventorySlot>();
    }

    private void Update()
    {
        if (assignedItem != inventorySlot.assignedItem)
        {
            assignedItem = inventorySlot.assignedItem;
            SlotChanged?.Invoke();
        }
    }

    public void ReturnItem()
    {
        if (inventorySlot.assignedItem == null) return;

        InventoryManager.Instance.TryAddItem(assignedItem);
        inventorySlot.SetItem(null);
    }
    public void ClearItem()
    {
        if (assignedItem != null)
        {
            Destroy(assignedItem.gameObject);
            inventorySlot.SetItem(null);
        }
    }
}
