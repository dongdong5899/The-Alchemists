using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private PotSlot[] _slots;
    [SerializeField] private PotionRecipeListSO _potionRecipeListSO;
    [SerializeField] private PotionTypeSlider _potionTypeSlider;
    [SerializeField] private TextMeshProUGUI _creatModeText;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].SlotChanged += UpdateCreatable;
        }
    }

    public void UpdateCreatable()
    {
        PotionItemSO potionSO = _potionRecipeListSO.GetPotion(_slots, _potionTypeSlider.GetPotionType());

        if (potionSO == null)
            _image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        else
            _image.color = new Color(1f, 1f, 1f, 1f);
    }

    private void Update()
    {
        if (_slots[0].assignedItem != null && _slots[1].assignedItem != null &&
            _slots[0].assignedItem.itemSO == _slots[1].assignedItem.itemSO)
        {
            _slots[2].gameObject.SetActive(true);
        }
        else
        {
            _slots[2].ReturnItem();
            _slots[2].gameObject.SetActive(false);
        }

        if (_potionTypeSlider.isThrowMode)
            _creatModeText.SetText("ÅõÃ´ Æ÷¼Ç");
        else
            _creatModeText.SetText("¼·Ãë Æ÷¼Ç");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CraftPotion();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.05f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    public void ReturnItem()
    {
        for (int i = 0; i < 3; i++)
        {
            _slots[i].ReturnItem();
        }
        _slots[0].inventorySlot.Save();
    }
    public void ClearItem()
    {
        for (int i = 0; i < 3; i++)
        {
            _slots[i].ClearItem();
        }
        _slots[0].inventorySlot.Save();
    }

    private void CraftPotion()
    {
        PotionItemSO potionSO = _potionRecipeListSO.GetPotion(_slots, _potionTypeSlider.GetPotionType());
        if (potionSO == null) return;


        Item item = Instantiate(InventoryManager.Instance.itemPrefab);
        item.Init();
        item.itemSO = potionSO;
        item.amount = 1;

        int sameItemCount = 0;
        for (int i = 0; i < _slots.Length; i++)
        {
            Item item1 = _slots[i].assignedItem;
            Item item2 = _slots[(i + 1) % _slots.Length].assignedItem;

            if (item1 == null || item2 == null) continue;

            if (item1.itemSO == item2.itemSO)
            {
                sameItemCount++;
            }
        }
        item.level = sameItemCount == 3 ? 2 : sameItemCount;

        bool succes = InventoryManager.Instance.TryAddItem(item);
        if (succes)
        {
            ItemGatherPanel gather = UIManager.Instance.GetUI(UIType.ItemGather) as ItemGatherPanel;
            gather.Init(item.itemSO, item.level);
            gather.Open();

            ClearItem();
        }
        else
        {
            Destroy(item.gameObject);
        }
    }

    private void OnDisable()
    {
        ClearItem();
    }
}
