using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Option : MonoBehaviour, IManageableUI
{
    [Space(10)]
    [SerializeField] private RectTransform _optionRect;
    [SerializeField] private float _easeingDelay = 0.5f;
    [SerializeField] private Vector2 _openOffset;
    [SerializeField] private Vector2 _closeOffset;
    private Tween tween;

    public bool isOpened = false;

    private OptionPanel _optionPanel;

    public void Open()
    {
        if (tween != null && tween.active)
            tween.Kill();

        _optionPanel.SettingActive(true);

        tween = _optionRect.DOAnchorPos(_openOffset, _easeingDelay)
            .OnComplete(() => Time.timeScale = 0f);
        isOpened = true;
    }
    public void Close()
    {
        Time.timeScale = 1f;
        if (tween != null && tween.active)
            tween.Kill();

        _optionPanel.SettingActive(false);

        tween = _optionRect.DOAnchorPos(_closeOffset, _easeingDelay);
        isOpened = false;
    }

    public void Init()
    {
        _optionPanel = _optionRect.GetComponent<OptionPanel>();
        _optionRect.anchoredPosition = _closeOffset;
    }
}
