using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScVisualSettings : MonoBehaviour {
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Awake() {
        Screen.fullScreen=true;
    }

    void Start() {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        List<string> uniqueResolutions = new List<string>();

        int currentResolutionsIndex = 0;

        for (int u = 0; u < resolutions.Length; u++) {
            string resolutionString = resolutions[u].width + " x " + resolutions[u].height;

            if (!uniqueResolutions.Contains(resolutionString)) {
                options.Add(resolutionString);
                uniqueResolutions.Add(resolutionString);

                if (resolutions[u].width == Screen.currentResolution.width && resolutions[u].height == Screen.currentResolution.height) {
                    currentResolutionsIndex = options.Count - 1;
                }
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionsIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex) {
        string resolutionString = resolutionDropdown.options[resolutionIndex].text;
        string[] resolutionParts = resolutionString.Split('x');
        int width = int.Parse(resolutionParts[0]);
        int height = int.Parse(resolutionParts[1]);

        Resolution resolution = new Resolution {
            width = width,
            height = height
        };

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen (bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
}
