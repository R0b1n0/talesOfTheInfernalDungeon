using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SCVolumeManager : MonoBehaviour {
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    public void SetMusicVolume() {
        float musVolume = musicSlider.value;
        audioMixer.SetFloat("music",musVolume);
    }

    public void SetSFXVolume() {
        float SFXVolume = SFXSlider.value;
        audioMixer.SetFloat("music",SFXVolume);
    }
}
