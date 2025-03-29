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
        // 클릭한 객체가 이미지인지 확인해주기
        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out ShopItemUI shopItemUI))
        {
            _clickPanel.SetActive(true);
            _clickPanel.SetPanelData(shopItemUI.ShopItemSO);
            // 클릭한 이미지를 변수로저장
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
