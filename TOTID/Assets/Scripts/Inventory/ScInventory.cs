using System;
using System.Collections.Generic;
using UnityEngine;

public class ScInventory : MonoBehaviour
{
    [SerializeField] List<ScItem> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ScItemSlot[] itemSlots;
    public event Action<ScItem> OnItemRightClickedEvent;
    private void Awake()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }
    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ScItemSlot>();
        RefreshUI();
    }
    private void RefreshUI()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i];
        }
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }
    public bool AddItem(ScItem item)
    {
        if (IsFull())
            return false;
        items.Add(item);
        RefreshUI();
        return true;
    }
    public bool RemoveItem(ScItem item)
    {
        if (items.Remove(item))
        {
            RefreshUI();
            return true;
        }
        return false;
    }
    public bool IsFull()
    {
        return items.Count >= itemSlots.Length;
    }
}
