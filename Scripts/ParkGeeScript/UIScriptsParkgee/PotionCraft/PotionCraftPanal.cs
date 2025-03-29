using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PotionCraftPanal : MonoBehaviour, IManageableUI
{
    [SerializeField] private float _onYPos;
    [SerializeField] private float _offYPos;
    [SerializeField] private RectTransform _rectTrm;

    private bool _isPlay;

    public void Open()
    {
        if (_isPlay) return;
        //Time.timeScale = 0;
        _isPlay = true;

        _rectTrm.DOAnchorPosY(_onYPos, 0.3f)
            .SetEase(Ease.InOutFlash)
            .SetUpdate(true)
            .OnComplete(() => _isPlay = false);
    }

    public void Close()
    {
        if (_isPlay) return;
        //Time.timeScale = 1;
        _isPlay = true;

        _rectTrm.DOAnchorPosY(_offYPos, 0.3f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true)
            .OnComplete(() => _isPlay = false);
    }

    public void Init()
    {

    }
}
