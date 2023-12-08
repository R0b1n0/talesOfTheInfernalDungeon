using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMenuManager : MonoBehaviour {
    public void SelectionMenu() {
        
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void EndCredit(){
        SceneManager.LoadSceneAsync(1);
    }

    public void MainMenu(){
        SceneManager.LoadSceneAsync(0);
    }

    public void ReplayLevel(){
        
    }

}
