using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private Slider allSlider, bgmSlider, sfxSlider;
    [SerializeField] private Button _titleBtn, _playBtn, _exitBtn;

    private void Start()
    {
        allSlider.value = AudioManager.Instance.volumeSaveData.allVolume;
        bgmSlider.value = AudioManager.Instance.volumeSaveData.bgmVolume;
        sfxSlider.value = AudioManager.Instance.volumeSaveData.sfxVolume;

        allSlider.onValueChanged.AddListener(HandleValueChanged);
        bgmSlider.onValueChanged.AddListener(HandleValueChanged);
        sfxSlider.onValueChanged.AddListener(HandleValueChanged);

        _titleBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        });
        _playBtn.onClick.AddListener(() => UIManager.Instance.Close(UIType.Option));
        _exitBtn.onClick.AddListener(() => Application.Quit());
    }

    private void HandleValueChanged(float arg0)
    {
        AudioManager.Instance.SetVolume(allSlider.value, bgmSlider.value, sfxSlider.value);
    }

    public void SettingActive(bool value)
    {
        allSlider.enabled = value;
        bgmSlider.enabled = value;
        sfxSlider.enabled = value;
    }
}