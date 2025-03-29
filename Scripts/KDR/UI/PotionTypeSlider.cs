using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionTypeSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _sliderTrm;
    [SerializeField] private Image[] _images;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _selectMagnetPower = 5;

    private bool isClicked = false;
    public bool isThrowMode => _sliderTrm.anchoredPosition.x > -50;


    private void Update()
    {
        if (isClicked == false && _sliderTrm.anchoredPosition.x != 0 && _sliderTrm.anchoredPosition.x != 100)
        {
            _sliderTrm.anchoredPosition =
                Vector2.Lerp(_sliderTrm.anchoredPosition,
                new Vector2(isThrowMode ? 0 : -100, _sliderTrm.anchoredPosition.y),
                Time.deltaTime * _selectMagnetPower);
        }

        if (isThrowMode)
        {
            _images[0].sprite = _sprites[0];
            _images[1].sprite = _sprites[3];
        }
        else
        {
            _images[0].sprite = _sprites[1];
            _images[1].sprite = _sprites[2];
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isClicked = false;
    }

    public PotionType GetPotionType()
    {
        return isThrowMode ? PotionType.Throw : PotionType.Drink;
    }
}
