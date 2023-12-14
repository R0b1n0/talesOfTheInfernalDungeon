using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScItemToolTips : MonoBehaviour
{
    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemSlotText;
    [SerializeField] Text ItemStatsText;

    private StringBuilder sb = new StringBuilder();

    public void ShowToolTip(ScEquipableItem item)
    {
        ItemNameText.text = item.name;
        ItemSlotText.text = item.equipmentType.ToString();

        sb.Length = 0;
        AddStat(item.strengthBonus, "Strength");
        AddStat(item.healthBonus, "Health");
        AddStat(item.strengthPercent, "Strength", isPercent: true);
        AddStat(item.healthPercent, "Health", isPercent: true);

        ItemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideToolTips()
    {
        gameObject.SetActive(false);
    }

    public void AddStat(float value, string statsName, bool isPercent = false)
    {
        if(value !=  0)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }

            if(value > 0)
            {
                sb.Append("+");
            }

            if(isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }else
            {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statsName);
        }
    }
}
