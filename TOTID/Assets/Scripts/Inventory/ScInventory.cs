using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScInventory : MonoBehaviour
{
    [SerializeField] List<ScItem> startingItems;
    [SerializeField] Transform itemsParent;
    [SerializeField] ScItemSlot[] itemSlots;


    public event Action<ScItemSlot> OnPointerExitEvent;
    public event Action<ScItemSlot> OnPointerEnterEvent;
    public event Action<ScItemSlot> OnRightClickEvent;
    public event Action<ScItemSlot> OnBeginDragEvent;
    public event Action<ScItemSlot> OnEndDragEvent;
    public event Action<ScItemSlot> OnDragEvent;
    public event Action<ScItemSlot> OnDropEvent;


    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {            
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
        }

        SetStartingItems();
    }
    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ScItemSlot>();
        SetStartingItems();
    }
    private void SetStartingItems()
    {
        int i = 0;
        for (; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItems[i];
        }
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }
    public bool AddItem(ScItem item)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(ScItem item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                itemSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                return false;
            }
        }
        return true;
    }
}
