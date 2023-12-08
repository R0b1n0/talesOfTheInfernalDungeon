using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMenuManager : MonoBehaviour {
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void Creditin() {
        animator.SetBool("Creditin", true);
    }

    public void LoadSettings() {
        animator.SetBool("Settings", true);
    }
    public void BackMenu() {
        animator.SetBool("Creditin", false);
        animator.SetBool("Settings", false);
    }
    public void SelectionMenu() {
        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void MainMenu(){
        SceneManager.LoadSceneAsync(0);
    }

    public void ReplayLevel(){
        
    }
}
