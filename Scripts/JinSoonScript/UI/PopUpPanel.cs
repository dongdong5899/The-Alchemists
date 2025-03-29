using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpPanel : MonoBehaviour, IManageableUI
{
    [SerializeField] private TextMeshProUGUI _popUpText;
    [SerializeField] private float _easingTime = 0.3f;
    private CanvasGroup _canvasGroup;
    private float _originY = -370;
    private float _downY = -430;
    private RectTransform _rect;

    private Sequence _seq;

    public void Close()
    {
        if (_seq != null && _seq.active)
            _seq.Complete();

        _seq = DOTween.Sequence();

        _seq.Append(_canvasGroup.DOFade(0, _easingTime))
            .Join(_rect.DOAnchorPosY(_downY, _easingTime));
    }

    public void Open()
    {
        if (_seq != null && _seq.active)
            _seq.Complete();

        _seq = DOTween.Sequence();

        _seq.Append(_canvasGroup.DOFade(1, _easingTime))
            .Join(_rect.DOAnchorPosY(_originY, _easingTime));
    }

    public void SetText(string txt)
    {
        _popUpText.SetText(txt);
    }

    public void Init()
    {
        _rect = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _originY = _rect.anchoredPosition.y;

        _canvasGroup.alpha = 0;
        _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x,_downY);
    }
}
