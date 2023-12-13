using UnityEngine;

public class ScInventoryManager : MonoBehaviour
{
    
    [SerializeField] ScInventory inventory;
    [SerializeField] ScEquipmentPanel equipmentPanel;


    private void Awake()
    {
        inventory.OnItemRightClickedEvent += EquipFromInventory;
        equipmentPanel.OnItemRightClickedEvent -= UnequipFromEquiPanel;
    }

    private void EquipFromInventory(ScItem item)
    {
        if(item is ScEquipableItem)
        {
            Equip((ScEquipableItem)item);
        }
    }    
    private void UnequipFromEquiPanel(ScItem item)
    {
        if(item is ScEquipableItem)
        {
            Unequip((ScEquipableItem)item);
        }
    }

    public void Equip(ScEquipableItem item)
    {
        if(inventory.RemoveItem(item))
        {
            ScEquipableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);

                }
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(ScEquipableItem item)
    {
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            inventory.AddItem(item);
        }
    }
}
