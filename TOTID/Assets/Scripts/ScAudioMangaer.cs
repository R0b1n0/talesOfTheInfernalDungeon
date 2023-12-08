using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAudioMangaer : MonoBehaviour {
    [Header("~~~~~~~~ Audio Source ~~~~~~~~")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("~~~~~~~~ Audio Clip ~~~~~~~~")]
    public AudioClip background;
    public AudioClip test;

    private void Start() {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }
}
