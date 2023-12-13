using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMovement : MonoBehaviour
{
    public static ScMovement Instance;
    private ScWayPoint currentCell;
    private List<ScWayPoint> path = new List<ScWayPoint>();
    Vector3 previousPos;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        FindFIrstCell();
    }

    private void Update()
    {
        if (path.Count > 0) 
        {
            if (Vector3.Distance(transform.position,path[path.Count-1].wayPointId + new Vector3(0, 1, 0)) < 0.1f)
            {
                previousPos = path[path.Count - 1].wayPointId;
                currentCell = path[path.Count - 1];
                path.Remove(path[path.Count-1]);
            }
            else
                transform.position = Vector3.Lerp(transform.position, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0), Vector3.Distance(previousPos, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0)) / 50);
        }
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
