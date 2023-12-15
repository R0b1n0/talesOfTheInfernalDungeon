using CharactherStats;
using UnityEngine;

public class ScStatsPanel : MonoBehaviour
{
    [SerializeField] ScStatsDysplai[] statsDisplays;
    [SerializeField] ScHealthDisplay[] statsDisplayHealth;
    [SerializeField] string[] statsName;

    private ScCharactereStats[] stats;

    private void OnValidate()
    {
        statsDisplays = GetComponentsInChildren<ScStatsDysplai>();
        statsDisplayHealth = GetComponentsInChildren<ScHealthDisplay>();
        UpdateStatsName();
    }

    public void SetStats(params ScCharactereStats[] charStats)
    {
        stats = charStats;
        
        if(stats.Length > statsDisplays.Length + statsDisplayHealth.Length)
        {
            Debug.LogError("Not Enought Stat Displays");
            return;
        }

        for (int i = 0; i < statsDisplays.Length + statsDisplayHealth.Length; i++)
        {
            statsDisplays[i].gameObject.SetActive(i < stats.Length);

            if(i < stats.Length)
            {
                statsDisplays[i].Stats = stats[i];
            }
        }
    }

    public void UpdateStatsValue()
    {
        for (int i = 0;i < stats.Length; i++)
        {
            statsDisplays[i].UpdateStatValue();
        }
    }    
    public void UpdateStatsName()
    {
        for (int i = 0;i < statsName.Length; i++)
        {
            statsDisplays[i].Name = statsName[i];
        }
    }
}
