using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossStageEnterPanal : MonoBehaviour, IManageableUI
{
    [SerializeField] private RectTransform _bgLeft, _bgRight;
    [SerializeField] private TextMeshProUGUI _bossNameTxt;
    [SerializeField] private AnimationCurve _easing;
    [SerializeField] private float _tweenTime = 0.7f;
    private Sequence _seq;


    public void SetBossName(string name)
    {
        _bossNameTxt.SetText(name);
    }



    public void Close()
    {
        
        //쓸일 없음 Open하면 나왔다 사라지는 거 까지 다 됨
    }

    public void Open()
    {
        if (_seq != null && _seq.active)
            _seq.Kill();

        _seq = DOTween.Sequence();

        CameraManager.Instance.ZoomIn(5);
        _seq.AppendInterval(0.5f)
            .Append(_bgLeft.DOAnchorPosX(0, _tweenTime).SetEase(_easing))
            .Join(_bgRight.DOAnchorPosX(0, _tweenTime).SetEase(_easing))
            .Join(_bossNameTxt.rectTransform.DOAnchorPosX(600, _tweenTime).SetEase(_easing))
            .Append(_bgLeft.DOAnchorPosX(1920, _tweenTime / 4).SetEase(Ease.InSine))
            .Join(_bgRight.DOAnchorPosX(-1920, _tweenTime / 4).SetEase(Ease.InSine))
            .Join(_bossNameTxt.rectTransform.DOAnchorPosX(-1920, _tweenTime / 4).SetEase(Ease.InSine))
            .AppendCallback(() =>
            {
                _bgLeft.anchoredPosition = new Vector2(-1920, _bgLeft.anchoredPosition.y);
                _bgRight.anchoredPosition = new Vector2(1920, _bgRight.anchoredPosition.y);
                _bossNameTxt.rectTransform.anchoredPosition = new Vector2(1920, _bossNameTxt.rectTransform.anchoredPosition.y);
            });
    }

    public void Init()
    {
        _bgLeft.anchoredPosition = new Vector2(-1920, _bgLeft.anchoredPosition.y);
        _bgRight.anchoredPosition = new Vector2(1920, _bgRight.anchoredPosition.y);
        _bossNameTxt.rectTransform.anchoredPosition = new Vector2(1920, _bossNameTxt.rectTransform.anchoredPosition.y);
    }
}
