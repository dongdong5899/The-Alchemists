using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBarUI : MonoBehaviour, IManageableUI
{
    private RectTransform _rect;
    [SerializeField]
    private Health _bossHealth;
    [SerializeField] private Slider _hpBar;

    [SerializeField] private float _openOffset = 490f;
    [SerializeField] private float _closeOffset = 600f;
    [SerializeField] private float _tweenDelay = 0.5f;

    [SerializeField] private float _hpDownDelay = 0.2f;

    private Sequence _seq;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _bossHealth.OnHit += SetEnemyHealth;
    }

    public void Close()
    {
        _rect.DOAnchorPosY(_closeOffset, _tweenDelay);
    }

    public void Init()
    {
        _hpBar.value = 1;
    }

    public void Open()
    {
        _rect.DOAnchorPosY(_openOffset, _tweenDelay);
    }

    public void SetEnemyHealth()
    {
        float hpRatio = _bossHealth.curHp / _bossHealth.maxHp.GetValue();

        if (_seq != null && _seq.active)
            _seq.Complete();

        _seq = DOTween.Sequence();

        _seq.Append(DOTween.To(() => _hpBar.value, x => _hpBar.value = x, hpRatio, _hpDownDelay));
    }
}
