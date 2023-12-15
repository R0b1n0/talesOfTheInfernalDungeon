using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMovement : MonoBehaviour
{
    public static ScMovement Instance;

    public ScWayPoint currentCell;
    private List<ScWayPoint> path = new List<ScWayPoint>();
    private ScAction actionScript;
    Vector3 previousPos;
    [SerializeField] Transform feetPos;

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
        actionScript = GetComponent<ScAction>();
    }


    public void SetCurrentCell(ScWayPoint myCurrentCell)
    {
        currentCell = myCurrentCell;
    }

    private void FindFIrstCell()
    {
        Ray groundCheck = new Ray(feetPos.position, Vector3.down);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(groundCheck, out hit);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            currentCell = hit.transform.GetComponent<ScRoom>().FindClossestCell(hit.point);
            transform.position = currentCell.wayPointId + new Vector3(0, 1, 0);
        }
    }

    public ScWayPoint GetCurrentCell()
    {
        return currentCell;
    }
    public void MoveToNextCell()
    {
        if (Vector3.Distance(transform.position, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0)) < 0.1f)
        {
            previousPos = path[path.Count - 1].wayPointId;
            currentCell = path[path.Count - 1];
            path.Remove(path[path.Count - 1]);
            actionScript.UseOneActionPoint();
            if (path.Count == 0)
            {
                actionScript.CanTriggerNewAction(true);
                actionScript.SetPlayerState(playerState.idle);
            }
        }
        else
            transform.position = Vector3.Lerp(transform.position, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0), Vector3.Distance(previousPos, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0)) / 100);
    }

    public void SetPath(List<ScWayPoint> newPath)
    {
        path = newPath;
    }
}
