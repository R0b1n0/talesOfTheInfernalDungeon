using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScDoor : ScInteractible {

    public void DoorOpen() {
        Debug.Log("Door Open");
    }
    public override void Interact(){
        DoorOpen();
    }
}
