public class ScInventoryData
{
    public ScInventoryData(int slotCount)
    {
        items = new ScItem[slotCount];
    }

    public ScItem[] items { private set; get; }

    public bool SlotAvailable(ScItem itemToAdd)
    {
        foreach (var item in items)
        {
            if(item.AvailableFor(itemToAdd)) return true;
        }

        return false; 
    }

    public void AddItem(ref ScItem itemAdd)
    {
        for (int i =  0; i < items.Length; i++)
        {
            if (itemAdd.Empty) return;
            if (items[i].AvailableFor(itemAdd))
            {
                items[i].Merge(ref itemAdd);
            }
        }
    }

    public ScItem Pick(int slotId)
    {
        if(slotId> items.Length) throw new System.Exception($"Id {slotId} out of Inventory");
        
        ScItem item = items[slotId];
        items[slotId] = new ScItem();

        return item;
    }

    public void Swap(int slotA, int slotB)
    {
        ScItem temp = items[slotA];

        items[slotA] = items[slotB];
        items[slotB] = temp;
    }
}
