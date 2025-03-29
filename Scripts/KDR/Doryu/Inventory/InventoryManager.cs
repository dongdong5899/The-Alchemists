using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventoryManager>();
                if ( _instance == null )
                {
                    Debug.LogWarning("싱글톤 오브젝트 안넣었어요");
                }
            }
            return _instance;
        }
    }

    public Dictionary<IngredientItemType, ItemSO> IngredientItemSODict { get; private set; } 
        = new Dictionary<IngredientItemType, ItemSO>();
    public Dictionary<PotionItemType, ItemSO> ThrowPotionItemSODict { get; private set; } 
        = new Dictionary<PotionItemType, ItemSO>();
    public Dictionary<PotionItemType, ItemSO> DrinkPotionItemSODict { get; private set; } 
        = new Dictionary<PotionItemType, ItemSO>();
    [field:SerializeField] public InventorySlot slotPrefab { get; private set; }
    [field:SerializeField] public Item itemPrefab { get; private set; }
    [SerializeField] private ItemListSO itemListSO;
    [SerializeField] private Inventory ingredientInventory;
    [SerializeField] private Inventory throwPotionInventory;
    [SerializeField] private Inventory drinkPotionInventory;
    [SerializeField] private Inventory gimmickPotionInventory;

    private List<Inventory> _inventories;

    public Action<PotionItemSO> OnGetPotion;

    [HideInInspector] public InventorySlot dragItemSlot;
    [HideInInspector] public InventorySlot stayMouseSlot;

    private void Awake()
    {

        foreach (IngredientItemSO itemSO in itemListSO.ingredientItemSOList)
        {
            IngredientItemSODict.Add(itemSO.itemType, itemSO);
        }
        foreach (ThrowPotionItemSO itemSO in itemListSO.throwPotionItemSOList)
        {
            ThrowPotionItemSODict.Add(itemSO.itemType, itemSO);
        }
        foreach (DrinkPotionItemSO itemSO in itemListSO.drinkPotionItemSOList)
        {
            DrinkPotionItemSODict.Add(itemSO.itemType, itemSO);
        }

        QuickSlotManager.Instance.SetHandle();

        _inventories = new List<Inventory>();
        _inventories = FindObjectsByType<Inventory>(FindObjectsSortMode.None).ToList();

        _inventories.ForEach(inven =>
        {
            //Debug.Log(inven.name);
            inven.Init();
        });
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            TryAddItem(ThrowPotionItemSODict[PotionItemType.StonePotion]);
        }
#endif
        if (dragItemSlot != null && dragItemSlot.assignedItem != null)
        {
            DragUpdate();
        }
    }

    public void DragUpdate()
    {
        Vector2 mousePos = Input.mousePosition;
        dragItemSlot.assignedItem.transform.localPosition = 
            mousePos - (Vector2)dragItemSlot.inventory.itemStorage.position;
    }

    public void AddGimmickPotion(PotionItemSO itemSO, bool openPannel = false)
    {
        bool succes = gimmickPotionInventory.AddItem(itemSO, int.MaxValue, 0);

        if (openPannel && succes)
        {
            ItemGatherPanel gather = UIManager.Instance.GetUI(UIType.ItemGather) as ItemGatherPanel;
            gather.Init(itemSO);
            gather.Open();
        }
    }

    public bool TryAddItem(Item item, bool openPannel = false)
    {
        bool succes = false;
        if (item.itemSO is IngredientItemSO)
        {
            succes = ingredientInventory.AddItem(item);
        }
        else if (item.itemSO is ThrowPotionItemSO throwPotionSO)
        {
            OnGetPotion?.Invoke(throwPotionSO);
            succes = throwPotionInventory.AddItem(item);
        }
        else if (item.itemSO is DrinkPotionItemSO drinkPotionSO)
        {
            OnGetPotion?.Invoke(drinkPotionSO);
            succes = drinkPotionInventory.AddItem(item);
        }

        if (openPannel && succes)
        {
            ItemGatherPanel gather = UIManager.Instance.GetUI(UIType.ItemGather) as ItemGatherPanel;
            gather.Init(item.itemSO);
            gather.Open();
        }

        return succes;
    }
    public bool TryAddItem(ItemSO itemSO, int amount = 1, int level = 0, bool openPannel = false)
    {
        bool succes = false;
        if (itemSO is IngredientItemSO)
        {
            succes = ingredientInventory.AddItem(itemSO, amount, level);
        }
        else if (itemSO is ThrowPotionItemSO throwPotionSO)
        {
            OnGetPotion?.Invoke(throwPotionSO);
            succes = throwPotionInventory.AddItem(itemSO, amount, level);
        }
        else if (itemSO is DrinkPotionItemSO drinkPotionSO)
        {
            OnGetPotion?.Invoke(drinkPotionSO);
            succes = drinkPotionInventory.AddItem(itemSO, amount, level);
        }

        if (openPannel && succes)
        {
            ItemGatherPanel gather = UIManager.Instance.GetUI(UIType.ItemGather) as ItemGatherPanel;
            gather.Init(itemSO);
            gather.Open();
        }

        return succes;
    }

    [ContextMenu("ResetSaveData")]
    public void ResetSaveDate()
    {
        if (_inventories == null)
        {
            _inventories = new List<Inventory>();
            _inventories = FindObjectsByType<Inventory>(FindObjectsSortMode.None).ToList();
        }

        _inventories.ForEach(inven => inven.ResetSaveData());
    }
}

