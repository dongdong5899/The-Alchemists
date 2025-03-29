using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GimmickSlotController : MonoBehaviour
{
    private InventorySlot[] _inventorySlot = new InventorySlot[3];

    private void Start()
    {
        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            _inventorySlot[i] = transform.GetChild(i).GetComponent<InventorySlot>();
            _inventorySlot[i].maxMergeAmount = int.MaxValue;
            _inventorySlot[i].isLocked = true;
        }
    }

    public void GimmickPotion(PotionItemSO itemSO)
    {
        bool flag = false;
        for (int i = 0; i < _inventorySlot.Length; i++)
        {
            if (_inventorySlot[i].assignedItem == null && flag == false)
            {
                Item item = Instantiate(InventoryManager.Instance.itemPrefab);
                item.itemSO = itemSO;
                item.amount = 1;
                _inventorySlot[i].SetItem(item);
                flag = true;
            }
        }
    }
}
