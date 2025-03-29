using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotManager : Singleton<QuickSlotManager>
{
    [SerializeField] private Transform _passiveLine;
    [SerializeField] private Transform _activeLine;
    [SerializeField] private Inventory _activeSlotInven;
    [SerializeField] private Inventory _passoveSlotInven;

    [field: SerializeField] public Sprite[] slotOutLines { get; private set; }
    [field: SerializeField] public Sprite slotNoneItemOutLine { get; private set; }

    private int _currentSelectIdx = -1;
    private List<QuickSlot> _passiveQuickSlots = new List<QuickSlot>();
    private List<QuickSlot> _activeLineQuickSlots = new List<QuickSlot>();

    public ThrowPotion throwPotion;

    private KeyCode[] _alphaNums =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
    };

    public void SetHandle()
    {
        _passiveLine.GetComponentsInChildren(_passiveQuickSlots);
        _activeLine.GetComponentsInChildren(_activeLineQuickSlots);

        _activeSlotInven.OnInventoryModified += HandleActiveInventoryModified;
        _passoveSlotInven.OnInventoryModified += HandlePassiveInventoryModified;
    }

    private void Update()
    {
        for (int i = 0; i < _alphaNums.Length; i++)
        {
            if (Input.GetKeyDown(_alphaNums[i]))
            {
                if (_currentSelectIdx == i)
                    SelectQuickSlot();
                else
                    SelectQuickSlot(i);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_currentSelectIdx == _activeLineQuickSlots.Count)
                SelectQuickSlot();
            else
                SelectQuickSlot(_activeLineQuickSlots.Count);
        }
    }

    public QuickSlot GetSelectedPotionSlot()
    {
        if (_currentSelectIdx == -1) return null;
        if (_currentSelectIdx == _activeLineQuickSlots.Count)
            return _passiveQuickSlots[0];
        else
            return _activeLineQuickSlots[_currentSelectIdx];
    }

    private void SelectQuickSlot(int index = -1)
    {
        if (_currentSelectIdx != -1)
        {
            if (_currentSelectIdx == _activeLineQuickSlots.Count)
                _passiveQuickSlots[0].OnSelect(false);
            else
                _activeLineQuickSlots[_currentSelectIdx].OnSelect(false);
        }

        _currentSelectIdx = index;

        if (_currentSelectIdx != -1)
        {
            if (_currentSelectIdx == _activeLineQuickSlots.Count)
                _passiveQuickSlots[0].OnSelect(true);
            else
                _activeLineQuickSlots[_currentSelectIdx].OnSelect(true);
        }
    }

    private void HandlePassiveInventoryModified(InventorySlot[,] slots)
    {
        _passiveQuickSlots[0].SetPotion(slots[0, 0]);
        if (_currentSelectIdx == _activeLineQuickSlots.Count && _passiveQuickSlots[0].assignedItem == null)
        {
            SelectQuickSlot();
        }
    }
    private void HandleActiveInventoryModified(InventorySlot[,] slots)
    {
        for (int i = 0; i < 5; i++)
        {
            _activeLineQuickSlots[i].SetPotion(slots[i, 0]);
            if (_currentSelectIdx == i && _activeLineQuickSlots[i].assignedItem == null)
            {
                SelectQuickSlot();
            }
        }
    }
}
