using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuideF : MonoBehaviour
{
    Sequence sequence;

    private void Awake()
    {
        sequence = DOTween.Sequence();
    }

    private void Update()
    {
        sequence.Append(transform.DOScale(4, 0.5f).SetEase(Ease.Linear)).
            Append(transform.DOScale(2, 0.5f).SetEase(Ease.Linear));
    }
}
