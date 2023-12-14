using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using CharactherStats;

public class ScStatsDysplai : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public ScCharactereStats Stats {
        get { return stats; }
        set
        {
            stats = value;
            UpdateStatValue();
        }
    }



    private ScCharactereStats stats;


    private string name;
    public string Name {  
        get { return name; }
        set
        {
            name = value;
            nameText.text = name;
        }
    }

        
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] ScStatsToolTips toolTips;

    private void OnValidate()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        nameText = texts[0];
        valueText = texts[1];

        if(toolTips == null)
            toolTips = FindObjectOfType<ScStatsToolTips>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTips.ShowToolTip(Stats, Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTips.HideToolTips();
    }

    public void UpdateStatValue()
    {
        valueText.text = stats.Value.ToString();
    }

}
