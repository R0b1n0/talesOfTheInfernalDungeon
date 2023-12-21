using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScDestroyableWall : ScInteractible {
    private GameObject wall;

    private void Awake(){
        wall.SetActive(true);
    }

    public void WallDisable() { 
        wall.SetActive(false);
    }

    public override void Interact() {
        WallDisable();
    }
}
