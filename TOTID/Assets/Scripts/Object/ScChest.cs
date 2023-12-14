using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScChest : ScInteractible
{
    public void ChestOpen()
    {
        Debug.Log("Chest Open");
    }
    public override void Interact()
    {
        ChestOpen();
    }
}
