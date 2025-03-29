//using System.Collections;
//using System.IO;
//using UnityEngine;
//using UnityEngine.UI;

////안쓰는거
//public class MysteryPortionInventory : Inventory
//{
//    private InventorySlot[] _slots;
//    [SerializeField] private MysteryPortionIndicator _indicator;

//    [SerializeField] private int _slotCnt = 3;
//    [SerializeField] private PortionItemSO[] _mysteryPortions;

//    [SerializeField] private GameObject _slotPf;
//    [SerializeField] private Transform _slotParent;

//    private int _openedMysteryPortionCnt = 1;

//    protected override void Awake()
//    {
//        path = Path.Combine(Application.dataPath, "MysteryPortionProgress.txt");

//        _slots = new InventorySlot[_slotCnt];

//        for (int i = 0; i < _slotCnt; i++)
//        {
//            _slots[i] = Instantiate(_slotPf, _slotParent).GetComponent<InventorySlot>();
//            _slots[i].transform.SetSiblingIndex(i);
//            _slots[i].AcceptInsert(false);
//            _slots[i].Init(this);
//        }
//    }

//    //protected override void OnDisable()
//    //{
//    //    Save();
//    //    //for (int i = 0; i < _slots.Length; i++)
//    //    //{
//    //    //    if (_slots[i] != null && _slots[i].assignedItem != null)
//    //    //    {
//    //    //        Destroy(_slots[i].assignedItem.gameObject);
//    //    //    }
//    //    //}

//    //    _isDisabled = false;
//    //}

//    private void OnEnable()
//    {
//        Debug.Log("=============================");
//        Load();
//    }

//    public void UnlockMysteryPortion(int portionCnt)
//    {
//        if (_openedMysteryPortionCnt < portionCnt) _openedMysteryPortionCnt = portionCnt;

//        Item item = InventoryManager.Instance.MakeItemInstanceByItemSO(_mysteryPortions[portionCnt - 1]);
//        item.GetComponent<Image>().raycastTarget = false;
//        _slots[portionCnt - 1].InsertItem(item);
//    }

//    public override void UnSelectAllSlot()
//    {
//        for (int i = 0; i < _slots.Length; i++)
//        {
//            _slots[i].UnSelect();
//        }
//    }

//    public override void SelectItem(Item assignedItem)
//    {
//        if (assignedItem == null || assignedItem.itemSO == null) return;
//        base.SelectItem(assignedItem);
//        _indicator.ChangePortionImage(assignedItem.itemSO);
//    }

//    public override void Save()
//    {
//        File.WriteAllText(path, _openedMysteryPortionCnt.ToString());
//    }

//    public override void Load()
//    {
//        if(File.Exists(path) == false)
//            Save();

//        _openedMysteryPortionCnt = int.Parse(File.ReadAllText(path));

//        StartCoroutine("DelaySetItem");
//    }

//    private IEnumerator DelaySetItem()
//    {
//        yield return null;
//        yield return null;
//        yield return null;

//        for (int i = 1; i <= _openedMysteryPortionCnt; i++)
//            UnlockMysteryPortion(i);
//    }
//}