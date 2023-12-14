using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class ScEnnemiAction : MonoBehaviour
{
    ScWayPoint currentCell;
    private ScEnnemiMouvement ennemiMovementScript;
    private List<ScWayPoint> path = new List<ScWayPoint>();
    private ScWayPoint neighbor;

    Vector3 Previouspos;

    bool EnnemiTurn = true;
   
    // Start is called before the first frame update
    void Start()
    {
        ennemiMovementScript = GetComponent<ScEnnemiMouvement>();
    }

    private void  MoveRandom()
    {
        
    }


  
    // Update is called once per frame
    void Update()
    {
        if (EnnemiTurn == true)
        {
            EnnemiTurn = false;
            //List<ScWayPoint> path = hit.transform.GetComponent<ScRoom>().FindPathBetween(hit.point, movementScript.GetCurrentCell());

             
        }
    }
}
