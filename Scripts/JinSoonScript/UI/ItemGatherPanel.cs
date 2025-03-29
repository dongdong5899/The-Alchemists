using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemGatherPanel : MonoBehaviour, IManageableUI
{
    private ItemSO _item;

    [SerializeField] private CanvasGroup _bgGroup;
    private RectTransform _bgRect;

    [SerializeField] private TextMeshProUGUI _nameTxt;
    [SerializeField] private TextMeshProUGUI _explainTxt;
    [SerializeField] private Image _itemImage;

    private Sequence _seq;
    private bool _isOpen = false;

    private void Update()
    {
        //아무 키보드  Keyboard.current.anyKey.wasPressedThisFrame
        if (_isOpen && Input.GetMouseButtonDown(0))
            Close();
    }

    public void Close()
    {
        _isOpen = false;
        if (_seq != null && _seq.active)
            _seq.Kill();

        _seq = DOTween.Sequence();

        _seq.Join(_bgGroup.DOFade(0f, 0.5f))
            .Join(_bgRect.DOAnchorPosY(-100f, 0.5f));

        if ((UIManager.Instance.GetUI(UIType.PotionCraft) as PotionCraftPanel).isOpen == false)
        {
            PlayerManager.Instance.EnablePlayerMovementInput();
            PlayerManager.Instance.EnablePlayerInventoryInput();
        }
        _bgGroup.blocksRaycasts = false;
    }

    public void Open()
    {
        AudioManager.Instance.PlaySound(SoundEnum.GetItem, PlayerManager.Instance.PlayerTrm);

        if (_seq != null && _seq.active)
            _seq.Kill();

        _seq = DOTween.Sequence();

        _seq.Join(_bgGroup.DOFade(1f, 0.5f))
            .Join(_bgRect.DOAnchorPosY(0f, 0.5f))
            .OnComplete(() => _isOpen = true);
        PlayerManager.Instance.DisablePlayerMovementInput();
        PlayerManager.Instance.DisablePlayerInventoryInput();
        _bgGroup.blocksRaycasts = true;
    }

    public void Init(ItemSO item, int level = 0)
    {
        _item = item;
        _nameTxt.SetText(_item.GetItemName(level));
        _explainTxt.SetText(_item.GetItemDescription(level));
        _itemImage.sprite = _item.image;
    }

    public void Init()
    {
        _bgRect = _bgGroup.GetComponent<RectTransform>();

        _bgGroup.alpha = 0f;
        _bgRect.anchoredPosition = new Vector2(_bgRect.anchoredPosition.x, -100);
    }
}
