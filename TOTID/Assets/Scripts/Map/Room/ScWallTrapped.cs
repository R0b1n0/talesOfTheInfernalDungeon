using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScWallTrapped : MonoBehaviour {
    [SerializeField] private GameObject arrow;
    private Ray trapRay;
    private RaycastHit trapHit;

    private void Start () { 
        trapHit = new RaycastHit();
    }

    private void Update() {
        trapRay = new Ray(transform.position, transform.forward);
        Physics.Raycast(trapRay, out trapHit);

        Debug.DrawRay(transform.position, transform.forward, Color.red);

        switch (trapHit.transform.gameObject.layer){
            case 8:
                Debug.Log("PlayeRFOUND");
                break;
            }
    }
}
