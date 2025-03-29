using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionArea : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI explain;

    private RectTransform _rectTrm;
    private InventorySlot _selectedSlot;

    private void Awake()
    {
        _rectTrm = icon.transform as RectTransform;
    }

    public void SetExplain(InventorySlot slot)
    {
        if (_selectedSlot != null)
        {
            _selectedSlot.isSelected = false;
            _selectedSlot.OnMouse(false);
            _selectedSlot.OnSelect(false);
        }
        if (slot == null || _selectedSlot == slot || slot.assignedItem == null)
        {
            _selectedSlot = null;
            itemName.SetText("");
            explain.SetText("");
            icon.color = new Color(1, 1, 1, 0);
            return;
        }
        if (_selectedSlot != slot)
        {
            _selectedSlot = slot;
            _selectedSlot.isSelected = true;
            _selectedSlot.OnMouse(true);
            _selectedSlot.OnSelect(true);
        }

         
        icon.color = new Color(1, 1, 1, 1);
        ItemSO itemSO = slot.assignedItem.itemSO;
        itemName.SetText(itemSO.GetItemName(slot.assignedItem.level));
        explain.SetText(itemSO.GetItemDescription(slot.assignedItem.level));
        icon.sprite = itemSO.image;
        _rectTrm.sizeDelta = Vector2.one * (itemSO is IngredientItemSO ? 350f : 200);
    }
} 
