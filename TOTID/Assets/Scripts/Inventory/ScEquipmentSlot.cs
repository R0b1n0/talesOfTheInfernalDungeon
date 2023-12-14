

public class ScEquipmentSlot : ScItemSlot
{
    public EquipmentType EquipmentType;
    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipmentType.ToString() + " Slot";
    }

    public override bool CanReceiveItem(ScItem item)
    {
        if(item == null)
            return true;

        ScEquipableItem equipableItem = item as ScEquipableItem;
        return equipableItem != null && equipableItem.equipmentType == EquipmentType;

    }
}
