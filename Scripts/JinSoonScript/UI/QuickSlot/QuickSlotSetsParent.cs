//using DG.Tweening;
//using UnityEngine;
//using UnityEngine.UI;
//using Doryu.Inventory;

//public class QuickSlotSetsParent : MonoBehaviour
//{
//    public GameObject quickSlotSetPf;

//    //���� ��밡���� ������
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
//        //���� �����ϴ� �����Լ�Ʈ�� �������� �־�ٸ� �������� ������ ���̰�
//        if (currentQuickSlotSet != null && currentQuickSlotSet.slotNum == slotIdx)
//            currentQuickSlotSet.SetItem(item, selectedSlot);
//    }

//    public void RemoveItem(int slotIdx, int selectedSlot)
//    {
//        if (currentQuickSlotSet != null && currentQuickSlotSet.slotNum == slotIdx)
//            currentQuickSlotSet.RemoveItem(selectedSlot);
//    }

//    /// <summary>
//    /// ���� �������� �ΰ��ӿ� �ν��Ͻ�ȭ����
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
//    /// �������� ������ ���� �Ҹ��ؼ� ���� �������� �Ѿ�Ե�
//    /// </summary>
//    public void GotoNextQuickSlotSet()
//    {
//        QuickSlotItems items = QuickSlotManager.Instance.quickSlots[0];
//        //currentQuickSlotSet�� ������!
//        currentQuickSlotSet.ChangeQuickSlotSet(items);
//    }

//    /// <summary>
//    /// When Use All Quickslot and move to next quickSlots
//    /// </summary>
//    /// <returns></returns>
//    //private IEnumerator GoNextQuickSlotRoutine()
//    //{
//    //    QuickSlotItems items = QuickSlotManager.Instance.quickSlots[1];
//    //    //currentQuickSlotSet�� ������!
//    //    currentQuickSlotSet.DisableQuickSlotSet(items);
//    //    //���� ������ ���� �������� ��ü
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
