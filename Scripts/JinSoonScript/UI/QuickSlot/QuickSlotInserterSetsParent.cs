//using DG.Tweening;
//using System;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class QuickSlotInserterSetsParent : MonoBehaviour
//{
//    public int maxQuickSlotCnt = 0;
//    public GameObject quickSlotSetPf;

//    public QuickSlotOffset quickSlotOffset;

//    private TextMeshProUGUI curSlotNumTxt;

//    private QuickSlotInserterSet curQuickSlot;
//    private QuickSlotInserterSet nextQuickSlot;
//    private int curSlotNum = 0;

//    private Vector2 originPos = new Vector2(0f, 25f);
//    private Vector2 downPeek = new Vector2(40f, -90f);
//    private Vector2 upPeek = new Vector2(-40f, 140f);
//    private Color disableColor = new Color(0.8f, 0.8f, 0.8f, 1f);

//    public QuickSlotInserterSet CurQuickSlot => curQuickSlot;
//    public QuickSlotInserterSet NextQuickSlot => nextQuickSlot;

//    private void Awake()
//    {
//        curSlotNumTxt = transform.Find("CurrentSlotNum").GetComponent<TextMeshProUGUI>();
//        Init();
//    }

//    public void GoSlotNumUp()
//    {
//        curSlotNum++;

//        if (curSlotNum >= maxQuickSlotCnt)
//            curSlotNum = 0;

//        curSlotNumTxt.SetText($"{curSlotNum + 1}");

//        nextQuickSlot.Init(curSlotNum);

//        curQuickSlot.GoDisable(upPeek, originPos, disableColor);
//        nextQuickSlot.GoEnable(originPos, Color.white);

//        QuickSlotInserterSet temp = curQuickSlot;
//        curQuickSlot = nextQuickSlot;
//        nextQuickSlot = temp;
//    }

//    public void GoSlotNumDown()
//    {
//        curSlotNum--;

//        if (curSlotNum < 0)
//            curSlotNum = maxQuickSlotCnt - 1;

//        curSlotNumTxt.SetText($"{curSlotNum + 1}");

//        nextQuickSlot.Init(curSlotNum);
//        curQuickSlot.GoDisable(downPeek, originPos, disableColor);
//        nextQuickSlot.GoEnable(originPos, Color.white);

//        QuickSlotInserterSet temp = curQuickSlot;
//        curQuickSlot = nextQuickSlot;
//        nextQuickSlot = temp;
//    }

//    public void RemoveItem(int slotIdx, int selectedSlot, bool removeInstance)
//    {
//        if (curQuickSlot != null && curQuickSlot.SlotIdx == slotIdx)
//            curQuickSlot.inserter[selectedSlot].RemoveItem(removeInstance);

//        if (nextQuickSlot != null && nextQuickSlot.SlotIdx == slotIdx)
//            nextQuickSlot.inserter[selectedSlot].RemoveItem(removeInstance);
//    }

//    public void SetItem(ItemSO item, int slotIdx, int selectedSlot)
//    {
//        if (curQuickSlot != null && curQuickSlot.SlotIdx == slotIdx)
//        {
//            Item itemInstance = InventoryManager.Instance.MakeItemInstanceByItemSO(item);
//            curQuickSlot.inserter[selectedSlot].InsertItem(itemInstance);
//        }

//        if (nextQuickSlot != null && nextQuickSlot.SlotIdx == slotIdx)
//        {
//            Item itemInstance = InventoryManager.Instance.MakeItemInstanceByItemSO(item);
//            nextQuickSlot.inserter[selectedSlot].InsertItem(itemInstance);
//        }
//    }

//    public void Init()
//    {
//        maxQuickSlotCnt = QuickSlotManager.Instance.MaxQuickSlotCnt;
//        nextQuickSlot = GetQuickSlot();
//        curQuickSlot = GetQuickSlot();
//    }

//    public QuickSlotInserterSet GetQuickSlot()
//    {
//        QuickSlotInserterSet quickSlotSet =
//            Instantiate(quickSlotSetPf, transform).GetComponent<QuickSlotInserterSet>();
//        quickSlotSet.Init(curSlotNum);

//        RectTransform rect = quickSlotSet.GetComponent<RectTransform>();
//        Image img = rect.GetComponent<Image>();

//        rect.anchoredPosition = quickSlotOffset.position;
//        rect.localScale = quickSlotOffset.scale;
//        img.color = quickSlotOffset.color;

//        return quickSlotSet;
//    }

//    public void GotoNextQuickSlotSet()
//    {
//        Debug.Log(curQuickSlot);
//        curQuickSlot.Init(curQuickSlot.SlotIdx);
//        nextQuickSlot.Init(nextQuickSlot.SlotIdx);
//    }
//}
