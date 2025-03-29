using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DiePanel : MonoBehaviour, IManageableUI
{
    private RectTransform _rect;

    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _kill;
    [SerializeField] private TextMeshProUGUI _gather;

    [SerializeField] private RectTransform _progressStart, _progressEnd;
    [SerializeField] private RectTransform _player;
    [SerializeField] private Transform _gatheredIngredientsContainer;

    private bool _isActive = false;

    private float _progress;
    private int _time;
    private int _killCnt;
    private int _gatherCnt;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_isActive)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }

    public void Init(int timer, int killCnt, int gatherCnt, float progress)
    {
        _time = timer;
        _killCnt = killCnt;
        _gatherCnt = gatherCnt;
        _progress = progress;
    }

    public void Close()
    {

    }

    public void Open()
    {
        _rect.DOAnchorPosY(0f, 0.5f)
            .OnComplete(() =>
            {
                StartCoroutine(ProgressRoutine());
                _isActive = true;
            });
    }

    private IEnumerator ProgressRoutine()
    {
        float curProgress = 0;
        _player.anchoredPosition = new Vector2(_progressStart.anchoredPosition.x, _player.anchoredPosition.y);
        yield return new WaitForSeconds(0.5f);

        while (curProgress < _progress)
        {
            float posX = Mathf.Lerp(_progressStart.anchoredPosition.x, _progressEnd.anchoredPosition.x, curProgress);

            Vector2 playerPosition = new Vector2(posX, _player.anchoredPosition.y);
            _player.anchoredPosition = playerPosition;

            curProgress += 0.005f;

            yield return null;
        }

        int time = 0;
        curProgress = 0;
        while (curProgress <= 1)
        {
            time = (int)Mathf.Lerp(0, _time, curProgress);
            int minute = time / 60;
            int second = time % 60;

            _timer.SetText($"{minute} : {second}");
            curProgress += 0.005f;
            yield return null;
        }
        _kill.SetText($"{_time}");

        int kill = 0;
        curProgress = 0;
        while (curProgress <= 1)
        {
            kill = (int)Mathf.Lerp(0, _killCnt, curProgress);
            _kill.SetText($"{kill}");
            curProgress += 0.005f;
            yield return null;
        }
        _kill.SetText($"{_killCnt}");

        int gather = 0;
        curProgress = 0;
        while (curProgress <= 1)
        {
            gather = (int)Mathf.Lerp(0, gather, curProgress);
            _gather.SetText($"{gather}");
            curProgress += 0.005f;
            yield return null;
        }
        _kill.SetText($"{_gatherCnt}");
    }

    public void Init()
    {

    }
}
