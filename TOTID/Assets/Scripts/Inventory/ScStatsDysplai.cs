using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScStatsDysplai : MonoBehaviour
{
        
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI valueText;

    private void OnValidate()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        nameText = texts[0];
        valueText = texts[1];
    }

}
