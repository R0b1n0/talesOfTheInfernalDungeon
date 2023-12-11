using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class ScVisualSettings : MonoBehaviour {

    public TMP_Dropdown resolutionDropdown;
    
    Resolution[] resolutions;

    void Start (){
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string> ();

        for (int u = 0; u < resolutions.Length; u++) {
            string option = resolutions[u].width + " x " +resolutions[u].height;
            options.Add (option);
        }

        resolutionDropdown.AddOptions (options);
    }

    public void SetFullscreen (bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
}
