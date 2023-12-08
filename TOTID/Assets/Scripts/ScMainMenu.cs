using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMainMenu : MonoBehaviour {
    public void SelectionMenu() {
        
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void EndCredit(){
        SceneManager.LoadSceneAsync(1);
    }

}
