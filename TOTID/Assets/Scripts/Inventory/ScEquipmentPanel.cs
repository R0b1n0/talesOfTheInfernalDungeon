using System;
using UnityEngine;

public class ScEquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    public ScEquipmentSlot[] equipmentSlots;

    public event Action<ScItemSlot> OnPointerExitEvent;
    public event Action<ScItemSlot> OnPointerEnterEvent;
    public event Action<ScItemSlot> OnRightClickEvent;
    public event Action<ScItemSlot> OnBeginDragEvent;
    public event Action<ScItemSlot> OnEndDragEvent;
    public event Action<ScItemSlot> OnDragEvent;
    public event Action<ScItemSlot> OnDropEvent;


    private void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            equipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            equipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
            equipmentSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            equipmentSlots[i].OnEndDragEvent += OnEndDragEvent;
            equipmentSlots[i].OnDragEvent += OnDragEvent;
            equipmentSlots[i].OnDropEvent += OnDropEvent;
        }
    }
    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<ScEquipmentSlot>();
    }

    public bool AddItem(ScEquipableItem item, out ScEquipableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].EquipmentType == item.equipmentType)
            {
                previousItem = (ScEquipableItem)equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(ScEquipableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].Item == item)
            {
                equipmentSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
}
