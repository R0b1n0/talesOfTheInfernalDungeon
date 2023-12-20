using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ScMenuManager : MonoBehaviour {
    private Animator animator;
    private enum Lorin { Null, Text1, Text2, Text3, Text4, Text5}
    [SerializeField] private Lorin lore = Lorin.Null;

    [SerializeField] private InputActionReference next;
    ScAudioManager audioManager;

    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<ScAudioManager>();
    }

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void Creditin() {
        audioManager.PlayRandomSFX(audioManager.click);
        animator.SetBool("Creditin", true);
    }

    public void LoadSettings() {
        audioManager.PlayRandomSFX(audioManager.click);
        animator.SetBool("Settings", true);
    }

    public void SelectionMenu(){
        audioManager.PlayRandomSFX(audioManager.click);
        animator.SetBool("Selecting", true);
    }

    public void BackMenu() {
        if (animator.GetBool("Creditin") != true) {
            audioManager.PlayRandomSFX(audioManager.click);
        }
        animator.SetBool("Creditin", false);
        animator.SetBool("Settings", false);
        animator.SetBool("Selecting", false);
    }
    
    public void QuitGame() {
        audioManager.PlayRandomSFX(audioManager.click);
        Application.Quit();
    }

    public void MainMenu(){
        SceneManager.LoadSceneAsync(0);
    }

    public void LevelTutorial() {
        SceneManager.LoadSceneAsync(1);
    }

    public void ReplayLevel(){
        
    }
    public void Play() {
        SceneManager.LoadScene("RobScene");
    }

    public void Lore() {
        audioManager.PlayRandomSFX(audioManager.click);
        animator.SetBool("Loring", true);
        animator.SetInteger("Lored", 1);
        lore = Lorin.Text1;
    }

    public void SwitchingLore(InputAction.CallbackContext ctx) {
        if (ctx.started){
            switch (lore){
                case Lorin.Text1:
                    animator.SetInteger("Lored", 2);
                    lore = Lorin.Text2;
                    break;
                case Lorin.Text2:
                    animator.SetInteger("Lored", 3);
                    lore = Lorin.Text3;
                    break;
                case Lorin.Text3:
                    animator.SetInteger("Lored", 4);
                    lore = Lorin.Text4;
                    break;
                case Lorin.Text4:
                    animator.SetInteger("Lored", 5);
                    lore = Lorin.Text5;
                    break;
                case Lorin.Text5:
                    Play();
                    break;
            }
        }
    }
}
