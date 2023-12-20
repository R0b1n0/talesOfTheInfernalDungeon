using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using CharactherStats;

public class ScHealthDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public ScCharactereStats StatsHealth {
        get { return statsHealth; }
        set
        {
            statsHealth = value;
            UpdateStatValueHealth();
        }
    }



    private ScCharactereStats statsHealth;


    private string nameHealth;
    public string NameHealth {  
        get { return nameHealth; }
        set
        {
            nameHealth = value;
            nameTextHealth.text = nameHealth;
        }
    }

        
    [SerializeField] TextMeshProUGUI nameTextHealth;
    [SerializeField] TextMeshProUGUI valueTextHealth;

    [SerializeField] ScStatsToolTips toolTipsHealth;
    [SerializeField] Slider healthSlider;

    private void OnValidate()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        nameTextHealth = texts[0];
        valueTextHealth = texts[1];


        if(toolTipsHealth == null)
            toolTipsHealth = FindObjectOfType<ScStatsToolTips>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipsHealth.ShowToolTip(StatsHealth, NameHealth);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipsHealth.HideToolTips();
    }

    public void UpdateStatValueHealth()
    {
        valueTextHealth.text = statsHealth.Value.ToString();
        valueTextHealth.text = healthSlider.value.ToString();
    }

}
