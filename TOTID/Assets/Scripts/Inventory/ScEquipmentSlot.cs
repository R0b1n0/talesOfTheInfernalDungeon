

public class ScEquipmentSlot : ScItemSlot
{
    public EquipmentType EquipmentType;
    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipmentType.ToString() + " Slot";
    }
}
