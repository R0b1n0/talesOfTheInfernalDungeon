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

    public void SelectionMenu(){
        animator.SetBool("Selecting", true);
    }

    public void BackMenu() {
        animator.SetBool("Creditin", false);
        animator.SetBool("Settings", false);
        animator.SetBool("Selecting", false);
    }
    

    public void QuitGame() {
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
    public void Play()
    {
        SceneManager.LoadScene("RobScene");
    }
}
