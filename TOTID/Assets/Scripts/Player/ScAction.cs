using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ScAction : MonoBehaviour {
    [SerializeField] Transform cameraHolder;
    private ScMovement movementScript;
    private Ray viewRay;
    private RaycastHit viewHit;
    private ScWayPoint endPath;
    public GameObject highlight;
    public Transform highlightTransform;

    private void Start() {
        movementScript = GetComponent<ScMovement>();
        viewHit = new RaycastHit();
    }

    private void Update() {
        viewRay = new Ray(cameraHolder.position, cameraHolder.forward);
        Physics.Raycast(viewRay, out viewHit);
        Preview();
    }

    public void ActionOnRelease() {
        if (viewHit.collider != null) {
            switch (viewHit.transform.gameObject.layer) {
                case 6:
                    List<ScWayPoint> path = viewHit.transform.GetComponent<ScRoom>().FindPathBetween(viewHit.point, movementScript.GetCurrentCell());
                    
                    movementScript.SetPath(path);
                    
                    break;
            }
        }
    }

    public void Preview() {

        if (viewHit.collider != null)
        {
            switch (viewHit.transform.gameObject.layer)
            {
                case 6:
                    endPath = viewHit.transform.GetComponent<ScRoom>().FindClossestCell(viewHit.point);
                    if (endPath != null)
                    {
                        highlight.SetActive(true);
                        highlightTransform.position = endPath.wayPointId;

                    }
                    break;
            }
        }
        else
        {
            highlight.SetActive(false);
        }
        
    }

    public void ActionOnClick() {

    }
}
