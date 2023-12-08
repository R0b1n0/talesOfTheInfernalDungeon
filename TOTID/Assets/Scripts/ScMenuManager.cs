using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMenuManager : MonoBehaviour {
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
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

    public void Creditin() {
        animator.SetBool("Creditin", true);
    }

    public void BackMenu() {
        animator.SetBool("Creditin", false);
    }
}
