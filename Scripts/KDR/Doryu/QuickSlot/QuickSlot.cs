using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    private Transform _itemParent;
    private RectTransform _rectTrm;
    private Image _coolTimeImage;
    private TextMeshProUGUI _coolTimeText;
    private Image _slotImage;
    private InventorySlot _invenSlot;
    private Sequence _seq;
    private Vector2 _defaultPos;

    public Item assignedItem { get; private set; }

    private bool isAwaked = false;

    private void Init()
    {
        _itemParent = transform.Find("PotionParent");
        _coolTimeImage = transform.Find("CoolTime").GetComponent<Image>();
        _coolTimeText = _coolTimeImage.transform.Find("Timer").GetComponent<TextMeshProUGUI>();
        _slotImage = GetComponent<Image>();
        _rectTrm = transform as RectTransform;
        _defaultPos = _rectTrm.anchoredPosition;

        _coolTimeImage.fillAmount = 0;
        _coolTimeText.text = "";
        _slotImage.sprite = QuickSlotManager.Instance.slotNoneItemOutLine;
        OnSelect(false);
    }

    public void OnSelect(bool on)
    {
        if (_seq != null && _seq.IsActive()) _seq.Kill();
        _seq = DOTween.Sequence();
        if (on)
            _seq.Append(_rectTrm.DOAnchorPos(_defaultPos + new Vector2(0f, 30f), 0.15f).SetEase(Ease.OutQuint));
        else
            _seq.Append(_rectTrm.DOAnchorPos(_defaultPos, 0.15f).SetEase(Ease.OutBounce));
    }

    public void SetPotion(InventorySlot inventorySlot)
    {
        if (isAwaked == false)
        {
            Init();
            isAwaked = true;
        }

        _invenSlot = inventorySlot;
        if (_invenSlot == null || _invenSlot.assignedItem == null)
        {
            if (assignedItem != null)
            {
                Destroy(assignedItem.gameObject);
                assignedItem = null;
                _slotImage.sprite = QuickSlotManager.Instance.slotNoneItemOutLine;
                OnSelect(false);
            }
            return;
        }
        if (assignedItem == null)
        {
            assignedItem = Instantiate(InventoryManager.Instance.itemPrefab, _itemParent);
            assignedItem.Init();
        }
        assignedItem.amount = _invenSlot.assignedItem.amount;
        assignedItem.itemSO = _invenSlot.assignedItem.itemSO;
        assignedItem.level = _invenSlot.assignedItem.level;
        assignedItem.SetSlot();
        PotionItemSO potionItemSO = assignedItem.itemSO as PotionItemSO;
        if (potionItemSO.quickSlotOutLine == QuickSlotOutLine.Level)
            _slotImage.sprite = QuickSlotManager.Instance.slotOutLines[assignedItem.level];
        else
            _slotImage.sprite = QuickSlotManager.Instance.slotOutLines[(int)potionItemSO.quickSlotOutLine];
    }

    public bool TryUsePotion()
    {
        return _invenSlot.TrySubAmount();
    }
}
