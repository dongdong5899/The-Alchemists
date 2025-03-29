//using UnityEngine;
//using Doryu.Inventory;

//public class QuickSlotManager : Singleton<QuickSlotManager>
//{
//    public int maxQuickSlotCnt = 3;
//    public int MaxQuickSlotCnt
//    {
//        get
//        {
//            return maxQuickSlotCnt;
//        }
//        set
//        {
//            maxQuickSlotCnt = value;
//            //quickSlotInserterSetParent.maxQuickSlotCnt = value;
//            quickSlotSetParent.maxQuickSlotCnt = value;
//        }
//    }

//    //�����ͻ� �����ϴ� �����Կ� �Ҵ�� �ִ� �����۵�
//    [HideInInspector] public QuickSlotItems[] quickSlots;

//    public QuickSlotItems[] QuickSlots
//    {
//        get
//        {
//            if(quickSlots == null || quickSlots.Length <= 0)
//            {
//                quickSlots = new QuickSlotItems[maxQuickSlotCnt];

//                for (int i = 0; i < maxQuickSlotCnt; i++)
//                    quickSlots[i] = new QuickSlotItems(i);
//            }

//            return quickSlots;
//        }
//        private set
//        {
//            quickSlots = value;
//        }
//    }

//    //public QuickSlotInserterSetsParent quickSlotInserterSetParent;
//    public QuickSlotSetsParent quickSlotSetParent;

//    public QuickSlotItems GetNextQuickSlot()
//    {
//        //������ �� ������ �� ������ �� ������ ���ְ� ������ ��ĭ�� ����
//        //�� ������ ���� �������
//        QuickSlotItems[] temp = quickSlots;

//        for (int i = 1; i < temp.Length; i++)
//            quickSlots[i - 1] = temp[i];

//        quickSlots[quickSlots.Length - 1] = new QuickSlotItems(quickSlots.Length - 1);
//        //quickSlotInserterSetParent.CurQuickSlot.Init(0);
//        //quickSlotInserterSetParent.NextQuickSlot.Init(1);

//        return quickSlots[0];
//    }

//    public void MoveToNextQuickSlot()
//    {
//        //�̰� ������ ���⤾��
//        // �ڷε� �����
//        QuickSlotItems[] temp = quickSlots;

//        for (int i = 1; i < temp.Length; i++)
//            quickSlots[i - 1] = temp[i];
//        quickSlots[quickSlots.Length - 1] = new QuickSlotItems(quickSlots.Length - 1);

//        quickSlotSetParent.GotoNextQuickSlotSet();
//        //quickSlotInserterSetParent.GotoNextQuickSlotSet();
//    }

//    public void RemoveItem(int slotIdx, int selectedSlot, bool removeInstance)
//    {
//        quickSlots[slotIdx].items[selectedSlot] = null;
//        quickSlotSetParent.RemoveItem(slotIdx, selectedSlot);
//        //quickSlotInserterSetParent.RemoveItem(slotIdx, selectedSlot, removeInstance);
//    }

//    public void InsertItem(int slotIdx, int selectedSlot, ItemSO item)
//    {
//        quickSlots[slotIdx].items[selectedSlot] = item;
//        quickSlotSetParent.SetItem(item, slotIdx, selectedSlot);
//    }
//}

//public class QuickSlotItems
//{
//    public int itemIdx;
//    public ItemSO[] items = new ItemSO[5];

//    public QuickSlotItems(int idx)
//    {
//        itemIdx = idx;
//        items = new ItemSO[5];
//    }
//}