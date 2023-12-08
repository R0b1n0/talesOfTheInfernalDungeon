using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMenuManager : MonoBehaviour {
    public bool credit = false;

    public void SelectionMenu() {
        
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void EndCredit(){
        bool credit = true;
    }

    public void MainMenu(){
        SceneManager.LoadSceneAsync(0);
    }

    public void ReplayLevel(){
        
    }

}
