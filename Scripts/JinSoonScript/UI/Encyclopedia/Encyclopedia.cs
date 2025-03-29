using Doryu.JBSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncySlotSaveData : ISavable<EncySlotSaveData>
{
    public bool[] slotLockeds;

    public void OnLoadData(EncySlotSaveData classData)
    {
        slotLockeds = classData.slotLockeds;
    }

    public void OnSaveData(string savedFileName)
    {

    }
}

public class Encyclopedia : MonoBehaviour
{
    [SerializeField] private EncyPotionRecipe _encyPotionRecipe;
    [SerializeField] private EncySlot[] _potionSlots;

    public EncySlotSaveData slotSaveData { get; private set; } = new EncySlotSaveData();

    [SerializeField] private PotionRecipeListSO _potionRecipeListSO;
    [SerializeField] private ItemListSO _itemListSO;

    private void Awake()
    {
        _encyPotionRecipe.SetData();

        for (int i = 0; i < _potionSlots.Length; i++)
        {
            _potionSlots[i].Init(i, _potionRecipeListSO, this);
        }

        slotSaveData.slotLockeds = new bool[_potionSlots.Length];
        for (int i = 0; i < slotSaveData.slotLockeds.Length; i++)
        {
            slotSaveData.slotLockeds[i] = true;
        }

        slotSaveData.LoadJson("EncySlotData");
        for (int i = 0; i < _potionSlots.Length; i++)
        {
            if (slotSaveData.slotLockeds[i] == false)
            {
                _potionSlots[i].Unlock();
            }
        }
    }

    public void ShowPotionData(PotionItemSO potionItemSO)
    {
        _encyPotionRecipe.SetData(_potionRecipeListSO, _itemListSO, potionItemSO);
    }
}
