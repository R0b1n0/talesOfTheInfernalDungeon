using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTEst : MonoBehaviour
{
    [SerializeField] private ScItem itemToPush, pickedItem;
    private ScInventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<ScInventory>();
        
    }

    public void Start()
    {
        Add();
    }

    [ContextMenu("Test Push")]
    public void Add()
    {
        itemToPush = inventory.AddItem(itemToPush);
    }

    [ContextMenu("Test pick")]
    public void Pick()
    {
        pickedItem = inventory.PickItem(1);
    }
}
