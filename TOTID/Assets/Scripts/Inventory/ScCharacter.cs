using UnityEngine;
using CharactherStats;

public class ScCharacter : MonoBehaviour
{

    public ScCharactereStats strength;
    public ScCharactereStats health;
    public ScCharactereStats a_p;
    
    [SerializeField] ScInventory inventory;
    [SerializeField] ScEquipmentPanel equipmentPanel;
    [SerializeField] ScStatsPanel statsPanel;


    private void Awake()
    {
        statsPanel.SetStats(strength, health, a_p);
        statsPanel.UpdateStatsValue();

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
                    previousItem.Unequip(this);
                    statsPanel.UpdateStatsValue();
                }
                item.Equip(this);
                statsPanel.UpdateStatsValue();
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
            item.Unequip(this);
            statsPanel.UpdateStatsValue();
            inventory.AddItem(item);
        }
    }
}
