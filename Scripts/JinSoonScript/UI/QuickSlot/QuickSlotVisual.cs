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
//        //이미 아이템이 할당해있으면 return
//        if (AssignedPortion != null || item == null) return;

//        AssignedPortion = item as PortionItemSO;

//        itemObj = Instantiate(AssignedPortion.prefab, transform);
//        itemObj.GetComponent<RectTransform>().anchoredPosition = offset;
//        itemObj.transform.SetSiblingIndex(1);
//        portion = itemObj.GetComponent<PortionItem>();

//        //아이템이라서 드래그앤드롭이 될 수 있어서 안잡히게 해줘
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
