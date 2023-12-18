using UnityEngine;

public enum EquipmentType
{
    weapon,
    item,
}

[CreateAssetMenu]
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
}
