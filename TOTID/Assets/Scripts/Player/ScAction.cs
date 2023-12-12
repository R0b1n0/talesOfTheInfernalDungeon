using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAction : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    private ScMovement movementScript;

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
                    List<ScWayPoint> path = hit.transform.GetComponent<ScRoom>().FindPathBetween(hit.point, movementScript.GetCurrentCell());
                    
                    movementScript.SetPath(path);

                    
                    break;
            }
        }
    }

    public void ActionOnClick()
    {

    }
}
