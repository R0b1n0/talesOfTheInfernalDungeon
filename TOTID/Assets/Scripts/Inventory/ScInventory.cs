using UnityEngine;
using static UnityEditor.Progress;

public class ScInventory : MonoBehaviour
{
    [SerializeField] private ScInventoryDisplay display;
    [SerializeField] private ScInventoryData data;
    private void Awake()
    {
        int slotCount = display.Initializes(this);

        data = new ScInventoryData(slotCount);

        display.UpdateDisplay(data.items);
    }

    public ScItem AddItem(ScItem item)
    {
        if (!data.SlotAvailable(item)) { return item; } 
        
        data.AddItem(ref item);

        display.UpdateDisplay(data.items);

        return item;
    }

    public ScItem PickItem(int slotId)
    {
        ScItem result = data.Pick(slotId);

        display.UpdateDisplay(data.items);
        return result;
    }

    public void SwapSlots(int slotA, int slotB)
    {
        data.Swap(slotA, slotB);

        display.UpdateDisplay(data.items);
    }

    public ScItem[] Data => data.items;
}
