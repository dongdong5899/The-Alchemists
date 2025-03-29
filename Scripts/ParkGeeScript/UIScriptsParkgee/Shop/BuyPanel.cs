using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText, _descriptionText;

    public void SetPanelData(ShopItemSO shopItemSO)
    {
        _nameText.SetText(shopItemSO.itemName);
        _descriptionText.SetText(shopItemSO.itemMenual);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
