using UnityEngine;

public class ScInventoryDisplay : MonoBehaviour
{
    private Scslot[] slots;
    private int draggedSlotsIndex;
    private ScInventory inventori;

    public int Initializes(ScInventory inventory)
    {
        slots = GetComponentsInChildren<Scslot>();
        inventori = inventory;
        for (var i = 0; i < slots.Length; i++)
        {
            slots[i].Initialize(this, i);
        }

        return slots.Length;
    }

    public void UpdateDisplay(ScItem[] item)
    {
        for (var i = 0;i < item.Length;i++)
        {
            slots[i].UpdateDisplay(item[i]);
        }
    }

    #region inputs
    public void ClickSlot(int _index)
    {
        Debug.Log($"Clic on slot {_index}");
    }
    public void DragSlots(int index) => draggedSlotsIndex = index;

    public void DropOnSlots(int index)
    {
        inventori.SwapSlots(index, draggedSlotsIndex);
    }
    #endregion
}
