using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBottle : MonoBehaviour
{
    private Image fill;
    private RectTransform imageRect;
    private Sequence seq;

    public bool IsBottleFull { get; private set; }
    public bool IsBottleEmpty { get; private set; }

    private void Awake()
    {
        imageRect = transform.Find("HealthBottle").GetComponent<RectTransform>();
        fill = transform.Find("HealthBottle/Heart").GetComponent<Image>();

        IsBottleFull = true;
        IsBottleEmpty = false;
    }

    public void HpDown()
    {
        if (IsBottleEmpty == true) return;

        if (IsBottleFull)
        {
            if(seq != null && seq.active)
                seq.Complete();

            seq = DOTween.Sequence();

            seq.Append(DOTween.To(() => 1, x => fill.fillAmount = x, 0.5f, 0.1f))
                .Join(imageRect.DOAnchorPosY(15f, 0.05f))
                .Insert(0.05f, imageRect.DOAnchorPosY(0f, 0.05f));
            IsBottleFull = false;
        }
        else
        {
            if (seq != null && seq.active)
                seq.Kill();

            seq = DOTween.Sequence();

            seq.Append(DOTween.To(() => 0.5f, x => fill.fillAmount = x, 0f, 0.1f))
                .Join(imageRect.DOAnchorPosY(15f, 0.05f))
                .Insert(0.05f, imageRect.DOAnchorPosY(0f, 0.05f));
            IsBottleEmpty = true;
        }
    }
    public void HpUp()
    {
        if (IsBottleFull == true) return;

        if (IsBottleEmpty)
        {
            if(seq != null && seq.active)
                seq.Complete();

            seq = DOTween.Sequence();

            seq.Append(DOTween.To(() => 0, x => fill.fillAmount = x, 0.5f, 0.1f))
                .Join(imageRect.DOAnchorPosY(15f, 0.05f))
                .Insert(0.05f, imageRect.DOAnchorPosY(0f, 0.05f));
            IsBottleEmpty = false;
        }
        else
        {
            if (seq != null && seq.active)
                seq.Kill();

            seq = DOTween.Sequence();

            seq.Append(DOTween.To(() => 0.5f, x => fill.fillAmount = x, 1f, 0.1f))
                .Join(imageRect.DOAnchorPosY(15f, 0.05f))
                .Insert(0.05f, imageRect.DOAnchorPosY(0f, 0.05f));
            IsBottleFull = true;
        }
    }

    public void SetAsHalfHp()
    {
        fill.fillAmount = 0.5f;
    }
}
