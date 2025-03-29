using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _costTxt;
    [SerializeField] private Image _iconImage;
    //[SerializeField] private Button _selectBtn;

    public ShopItemSO ShopItemSO { get; private set; }

    private void OnValidate()
    {
        if(ShopItemSO != null)
        {
            SelectItemVisual();
        }
    }

    private void Awake()
    {
        //_selectBtn.onClick.AddListener(SelectItem);
    }

    public void SetItemData(ShopItemSO data)
    {
        ShopItemSO = data;
        SelectItemVisual();
    }

    private void SelectItemVisual()
    {
        if(_costTxt != null)
            _costTxt.text = ShopItemSO.itemPrice;
        if (_iconImage != null)
            _iconImage.sprite = ShopItemSO.itemImg;
    }

    private void SelectItem()
    {
        UIManager.Instance.Close(UIType.Shop);
    }
}
