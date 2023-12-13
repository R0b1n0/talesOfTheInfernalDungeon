using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class ScEnnemiAction : MonoBehaviour
{
    ScWayPoint currentCell;
    private ScMovement movementScript;
    private List<ScWayPoint> path = new List<ScWayPoint>();
    private ScWayPoint neighbor;
    Vector3 Previouspos;

    bool EnnemiTurn = true;
    private GameObject rng;
    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<ScMovement>();
    }

    private void  ActionSelected()
    {
        
    }


    private void SetCurrentEnnemiCell(ScWayPoint myCurrentCell)
    {
        currentCell = myCurrentCell;
    }
    // Update is called once per frame
    void Update()
    {
        if (EnnemiTurn == true)
        {
            
            //List<ScWayPoint> path = hit.transform.GetComponent<ScRoom>().FindPathBetween(hit.point, movementScript.GetCurrentCell());

             
        }
    }
}
