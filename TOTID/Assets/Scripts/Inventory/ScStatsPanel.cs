using CharactherStats;
using UnityEngine;

public class ScStatsPanel : MonoBehaviour
{
    [SerializeField] ScStatsDysplai[] statsDisplays;
    [SerializeField] string[] statsName;

    private ScCharactereStats[] stats;

    private void OnValidate()
    {
        statsDisplays = GetComponentsInChildren<ScStatsDysplai>();
        UpdateStatsName();
    }

    public void SetStats(params ScCharactereStats[] charStats)
    {
        stats = charStats;
        
        if(stats.Length > statsDisplays.Length)
        {
            Debug.LogError("Not Enought Stat Displays");
            return;
        }

        for (int i = 0; i < statsDisplays.Length; i++)
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
