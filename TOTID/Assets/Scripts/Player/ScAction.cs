using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAction : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    private ScMovement movementScript;
    private ScGps myTomTom = new ScGps();
    [SerializeField] private int maxAactionPoint;
    private int currentActionPoint;


    private void Start()
    {
        movementScript = GetComponent<ScMovement>();
    }

    public void ActionOnRelease()
    {
        Ray ray = new Ray(cameraHolder.position, cameraHolder.forward);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            switch (hit.transform.gameObject.layer)
            {
                case 6:
                    ScWayPoint destination = hit.transform.GetComponent<ScRoom>().FindClossestCell(hit.point);

                    
                    movementScript.SetPath(myTomTom.FindPath(movementScript.GetCurrentCell(), destination));

                    
                    break;
            }
        }
    }

    public void ActionOnClick()
    {

    }

    public void UseOneActionPoint()
    {
        currentActionPoint--;
        if (currentActionPoint == 0)
        {
            Sccyclemanager.instance.PlayerTurnOver();
        }
    }
}
