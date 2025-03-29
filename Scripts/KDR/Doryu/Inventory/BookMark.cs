using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public struct MoveRectTrmData
{
    public RectTransform moveObj;
    public Vector2 movePos;
    public float speed;
    public AnimationCurve animation;
}

public class BookMark : MonoBehaviour
{
    public BookMark another;

    private Image image;
    [SerializeField] private Sprite enableSprite;
    [SerializeField] private Sprite disableSprite;
    [SerializeField] private GameObject[] _setActiveObj;
    [SerializeField] private MoveRectTrmData[] _moveRectTrmDatas;
    [SerializeField] private bool _isActiveStart;
    [SerializeField] private bool _useOnDisableReset;

    private Button _button;
    private Sequence _seq;
    private bool _isActive;

    private void Awake()
    {
        image = GetComponent<Image>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(Enable);
    }

    public void Enable()
    {
        _isActive = true;
        another.Disable();
        image.sprite = enableSprite;

        for (int i = 0; i < _setActiveObj.Length; i++)
        {
            _setActiveObj[i].SetActive(_isActive);
        }
        if (_seq != null && _seq.IsActive()) _seq.Kill();
        _seq = DOTween.Sequence();
        for (int i = 0; i < _moveRectTrmDatas.Length; i++)
        {
            if (i == 0)
                _seq.Append(_moveRectTrmDatas[i].moveObj
                    .DOAnchorPos(_moveRectTrmDatas[i].movePos, _moveRectTrmDatas[i].speed)
                    .SetEase(_moveRectTrmDatas[i].animation));
            else
                _seq.Join(_moveRectTrmDatas[i].moveObj
                    .DOAnchorPos(_moveRectTrmDatas[i].movePos, _moveRectTrmDatas[i].speed)
                    .SetEase(_moveRectTrmDatas[i].animation));
        }
    }

    public void Disable()
    {
        _isActive = false;
        image.sprite = disableSprite;
        for (int i = 0; i < _setActiveObj.Length; i++)
        {
            _setActiveObj[i].SetActive(_isActive);
        }
    }

    private void OnDisable()
    {
        if (_isActiveStart && _useOnDisableReset)
            Enable();
    }
}
