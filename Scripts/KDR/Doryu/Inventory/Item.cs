using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private TextMeshProUGUI _textOutLine;
    private Image _image;
    private RectTransform _rectTrm;
    private int _amount;
    public int amount
    {
        get => _amount;
        set
        {
            _amount = value;
            if (_amount < 100)
                TextUpdate();
        }
    }

    private ItemSO _itemSO;
    public ItemSO itemSO
    {
        get => _itemSO;
        set
        {
            _itemSO = value;
            _image.sprite = _itemSO.image;
        }
    }

    [HideInInspector] public int level;

    public void TextUpdate()
    {
        _text.SetText("x" + amount.ToString());
        _textOutLine.SetText("x" + amount.ToString());
    }

    public void Init()
    {
        _rectTrm = transform as RectTransform;
        _text = transform.Find("AmountText").GetComponent<TextMeshProUGUI>();
        _textOutLine = transform.Find("AmountTextOutLine").GetComponent<TextMeshProUGUI>();
        _image = GetComponent<Image>();
    }

    public void SetSlot(Transform trm)
    {
        transform.SetParent(trm);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        if (_itemSO is PotionItemSO)
            _rectTrm.sizeDelta = Vector2.one * 80;
        else if (_itemSO is IngredientItemSO)
            _rectTrm.sizeDelta = Vector2.one * 110;
    }
    public void SetSlot()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        if (_itemSO is PotionItemSO)
            _rectTrm.sizeDelta = Vector2.one * 80;
        else if (_itemSO is IngredientItemSO)
            _rectTrm.sizeDelta = Vector2.one * 110;
    }
}

