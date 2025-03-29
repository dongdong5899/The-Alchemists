using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionInventoryTogleUI : MonoBehaviour
{
    [SerializeField] private Image _throwModeImage;
    [SerializeField] private Image _drinkModeImage;
    [SerializeField] private Button _throwModeBtn;
    [SerializeField] private Button _drinkModeBtn;
    [SerializeField] private Inventory _throwInven;
    [SerializeField] private Inventory _drinkInven;

    [SerializeField] private TextMeshProUGUI _currentInventoryTypeText;
    [SerializeField] private Sprite[] _changeBtnBack;

    private Inventory _currentInven;

    private void Start()
    {
        _currentInven = _throwInven;
        _drinkInven.SetActive(false);
        _throwModeBtn.onClick.AddListener(() =>
        {
            _throwModeImage.sprite = _changeBtnBack[0];
            _drinkModeImage.sprite = _changeBtnBack[1];
            ChangeInvenView(_throwInven);
            _currentInventoryTypeText.SetText("ÅõÃ´ Æ÷¼Ç");
        });
        _drinkModeBtn.onClick.AddListener(() =>
        {
            _drinkModeImage.sprite = _changeBtnBack[0];
            _throwModeImage.sprite = _changeBtnBack[1];
            ChangeInvenView(_drinkInven);
            _currentInventoryTypeText.SetText("¼·Ãë Æ÷¼Ç");
        });
    }

    private void ChangeInvenView(Inventory inven)
    {
        _currentInven.SetSelected(null);
        _currentInven.SetActive(false);
        _currentInven = inven;
        _currentInven.SetActive(true);
    }
}
