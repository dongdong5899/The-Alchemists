//using DG.Tweening;
//using UnityEngine;
//using UnityEngine.UI;
//using Doryu.Inventory;

//public class QuickSlotVisual : MonoBehaviour
//{
//    public PortionItemSO AssignedPortion { get; private set; }
//    private PortionItem portion;
//    private Item itemObj;

//    [SerializeField] private float _enableOffset = 20f;
//    [SerializeField] private float _disableOffset = 2f;
//    [SerializeField] private float _delay = 0.5f;

//    private RectTransform rect;
//    private Tween tween;
//    private Vector3 offset = new Vector3(0f, 7f, 0f);

//    private void Awake()
//    {
//        rect = GetComponent<RectTransform>();
//    }

//    public void SetItem(ItemSO item)
//    {
//        //�̹� �������� �Ҵ��������� return
//        if (AssignedPortion != null || item == null) return;

//        AssignedPortion = item as PortionItemSO;

//        itemObj = Instantiate(AssignedPortion.prefab, transform);
//        itemObj.GetComponent<RectTransform>().anchoredPosition = offset;
//        itemObj.transform.SetSiblingIndex(1);
//        portion = itemObj.GetComponent<PortionItem>();

//        //�������̶� �巡�׾ص���� �� �� �־ �������� ����
//        itemObj.GetComponent<Image>().raycastTarget = false;
//        //portion.Init(1, null);
//    }

//    public void UseItem()
//    {
//        //AssignedPortion = null;
//        //portion.RemoveItem(1);

//        //switch(portion.portionType)
//        //{
//        //    case Portion.PortionForMyself:
//        //        PlayerManager.Instance.Player.healthCompo.GetEffort(portion.portionEffect);
//        //        break;
//        //    case Portion.PortionForThrow:
//        //        PlayerManager.Instance.Player.ThrowPortion(portion);
//        //        break;
//        //    case Portion.Flask:
//        //        PlayerManager.Instance.Player.WeaponEnchant(portion);
//        //        break;
//        //}
//    }

//    public void DeleteItem()
//    {
//        AssignedPortion = null;
//        if (itemObj != null) Destroy(itemObj);
//    }

//    public void EnableSlot()
//    {
//        if (tween != null && tween.active)
//            tween.Kill();

//        tween = rect.DOAnchorPosY(_enableOffset, _delay);
//    }

//    public void DisableSlot()
//    {
//        if (tween != null && tween.active)
//            tween.Kill();

//        tween = rect.DOAnchorPosY(_disableOffset, _delay);
//    }
//}
