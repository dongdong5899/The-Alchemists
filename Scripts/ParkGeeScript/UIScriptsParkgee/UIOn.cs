using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIOn : MonoBehaviour
{
    [SerializeField] private GameObject[] ui;

    private RectTransform rectTransform;

    private Sequence seq;

    [SerializeField] private bool isESC = false;
    [SerializeField] private bool isInven = false;
    //[SerializeField] private bool isOnOff = false;

    Vector3 escPanelPos, invenScale;

    private void Awake()
    {
        ui[0].SetActive(false);
        ui[1].SetActive(false);
        escPanelPos = ui[0].transform.position;
        invenScale = ui[1].transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isESC = !isESC;
            if (isESC)
            {
                ShowSetting();
            }
            else
            {
                ui[0].transform.position = escPanelPos;
                ui[0].SetActive(false);
                ui[2].SetActive(false);
            }

            if (isInven)
            {
                ui[0].SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInven = !isInven;
            if(isInven)
            {
                ShowInven();
            }
            else
            {
                ui[1].transform.localScale = invenScale;
                ui[1].SetActive(false);
            }

            if (isESC)
            {
                ui[1].SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UIManager.Instance.Open(UIType.Shop);
        }
    }

    public void ShowSetting()
    {
        isESC = true;
        ui[0].SetActive(true);
        rectTransform = ui[0].GetComponent<RectTransform>();
        if (seq != null && seq.IsActive()) seq.Kill();
        seq = DOTween.Sequence();
        rectTransform.position = escPanelPos;
        seq.Append(rectTransform.DOMove(new Vector2(2000, 450), 0.5f).SetEase(Ease.Linear));
    }

    public void ShowInven()
    {
        isInven = true;
        ui[1].SetActive(true);
        rectTransform = ui[1].GetComponent<RectTransform>();
        if (seq != null && seq.IsActive()) seq.Kill();
        seq = DOTween.Sequence();
        rectTransform.sizeDelta = new Vector2(100, 50);
        seq.Append(rectTransform.DOSizeDelta(new Vector2(1800, 950), 0.5f).SetEase(Ease.Linear));
    }

    public void SetPanelOn()
    {
        ui[2].SetActive(true);
        rectTransform = ui[2].GetComponent<RectTransform>();
        if (seq != null && seq.IsActive()) seq.Kill();
        seq = DOTween.Sequence();
        rectTransform.position = escPanelPos;
        seq.Append(rectTransform.DOMove(new Vector2(2000, 450), 0.5f).SetEase(Ease.Linear));
    }

    public void Continue()
    {
        for (int i = 0; i < ui.Length; i++)
        {
            ui[i].SetActive(false);
        }
    }
}
