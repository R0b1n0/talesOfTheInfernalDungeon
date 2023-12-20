using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAudioManager : MonoBehaviour {
    [Header("~~~~~~~~ Audio Source ~~~~~~~~")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("~~~~~~~~ Audio Clip ~~~~~~~~")]
    public AudioClip background;
    public AudioClip mob;
    public AudioClip[] footStep;
    public AudioClip[] sword;
    public AudioClip[] click;


    private void Start() {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }

    public void PlayRandomSFX(AudioClip[] clip) {
        int u = Random.Range(0, clip.Length);
        SFXSource.PlayOneShot(clip[u]);
    }
}
