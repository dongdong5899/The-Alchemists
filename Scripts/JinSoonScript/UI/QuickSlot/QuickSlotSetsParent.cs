//using DG.Tweening;
//using UnityEngine;
//using UnityEngine.UI;
//using Doryu.Inventory;

//public class QuickSlotSetsParent : MonoBehaviour
//{
//    public GameObject quickSlotSetPf;

//    //현재 사용가능한 퀵슬롯
//    [HideInInspector] public QuickSlotSet currentQuickSlotSet;

//    public QuickSlotOffset enabledOffset;

//    public int maxQuickSlotCnt = 3;
//    private int nextIndex = 2;

//    private Sequence seq;
//    private Coroutine coroutine;

//    private void Awake()
//    {
//        QuickSlotItems firstSet = QuickSlotManager.Instance.QuickSlots[0];
//        InitQuickSlotSet(firstSet);
//    }

//    public void SetItem(ItemSO item, int slotIdx, int selectedSlot)
//    {
//        //현재 존재하는 퀵슬롯세트에 아이템을 넣어다면 아이템이 넣을게 보이게
//        if (currentQuickSlotSet != null && currentQuickSlotSet.slotNum == slotIdx)
//            currentQuickSlotSet.SetItem(item, selectedSlot);
//    }

//    public void RemoveItem(int slotIdx, int selectedSlot)
//    {
//        if (currentQuickSlotSet != null && currentQuickSlotSet.slotNum == slotIdx)
//            currentQuickSlotSet.RemoveItem(selectedSlot);
//    }

//    /// <summary>
//    /// 현재 퀵슬롯을 인게임에 인스턴스화해줘
//    /// </summary>
//    /// <param name="quickSlotSet"></param>
//    public void SetCurrentQuickSlotSet(QuickSlotItems quickSlotSet)
//    {
//        currentQuickSlotSet = Instantiate(quickSlotSetPf, transform).GetComponent<QuickSlotSet>();
//        currentQuickSlotSet.Init(quickSlotSet);

//        RectTransform rect = currentQuickSlotSet.GetComponent<RectTransform>();
//        Image img = currentQuickSlotSet.GetComponent<Image>();
//        currentQuickSlotSet.transform.SetAsFirstSibling();

//        rect.anchoredPosition = enabledOffset.position;
//        rect.localScale = enabledOffset.scale;
//        img.color = enabledOffset.color;
//    }

//    /// <summary>
//    /// 퀵슬롯의 포션을 전부 소모해서 다음 슬롯으로 넘어가게됨
//    /// </summary>
//    public void GotoNextQuickSlotSet()
//    {
//        QuickSlotItems items = QuickSlotManager.Instance.quickSlots[0];
//        //currentQuickSlotSet을 제거해!
//        currentQuickSlotSet.ChangeQuickSlotSet(items);
//    }

//    /// <summary>
//    /// When Use All Quickslot and move to next quickSlots
//    /// </summary>
//    /// <returns></returns>
//    //private IEnumerator GoNextQuickSlotRoutine()
//    //{
//    //    QuickSlotItems items = QuickSlotManager.Instance.quickSlots[1];
//    //    //currentQuickSlotSet을 제거해!
//    //    currentQuickSlotSet.DisableQuickSlotSet(items);
//    //    //현재 슬롯을 다음 슬롯으로 교체
//    //    currentQuickSlotSet = nextQuickSlotSet;
//    //    yield return new WaitForSeconds(0.4f);

//    //    currentQuickSlotSet.slotNum = 0;
//    //    currentQuickSlotSet.EnableQuickSlotSet(enabledOffset);
//    //    nextQuickSlotSet = null;
//    //    yield return new WaitForSeconds(0.5f);


//    //    if (nextIndex >= maxQuickSlotCnt) yield break;

//    //}

//    /// <summary>
//    /// Initialize QuickSlot
//    /// SetQuickSlots, currentSlot, nextQuickSlotSet
//    /// </summary>
//    public void InitQuickSlotSet(QuickSlotItems firstSlot)
//    {
//        SetCurrentQuickSlotSet(firstSlot);
//        nextIndex = 1;
//    }
//}

//[System.Serializable]
//public struct QuickSlotOffset
//{
//    public Vector3 position;
//    public Vector3 scale;
//    public Color color;
//}
