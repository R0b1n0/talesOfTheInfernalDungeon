using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScDoor : ScInteractible {
    private bool isDoorOpen = false;
    private Transform parentTransform;

    private void Start()
    {
        parentTransform = transform.root;
    }
    public void DoorOpen() {
        Debug.Log("Door Open");
        isDoorOpen = true;
        parentTransform.Rotate(0, -90, 0);
    }

    public void DoorClose(){
        Debug.Log("Door Close");
        isDoorOpen = false;
        parentTransform.Rotate(0, 90, 0);
    }

    public override void Interact(){
        if (isDoorOpen) { DoorClose(); }
        else if (!isDoorOpen) { DoorOpen(); }
    }
}
