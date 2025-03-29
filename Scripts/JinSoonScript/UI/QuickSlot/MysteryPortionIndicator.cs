//using DG.Tweening;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class MysteryPortionIndicator : MonoBehaviour
//{
//    [SerializeField] private MysteryPortionInventory _inventory;
//    [SerializeField] private Transform _portionParent;
//    [SerializeField] private RectTransform _slotRect;

//    [SerializeField] private Image coolTimeImage;
//    [SerializeField] private TextMeshProUGUI _timer;
//    [SerializeField] private float _enabledOffset;
//    [SerializeField] private float _disabledOffset;
//    [SerializeField] private float _coolTime = 30f;

//    private float _coolTimeDown;

//    private Player _player;

//    private PortionItem _portion;
//    private PortionItemSO _mysteryPortion;

//    private bool _isSelectedMysteryPortion = false;
//    private Tween _slotTween;

//    private void Awake()
//    {
//        _player = PlayerManager.Instance.Player;
//        _coolTimeDown = _coolTime;
//    }

//    private void OnEnable()
//    {
//        _player.PlayerInput.SelectMysteryPortion += SelectMysteriyPortion;
//        _player.PlayerInput.SelectQuickSlot += UnSelectMysteryPortion;
//        _player.PlayerInput.OnUseQuickSlot += UseMysteryPortion;
//    }

//    private void OnDisable()
//    {
//        _player.PlayerInput.SelectMysteryPortion -= SelectMysteriyPortion;
//        _player.PlayerInput.SelectQuickSlot -= UnSelectMysteryPortion;
//        _player.PlayerInput.OnUseQuickSlot -= UseMysteryPortion;
//    }

//    private void Update()
//    {
//        if (_coolTime > _coolTimeDown)
//        {
//            _coolTimeDown += Time.deltaTime;
//            _timer.SetText($"{(_coolTime - _coolTimeDown).ToString("0.0")}s");

//            if(_coolTime <= _coolTimeDown)
//                _timer.gameObject.SetActive(false);
//        }

//        coolTimeImage.rectTransform.anchoredPosition = _slotRect.anchoredPosition;
//        coolTimeImage.fillAmount = 1 - (_coolTimeDown / _coolTime);
//    }

//    public void ChangePortionImage(ItemSO itemSO)
//    {
//        if (_portion != null)
//            Destroy(_portion.gameObject);

//        _portion = InventoryManager.Instance.MakeItemInstanceByItemSO(itemSO) as PortionItem;

//        if (_portion != null)
//        {
//            _mysteryPortion = itemSO as PortionItemSO;
//            _portion.transform.SetParent(_portionParent);
//        }
//    }

//    private void UseMysteryPortion()
//    {
//        if (_isSelectedMysteryPortion == false || _portion == null) return;
//        if (_coolTimeDown < _coolTime) return;

//        _timer.gameObject.SetActive(true);
//        switch (_portion.portionType)
//        {
//            case Portion.PortionForMyself:
//                PlayerManager.Instance.Player.healthCompo.GetEffort(_portion.portionEffect);
//                break;
//            case Portion.PortionForThrow:
//                PlayerManager.Instance.Player.ThrowPortion(_portion);
//                break;
//            case Portion.Flask:
//                PlayerManager.Instance.Player.WeaponEnchant(_portion);
//                break;
//        }

//        _coolTimeDown = 0;
//    }

//    private void SelectMysteriyPortion()
//    {
//        if (_isSelectedMysteryPortion == true)
//        {
//            _player.throwingPortionSelected = (false);
//            UnSelectMysteryPortion();
//            return;
//        }

//        _player.throwingPortionSelected = (true);
//        _isSelectedMysteryPortion = true;

//        if (_slotTween != null && _slotTween.active)
//            _slotTween.Kill();

//        _slotTween = _slotRect.DOAnchorPosY(_enabledOffset, 0.3f);
//    }

//    private void UnSelectMysteryPortion(int num = 69)
//    {
//        _isSelectedMysteryPortion = false;

//        if (_slotTween != null && _slotTween.active)
//            _slotTween.Kill();

//        _slotTween = _slotRect.DOAnchorPosY(_disabledOffset, 0.3f);
//    }
//}
