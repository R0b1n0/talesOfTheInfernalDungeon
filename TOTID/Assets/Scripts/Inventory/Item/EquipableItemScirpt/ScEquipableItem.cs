using UnityEngine;
using static UnityEditor.Progress;


public enum EquipmentType
{
    weapon,
    item,
}

[CreateAssetMenu(menuName = ("Item/Item/Equipable Item"))]
public class ScEquipableItem : ScItem
{
    public int strengthBonus;
    public int healthBonus;
    [Space]
    public float strengthPercent;
    public float healthPercent;
    [Space]
    public EquipmentType equipmentType;

    public void Equip(ScCharacterData c)
    {
        if(strengthBonus != 0)
        {
            c.strength.AddModifier(new ScStatsModifier(strengthBonus, StatModType.Flat, this));
        }        
        if(strengthPercent != 0)
        {
            c.strength.AddModifier(new ScStatsModifier(strengthPercent, StatModType.PercentMult, this));
        }        
        
        if(healthBonus != 0)
        {
            c.health.AddModifier(new ScStatsModifier(healthBonus, StatModType.Flat, this));
        }        
        if(healthPercent != 0)
        {
            c.health.AddModifier(new ScStatsModifier(healthPercent, StatModType.PercentMult, this));
        }
    }

    public void Unequip(ScCharacterData c)
    {
        c.strength.RemoveAllModifiersFromSource(this);
        c.health.RemoveAllModifiersFromSource(this);
    }



    public void AddStat(float value, string statsName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            if (value > 0)
            {
                sb.Append("+");
            }

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statsName);
        }
    }

    public override string GetItemType()
    {
        return equipmentType.ToString();
    }

    public override string GetDescription()
    {

        sb.Length = 0;
        AddStat(strengthBonus, "Strength");
        AddStat(healthBonus, "Health");
        AddStat(strengthPercent, "Strength", isPercent: true);
        AddStat(healthPercent, "Health", isPercent: true);
        return sb.ToString();
    }
}
