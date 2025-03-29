//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.UI;
//using static UnityEditor.Progress;

//public class Inventory : MonoBehaviour
//{
//    protected string path = "";
//    private InventorySlot[,] _inventory = new InventorySlot[5, 4];
//    private InventorySlot[,] inventory
//    {
//        get => _inventory;
//        set
//        {
//            _inventory = value;
//            Debug.Log("������");
//        }
//    }
//    private InventorySlot[,] prevInventory = new InventorySlot[5, 4];
//    [SerializeField] private Vector2Int inventorySize = new Vector2Int(5, 4);
//    public Action<ItemSO> OnSelectItem;

//    [SerializeField] private Transform slotParent;
//    [SerializeField] private Transform itemParent;
//    [SerializeField] private Transform selectedItemParent;
//    [SerializeField] private InventorySlot slotPf;

//    [SerializeField] private bool _indicateIngredient;
//    [SerializeField] private bool _indicatePortion;

//    [SerializeField] private List<ItemStruct> _excludingItem = new List<ItemStruct>();

//    [HideInInspector] public Item selectedItem;
//    [HideInInspector] public Item combineableItem;

//    protected bool _isDisabled = false;

//    protected virtual void Awake()
//    {
//        path = Path.Combine(Application.dataPath, "SaveDatas\\Inventory.json");
//        inventory = new InventorySlot[inventorySize.x, inventorySize.y];

//        for (int i = 0; i < inventory.GetLength(1); i++)
//        {
//            for (int j = 0; j < inventory.GetLength(0); j++)
//            {
//                inventory[j, i] = Instantiate(slotPf, slotParent);
//                inventory[j, i].Init(this);
//            }
//        }
//    }

//    private void Start()
//    {
//        StartCoroutine(DelayLoad());
//    }

//    private void Update()
//    {
//    }

//    //protected virtual void OnDisable()
//    //{
//    //    for (int i = 0; i < inventory.GetLength(0); i++)
//    //    {
//    //        for (int j = 0; j < inventory.GetLength(1); j++)
//    //        {
//    //            if (inventory[i, j].assignedItem != null)
//    //            {
//    //                Destroy(inventory[i, j].assignedItem.gameObject);
//    //            }
//    //        }
//    //    }

//    //    _isDisabled = true;
//    //}


//    /// <summary>
//    /// �κ��丮�� �������� ���ų��� �� ����� �Լ��Ӥ���
//    /// </summary>
//    /// <param name="item">�κ��丮�� ���ų��� ������</param>
//    /// <returns>�������� ���� �� return���� true�� �ʵ��� item �ν��Ͻ� �����ָ� �ǰ�
//    /// return���� false�� �ʵ��� item �ν��Ͻ��� ������ ������ ��</returns>
//    public bool TryInsertItem(Item item)
//    {
//        if (item == null) return false;

//        int id = item.itemSO.id;
//        int remainItem = item.itemAmount;

//        for (int i = 0; i < inventory.GetLength(0); i++)
//        {
//            for (int j = 0; j < inventory.GetLength(1); j++)
//            {
//                //���� �������� �ִ��� Ȯ��
//                //���� �������� �ִٸ� �� �������� ��ĭ �ִ� �������� �������Ⱦ�������
//                //�� ĭ�� �־��ֱ�
//                //Debug.Log(inventory + " " + inventory.GetLength(0) + " " + inventory.GetLength(1) + " " + inventory[i, j] + " " + i + " " + j);
//                Item it = inventory[i, j].assignedItem;
//                //Debug.Log("����" + inventory[i, j].GetInstanceID());

//                if (it != null && it.itemSO.id == id && it.itemAmount != it.itemSO.maxCarryAmountPerSlot)
//                {
//                    Debug.Log(it.currentSlot.GetInstanceID() + " | " + j + "," + i + " " + inventory[i, j].GetInstanceID());
//                    int remainSpace = it.itemSO.maxCarryAmountPerSlot - it.itemAmount;
//                    if (remainSpace - remainItem < 0)
//                    {
//                        it.AddItem(remainSpace);

//                        Debug.Log(it.itemAmount + " " + inventory[i, j].assignedItem.itemAmount);

//                        Item newItem = InventoryManager.Instance.MakeItemInstanceByItemSO(it.itemSO, remainItem - remainSpace);
//                        InventoryManager.Instance.PlayerInventory.TryInsertItem(newItem);
//                    }
//                    else
//                    {
//                        it.AddItem(remainItem);
//                        Destroy(item.gameObject);

//                        //Debug.Log(it.itemAmount + " << " + inventory[i, j].assignedItem.itemAmount);

//                        Save();

//                        //InventoryManager.Instance.LoadAllInventory();
//                    }
//                    return true;

//                }
//            }
//        }


//        for (int i = 0; i < inventory.GetLength(1); i++)
//        {
//            for (int j = 0; j < inventory.GetLength(0); j++)
//            {
//                //�� ������ ���� �ʰ� ���� �������� ���⼭ Null�� ĭ�� ã�� �� ��
//                Item it = inventory[j, i].assignedItem;
//                if (it == null)
//                {
//                    inventory[j, i].InsertItem(item);

