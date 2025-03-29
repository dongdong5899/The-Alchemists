using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Doryu
{
    [Serializable]
    public struct MoveTweenData
    {
        public Vector2 pos;
        public AnimationCurve curve;
        public float time;
    }

    public class InventoryPanel : MonoBehaviour, IManageableUI
    {
        [SerializeField] private MoveTweenData _closeData;
        [SerializeField] private MoveTweenData _openData;
        [SerializeField] private BookMark[] _bookMarks;

        private RectTransform _rectTrm;
        private Sequence _seq;

        public void Close()
        {
            if (_seq != null && _seq.IsActive()) _seq.Kill();
            _seq = DOTween.Sequence();

            _seq.Append(
                _rectTrm.DOAnchorPos(_closeData.pos, _closeData.time)
                .SetEase(_closeData.curve));
        }

        public void Init()
        {
            _rectTrm = transform as RectTransform;
            _rectTrm.anchoredPosition = _closeData.pos;
        }

        public void Open()
        {
            if (_seq != null && _seq.IsActive()) _seq.Kill();
            _seq = DOTween.Sequence();

            _seq.Append(
                _rectTrm.DOAnchorPos(_openData.pos, _openData.time)
                .SetEase(_openData.curve));
            _bookMarks[1].Enable();
            OnPotUI(false);
        }

        public void OnPotUI(bool on)
        {
            for (int i = 0; i < _bookMarks.Length; i++)
            {
                _bookMarks[i].gameObject.SetActive(i < 2 ? !on : on);
            }
            if (on)
                _bookMarks[3].Enable();
            else
                _bookMarks[1].Enable();
        }
    }
}
