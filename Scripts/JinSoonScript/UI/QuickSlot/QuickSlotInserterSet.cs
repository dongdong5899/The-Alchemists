//using DG.Tweening;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class QuickSlotInserterSet : MonoBehaviour
//{
//    public readonly QuickSlotInserter[] inserter = new QuickSlotInserter[5];
//    private int _slotIdx;
//    private RectTransform inserterSetRect;
//    private Image[] inserterImages;

//    public bool isEnable = false;
//    public int SlotIdx => _slotIdx;

//    private Sequence seq;

//    private void Awake()
//    {
//        for (int i = 0; i < 5; i++)
//        {
//            inserter[i] = transform.GetChild(i).GetComponent<QuickSlotInserter>();
//            inserter[i].Init(i);
//        }

//        inserterSetRect = GetComponent<RectTransform>();
//        inserterImages = GetComponentsInChildren<Image>();
//    }

//    public void GoDisable(Vector2 peek, Vector2 origin, Color disableColor)
//    {
//        if (seq != null && seq.IsActive())
//            seq.Kill();

//        seq = DOTween.Sequence();

//        seq.Append(inserterSetRect.DOAnchorPos(peek, 0.3f))
//            .Join(inserterSetRect.DOScale(1.1f, 0.3f))
//            .AppendCallback(() => transform.SetAsFirstSibling())
//            .Append(inserterSetRect.DOAnchorPos(origin, 0.2f))
//            .Join(inserterSetRect.DOScale(1f, 0.2f))
//            .OnStart(() =>
//            {
//                for (int i = 0; i < inserter.Length; i++)
//                {
//                    if (inserter[i].assignedItem != null)
//                        inserter[i].assignedItem.transform.SetParent(transform);
//                }
//            })
//            .OnComplete(() =>
//            {
//                for (int i = 0; i < inserter.Length; i++)
//                {
//                    if (inserter[i].assignedItem != null)
//                        Destroy(inserter[i].assignedItem.gameObject);

//                    inserter[i].UnSelect();
//                }
//            });

//        foreach (var item in inserterImages)
//            seq.Join(item.DOColor(disableColor, 0.5f));
//    }

//    public void GoEnable(Vector2 origin, Color enableColor)
//    {
//        if (seq != null && seq.IsActive())
//            seq.Kill();

//        seq = DOTween.Sequence();

//        foreach (var item in inserterImages)
//            seq.Join(item.DOColor(enableColor, 0.5f));

//        seq.Join(inserterSetRect.DOScale(1f, 0.5f))
//            .OnComplete(() =>
//            {
//                Transform itemParent = InventoryManager.Instance.ItemParent;
//                for (int i = 0; i < inserter.Length; i++)
//                {
//                    if (inserter[i].assignedItem != null)
//                        inserter[i].assignedItem.transform.SetParent(itemParent);
//                }
//            });

//    }

//    public void RemoveItem()
//    {

//    }

//    public void Init(int slotIdx)
//    {
//        _slotIdx = slotIdx;

//        QuickSlotItems items = QuickSlotManager.Instance.QuickSlots[_slotIdx];
//        for (int i = 0; i < items.items.Length; i++)
//        {
//            if (inserter[i].assignedItem != null)
//                inserter[i].RemoveItem(true);

//            if (items.items[i] == null) continue;

//            Item item =
//                InventoryManager.Instance.MakeItemInstanceByItemSO(items.items[i]);

//            if (item != null)
//            {
//                inserter[i].InsertItem(item);
//                item.transform.SetParent(transform);
//            }
//        }
//    }
//}
