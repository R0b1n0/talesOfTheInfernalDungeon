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
   
    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<ScMovement>();
    }

    private void  RaycastGround()
    {
        Ray groundCheck = new Ray(transform.position, Vector3.down);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(groundCheck, out hit);

        if (hit.collider != null)
        {
            switch (hit.transform.gameObject.layer)
            {
                case 6:
                    List<ScWayPoint> path = hit.transform.GetComponent<ScRoom>().FindClossestCell(ScMovement.Instance.currentCell.wayPointId).GetAllNeighbors();

                    movementScript.SetPath(path);


                    break;
            }
        }
    }


    private void OnActionTurn()
    {

    }

  
    // Update is called once per frame
    void Update()
    {
        if (EnnemiTurn == true)
        {
            RaycastGround();
            EnnemiTurn = false;
            //List<ScWayPoint> path = hit.transform.GetComponent<ScRoom>().FindPathBetween(hit.point, movementScript.GetCurrentCell());

             
        }
    }
}
