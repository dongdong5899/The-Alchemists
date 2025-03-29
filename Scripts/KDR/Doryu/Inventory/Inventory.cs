using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Doryu.JBSave;
using System;
using static Cinemachine.DocumentationSortingAttribute;

[Serializable]
public struct InventoryUseItemData
{
    public bool useIngredientItem;
    public bool useDrinkPotionItem;
    public bool useThrowPotionItem;
}

public class Inventory : MonoBehaviour
{
    private InventorySaveData _inventoryData = new InventorySaveData();
    private InventorySlot[,] slots = new InventorySlot[0, 0];

    [SerializeField] private ItemDescriptionArea _description;
    [SerializeField] private Vector2Int _inventorySize;
    [SerializeField] private Transform _slotParent;
    [SerializeField] private bool _isSlotInstantiate = true;

    public InventoryUseItemData useItemData;
    public Action<InventorySlot[,]> OnInventoryModified;
    public Transform itemStorage;
    
    [ContextMenu("ResetSaveData")]
    public void ResetSaveData()
    {
        //slots = new InventorySlot[_inventorySize.x, _inventorySize.y];
        for (int y = 0; y < _inventorySize.y; y++)
        {
            for (int x = 0; x < _inventorySize.x; x++)
            {
                if (slots[x, y].assignedItem != null)
                    Destroy(slots[x, y].assignedItem.gameObject);
                slots[x, y].SetItem(null);
            }
        }
        _inventoryData.Init(_inventorySize.x * 100 + _inventorySize.y);
        Save();
    }

    public InventorySlot GetSlot(int x, int y)
    {
        return slots[x, y];
    }    

