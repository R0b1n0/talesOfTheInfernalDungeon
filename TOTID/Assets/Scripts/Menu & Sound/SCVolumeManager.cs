using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SCVolumeManager : MonoBehaviour {
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start() {
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume() {
        float musicVolume = musicSlider.value;
        audioMixer.SetFloat("music",Mathf.Log10(musicVolume)*20);
    }

    public void SetSFXVolume() {
        float SFXVolume = SFXSlider.value;
        audioMixer.SetFloat("SFX",Mathf.Log10(SFXVolume)*20);
    }
}
