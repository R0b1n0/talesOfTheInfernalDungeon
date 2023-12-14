using System.Text;
using UnityEngine.UI;
using UnityEngine;
using CharactherStats;

public class ScStatsToolTips : MonoBehaviour
{
    [SerializeField] Text StatsNameText;
    [SerializeField] Text StatsModifierLabelText;
    [SerializeField] Text StatsModifiersText;

    private StringBuilder sb = new StringBuilder();

    public void ShowToolTip(ScCharactereStats stats, string statsName)
    {
        StatsNameText.text = GetStatsTopText(stats, statsName);

        StatsModifiersText.text = GetStatModifierText(stats);

        gameObject.SetActive(true);
    }

    public void HideToolTips()
    {
        gameObject.SetActive(false);
    }

    private string GetStatsTopText(ScCharactereStats stats, string statsName)
    {
        sb.Length = 0;
        sb.Append(statsName);
        sb.Append(" ");
        sb.Append(stats.Value);

        if (stats.Value != stats.baseValue)
        {
            sb.Append(" (");
            sb.Append(stats.baseValue);

            if (stats.Value > stats.baseValue)
                sb.Append("+");
            sb.Append(System.Math.Round(stats.Value - stats.baseValue, 4));
            sb.Append(")");
        }

        return sb.ToString();
    }

    private string GetStatModifierText(ScCharactereStats stat)
    {
        sb.Length = 0;

        foreach (ScStatsModifier mod in stat.StatsModifiers)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (mod.value > 0)
                sb.Append("+");
            if(mod.type == StatModType.Flat)
            {
                sb.Append(mod.value);
            }
            else
            {
                sb.Append(mod.value * 100);
                sb.Append("%");
            }

            ScEquipableItem item = mod.source as ScEquipableItem;

            if (item != null)
            {
                sb.Append(" ");
                sb.Append(item.nameItem);
            }
            else
            {
                Debug.LogError("Modifier is not an EquipableItem");
            }
        }
        return sb.ToString();
    }

}