    public bool CanEnterInven(ItemSO itemSO)
    {
        if (itemSO is IngredientItemSO)
            return useItemData.useIngredientItem;
        else if (itemSO is ThrowPotionItemSO)
            return useItemData.useThrowPotionItem;
        else if (itemSO is DrinkPotionItemSO)
            return useItemData.useDrinkPotionItem;
        return false;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Init()
    {
        int idx = 0;
        slots = new InventorySlot[_inventorySize.x, _inventorySize.y];
        for (int y = 0; y < _inventorySize.y; y++)
        {
            for (int x = 0; x < _inventorySize.x; x++)
            {
                InventorySlot slot;
                if (_isSlotInstantiate == false)
                {
                    slot = transform.GetChild(idx++).GetComponent<InventorySlot>();
                }
                else
                {
                    slot = Instantiate(InventoryManager.Instance.slotPrefab, _slotParent);
                }
                slot.Init(this);
                slots[x, y] = slot;
            }
        }
        _inventoryData.Init(_inventorySize.x * 100 + _inventorySize.y);
        Load();

        OnInventoryModified?.Invoke(slots);
    }

    public bool AddItem(Item item)
    {
        //이미 있는 아이템에 더하기
        for (int y = 0; y < _inventorySize.y; y++)
        {
            for (int x = 0; x < _inventorySize.x; x++)
            {
                Item slotItem = slots[x, y].assignedItem;
                if (slotItem != null &&
                    slotItem.itemSO == item.itemSO &&
                    slots[x, y].isFull == false &&
                    slotItem.level == item.level)
                {
                    int remain = slots[x, y].AddAmount(item.amount);
                    if (remain != 0)
                    {
                        item.amount = remain;
                        AddItem(item);
                    }
                    else
                    {
                        if (item.gameObject.activeSelf)
                            Destroy(item.gameObject);
                    }
                    Save();
                    return true;
                }
            }
        }

        //이미 있는 아이템이 없거나 가득 찼을때 하나 만들기
        for (int y = 0; y < _inventorySize.y; y++)
        {
            for (int x = 0; x < _inventorySize.x; x++)
            {
                if (slots[x, y].assignedItem == null)
                {
                    if (item.amount > slots[x, y].maxMergeAmount)
                    {
                        Item newItem = Instantiate(InventoryManager.Instance.itemPrefab);
                        newItem.Init();
                        newItem.itemSO = item.itemSO;
                        newItem.level = item.level;
                        newItem.amount = slots[x, y].maxMergeAmount;
                        slots[x, y].SetItem(newItem);

                        item.amount -= slots[x, y].maxMergeAmount;
                        AddItem(item);
                    }
                    else
                    {
                        slots[x, y].SetItem(item);
                    }
                    Save();
                    return true;
                }
            }
        }
        Debug.Log("아이템 인벤이 가득 찼습니다.");
        return false;
    }
    public bool AddItem(ItemSO itemSO, int amount, int level)
    {
        //이미 있는 아이템에 더하기
        for (int y = 0; y < _inventorySize.y; y++)
        {
            for (int x = 0; x < _inventorySize.x; x++)
            {
                if (slots[x, y].assignedItem != null &&
                    slots[x, y].assignedItem.itemSO == itemSO &&
                    slots[x, y].isFull == false &&
                    slots[x, y].assignedItem.level == level)
                {
                    int remain = slots[x, y].AddAmount(amount);
                    if (remain != 0)
                    {
                        AddItem(itemSO, remain, level);
                    }
                    Save();
                    return true;
                }
            }
        }

        //이미 있는 아이템이 없거나 가득 찼을때 하나 만들기
        for (int y = 0; y < _inventorySize.y; y++)
        {
            for (int x = 0; x < _inventorySize.x; x++)
            {
                if (slots[x, y].assignedItem == null)
                {
                    Item newItem = Instantiate(InventoryManager.Instance.itemPrefab);
                    newItem.Init();
                    newItem.itemSO = itemSO;
                    newItem.level = level;
                    if (amount > slots[x, y].maxMergeAmount)
                    {
                        newItem.amount = slots[x, y].maxMergeAmount;
                        slots[x, y].SetItem(newItem);

                        amount -= slots[x, y].maxMergeAmount;
                        AddItem(itemSO, amount, level);
                    }
                    else
                    {
                        newItem.amount = amount;
                        slots[x, y].SetItem(newItem);
                    }
                    Save();
                    return true;
                }
            }
        }
        return false;
    }

    public void Save()
    {
        for (int x = 0; x < _inventorySize.x; x++)
        {
            for (int y = 0; y < _inventorySize.y; y++)
            {
                SlotSave newSlotSaveStruct = new SlotSave();
                if (slots[x, y] != null && slots[x, y].assignedItem != null)
                {
                    newSlotSaveStruct.pos = new Vector2Int(x, y);
                    newSlotSaveStruct.itemNameInt = slots[x, y].assignedItem.itemSO.GetItemTypeNumber();
                    if (slots[x, y].assignedItem.itemSO is IngredientItemSO)
                        newSlotSaveStruct.itemType =  ItemType.Ingredient;
                    else if (slots[x, y].assignedItem.itemSO is PotionItemSO potionSO)
                    {
                        newSlotSaveStruct.level = slots[x, y].assignedItem.level;
                        if (potionSO is ThrowPotionItemSO)
                            newSlotSaveStruct.itemType = ItemType.ThrowPotion;
                        else
                            newSlotSaveStruct.itemType = ItemType.DrinkPotion;
                    }
                    newSlotSaveStruct.amount = slots[x, y].assignedItemAmount;
                }
                else
                    newSlotSaveStruct.amount = 0;
                _inventoryData.slotDatas[x * 100 + y] = newSlotSaveStruct;
            }
        }
        _inventoryData.SaveJson("Inventory_" + name);
        OnInventoryModified?.Invoke(slots);
    }
    public void Load()
    {
        bool loadSucces = _inventoryData.LoadJson("Inventory_" + name);

        if (loadSucces == false || _inventoryData.slotDatas == null) return;

        int idx = 0;
        for (int y = 0; y < _inventorySize.y; y++)
        {
            for (int x = 0; x < _inventorySize.x; x++)
            {
                InventorySlot slot;
                if (idx < _slotParent.childCount)
                    slot = _slotParent.GetChild(idx).GetComponent<InventorySlot>();
                else
                    slot = Instantiate(InventoryManager.Instance.slotPrefab, _slotParent);
                slot.Init(this);

                SlotSave slotSave = _inventoryData.slotDatas[x * 100 + y];
                if (slotSave != null && slotSave.itemNameInt != -1)
                {
                    Item item = Instantiate(InventoryManager.Instance.itemPrefab);
                    item.Init();
                    switch (slotSave.itemType)
                    {
                        case ItemType.Ingredient:
                            item.itemSO = InventoryManager.Instance.IngredientItemSODict[(IngredientItemType)slotSave.itemNameInt];
                            break;
                        case ItemType.ThrowPotion:
                            item.itemSO = InventoryManager.Instance.ThrowPotionItemSODict[(PotionItemType)slotSave.itemNameInt];
                            break;
                        case ItemType.DrinkPotion:
                            item.itemSO = InventoryManager.Instance.DrinkPotionItemSODict[(PotionItemType)slotSave.itemNameInt];
                            break;
                        default:
                            break;
                    }
                    item.amount = slotSave.amount;
                    item.level = slotSave.level;
                    slot.SetItem(item);
                }
                else if (slot.assignedItem != null)
                {
                    slot.SetItem(null);
                }
                slots[x, y] = slot;

                idx++;
            }
        }
    }


    
    public void SetSelected(InventorySlot slot)
    {
        _description?.SetExplain(slot);
    }
}

public enum ItemType
{
    Ingredient,
    ThrowPotion,
    DrinkPotion,
}

[Serializable]
public class SlotSave
{
    public Vector2Int pos;
    public int itemNameInt = -1;
    public ItemType itemType;
    public int level;
    public int amount;
}

public class InventorySaveData : ISavable<InventorySaveData>
{
    public SlotSave[] slotDatas;

    public void Init(int size)
    {
        slotDatas = new SlotSave[size + 1];
    }

    public void OnLoadData(InventorySaveData classData)
    {
        if (classData == null || classData.slotDatas == null || classData.slotDatas.Length == 0)
        {
            Debug.Log("불러올 데이터가 없습니다.");
            return;
        }

        slotDatas = classData.slotDatas;
    }

    public void OnSaveData(string savedFileName)
    {

    }
}