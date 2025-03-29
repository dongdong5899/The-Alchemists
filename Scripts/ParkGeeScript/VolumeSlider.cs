using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider audioSlider;

    public void AudioControl()
    {
        float sound = audioSlider.value;
        
        if (sound == -40f) 
            masterMixer.SetFloat("BGM", -80);
        else 
            masterMixer.SetFloat("BGM", sound);
    }

    /*public void ToggleAudioVolume() // 토글방식으로 On/Off
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }*/
}
