using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private BuyPanel _clickPanel;
    //[SerializeField] private Image _selectedItemOn;
    //[SerializeField, TextArea] private string _menual;
    //[SerializeField] private int _price;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Ŭ���� ��ü�� �̹������� Ȯ�����ֱ�
        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out ShopItemUI shopItemUI))
        {
            _clickPanel.SetActive(true);
            _clickPanel.SetPanelData(shopItemUI.ShopItemSO);
            // Ŭ���� �̹����� ����������
            Image clickedImage = eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>();

            //_selectedItemOn.sprite = clickedImage.sprite;
        }
    }

    public void Back()
    {
        _clickPanel.SetActive(false);
    }

    public void OffShop()
    {
        UIManager.Instance.Close(UIType.Shop);
    }
}
