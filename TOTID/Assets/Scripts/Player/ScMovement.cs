using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMovement : MonoBehaviour
{
    private ScWayPoint currentCell;
    private List<ScWayPoint> path = new List<ScWayPoint>();

    private void Start()
    {
        FindFIrstCell();
    }

    public void SetCurrentCell(ScWayPoint myCurrentCell)
    {
        currentCell = myCurrentCell;
    }

    private void FindFIrstCell()
    {
        Ray groundCheck = new Ray(transform.position, Vector3.down);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(groundCheck, out hit);

        if (hit.collider != null)
        {
            currentCell = hit.transform.GetComponent<ScRoom>().FindClossestCell(hit.point);
            transform.position = currentCell.wayPointId + new Vector3(0, 1, 0);
        }
    }

    public ScWayPoint GetCurrentCell()
    {
        return currentCell;
    }

    public void SetPath(List<ScWayPoint> newPath)
    {
        path = newPath;
    }
}