//                    item.gameObject.SetActive(!_isDisabled);

//                    Save();
//                    //InventoryManager.Instance.LoadAllInventory();
//                    return true;
//                }
//            }
//        }

//        //���� �κ��丮�� �� ���� ���ٸ� return false
//        Save();
//        //InventoryManager.Instance.LoadAllInventory();
//        return false;
//    }

//    //�� id���� ���� �̸����� �޵� ���������ؼ� inventory���� �������� return ����
//    public bool GetItem(int id, int amount, out Item item)
//    {
//        item = null;
//        for (int i = 0; i < inventory.GetLength(0); i++)
//        {
//            for (int j = 0; j < inventory.GetLength(1); j++)
//            {
//                Item it = inventory[i, j].assignedItem;
//                if (it != null && it.itemSO.id == id)
//                {
//                    item = it;
//                    amount -= it.itemAmount;

//                    if (amount <= 0) return true;
//                }
//            }
//        }
//        return amount <= 0;
//    }

//    public virtual void UnSelectAllSlot()
//    {
//        for (int i = 0; i < inventory.GetLength(0); i++)
//        {
//            for (int j = 0; j < inventory.GetLength(1); j++)
//            {
//                inventory[i, j].UnSelect();
//            }
//        }
//    }

//    private IEnumerator DelayLoad()
//    {
//        yield return null;
//        Load();
//    }

//    public virtual void Save()
//    {
//        InventorySaveData saveData = new();

//        for (int i = 0; i < inventory.GetLength(0); i++)
//        {
//            for (int j = 0; j < inventory.GetLength(1); j++)
//            {
//                if (inventory[i, j].assignedItem != null)
//                {
//                    ItemStruct itemS;
//                    itemS.amount = inventory[i, j].assignedItem.itemAmount;
//                    itemS.id = inventory[i, j].assignedItem.itemSO.id;
//                    itemS.itemPos = new Vector2Int(i, j);

//                    saveData.inventory.Add(itemS);
//                }
//            }
//        }

//        _excludingItem.ForEach(item => saveData.inventory.Add(item));

//        string json = JsonUtility.ToJson(saveData, true);
//        File.WriteAllText(path, json);
//    }

//    public virtual void Load()
//    {
//        if (!File.Exists(path))
//        {
//            Save();
//            return;
//        }

//        Debug.Log("sdds");

//        string json = File.ReadAllText(path);
//        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(json);
//        _excludingItem = new List<ItemStruct>();

//        //���� �ִ°� �� �����
//        for (int i = 0; i < inventory.GetLength(1); i++)
//        {
//            for (int j = 0; j < inventory.GetLength(0); j++)
//            {
//                if (inventory[j, i].assignedItem != null)
//                    Destroy(inventory[j, i].assignedItem.gameObject);

//                inventory[j, i].DeleteItem();
//            }
//        }

//        //saveData�� �ִ� �������� �����
//        for (int i = 0; i < saveData.inventory.Count; i++)
//        {
//            ItemStruct itemStruct = saveData.inventory[i];
//            int id = itemStruct.id;
//            ItemSO itemSO = InventoryManager.Instance.ItemSet.GetItem(id);
//            //Debug.Log(id);

//            if (id == -1 || itemSO == null) continue;

//            if(itemSO.itemType == ItemType.Ingredient && !_indicateIngredient)
//            {
//                _excludingItem.Add(itemStruct);
//                continue;
//            }
//            if (itemSO.itemType == ItemType.Portion && !_indicatePortion)
//            {
//                _excludingItem.Add(itemStruct);
//                continue;
//            }

//            int amount = itemStruct.amount;
//            Vector2Int pos = itemStruct.itemPos;

//            //Debug.Log(inventory.GetLength(0) + " / " + inventory.GetLength(1));
//            //Debug.Log(pos.x + " | " + pos.y);
//            if (inventory.GetLength(0) <= pos.x || inventory.GetLength(1) <= pos.y) continue;

//            //Debug.Log("���� ����");
//            Item it = Instantiate(itemSO.prefab, itemParent).GetComponent<Item>();
//            //Debug.Log(it);

//            it.Init(amount, inventory[pos.x, pos.y]);
//            inventory[pos.x, pos.y].InsertItem(it);
//            //if (inventory[pos.x, pos.y].assignedItem != null)
//                //Debug.Log(inventory[pos.x, pos.y].assignedItem.itemAmount + " >> " + inventory[pos.x, pos.y].assignedItem.transform.GetComponent<Image>().GetInstanceID() + " <<");
//        }
//    }

//    public virtual void SelectItem(Item assignedItem)
//    {
//        if (assignedItem == null)
//        {
//            selectedItem?.transform.SetParent(itemParent);
//        }
//        else
//        {
//            assignedItem.transform.SetParent(selectedItemParent);
//        }

//        selectedItem = assignedItem;
//    }

//}

//[Serializable]
//public class InventorySaveData
//{
//    public List<ItemStruct> quickSlot = new();
//    public List<ItemStruct> inventory = new();
//}

//[System.Serializable]
//public struct ItemStruct
//{
//    public int id;
//    public int amount;
//    public Vector2Int itemPos;
//}