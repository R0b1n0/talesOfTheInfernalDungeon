using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScDrawPath : MonoBehaviour{

    LineRenderer linePath;
    public Transform[] pathPositions;

    void Start() {
        linePath = GetComponent<LineRenderer>();
        pathPositions = GameObject.Find("Player").GetComponent<ScMovement>().path;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
