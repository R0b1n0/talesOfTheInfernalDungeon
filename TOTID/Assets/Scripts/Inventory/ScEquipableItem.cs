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
}
