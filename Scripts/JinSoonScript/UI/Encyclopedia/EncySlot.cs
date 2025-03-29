using Doryu.JBSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EncySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isLocked;

    private Item _assignedPotion;
    private GameObject _select;
    private Image _potionSprite;
    private Encyclopedia _owner;
    private PotionItemSO _potionItemSO;
    private int _idx;

    public void Init(int idx, PotionRecipeListSO potionRecipeListSO, Encyclopedia owner)
    {
        _idx = idx;
        _owner = owner;
        _select = transform.GetChild(0).gameObject;
        PotionRecipeSO potionRecipeSO = potionRecipeListSO.GetRecipeSO(idx);
        if (potionRecipeSO == null) return;
        _potionItemSO = potionRecipeSO.potion;
        Item potion = Instantiate(InventoryManager.Instance.itemPrefab);
        potion.Init();
        potion.itemSO = _potionItemSO;
        potion.amount = int.MaxValue;
        potion.SetSlot(transform);
        _assignedPotion = potion;
        _potionSprite = _assignedPotion.GetComponent<Image>();
        Lock();

        InventoryManager.Instance.OnGetPotion += HandleGetPotionEvent;
    }

    private void HandleGetPotionEvent(PotionItemSO potionSO)
    {
        if (potionSO == _potionItemSO)
        {
            Unlock();
            _owner.slotSaveData.slotLockeds[_idx] = false;
            _owner.slotSaveData.SaveJson("EncySlotData");
        }
    }

    public void Unlock()
    {
        isLocked = false;
        _potionSprite.color = Color.white;
    }
    public void Lock()
    {
        isLocked = true;
        _potionSprite.color = Color.black;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLocked) return;
        _owner.ShowPotionData(_potionItemSO);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isLocked) return;
        _select.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isLocked) return;
        _select.SetActive(false);
    }
}
