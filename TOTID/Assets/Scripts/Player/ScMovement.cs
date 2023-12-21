using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMovement : MonoBehaviour
{
    public static ScMovement Instance;

    public ScWayPoint currentCell;
    [SerializeField] LayerMask playerCollMask;
    [SerializeField] Collider playerCollider;
    public List<ScWayPoint> path = new List<ScWayPoint>();
    private ScAction actionScript;
    Vector3 previousPos;
    private Transform myTrans;
    [SerializeField] Transform feetPos;


    LineRenderer linePath;
    public List<Vector3> pathPositions = new List<Vector3>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        linePath = GetComponent<LineRenderer>();
        FindFIrstCell();
        actionScript = GetComponent<ScAction>();
        myTrans = transform;
    }


    public void SetCurrentCell(ScWayPoint myCurrentCell)
    {
        currentCell = myCurrentCell;
    }

    private void FindFIrstCell()
    {
        Ray groundCheck = new Ray(feetPos.position, -Vector3.up);
        RaycastHit hit = new RaycastHit();

        Physics.Raycast(groundCheck, out hit, 3, ~playerCollMask);


        currentCell = hit.transform.GetComponent<ScRoom>().FindClossestCell(transform.position);
        transform.position = currentCell.wayPointId + new Vector3(0, 1, 0);
        playerCollider.enabled = true;
    }

    public ScWayPoint GetCurrentCell()
    {
        return currentCell;
    }

    private void Update(){
        if(path.Count > 0 ) 
        { 
            if (linePath.positionCount > 0 ) 
                linePath.SetPosition(linePath.positionCount - 1, myTrans.position - new Vector3(0, 0.6f, 0));
        }
    }

    public void MoveToNextCell()
    {
        if (Vector3.Distance(transform.position, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0)) < 0.1f)
        {
            previousPos = path[path.Count - 1].wayPointId;
            currentCell = path[path.Count - 1];
            path.Remove(path[path.Count - 1]);
            actionScript.UseOneActionPoint();
            pathPositions.RemoveAt(pathPositions.Count - 1);
            linePath.positionCount = pathPositions.Count;

            if (path.Count == 0)
            {
                ResetLine();
                actionScript.CanTriggerNewAction(true);
                actionScript.SetPlayerState(playerState.idle);
            }
        }
        else
        {
            DrawLine();
            myTrans.position = Vector3.Lerp(myTrans.position, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0), Vector3.Distance(previousPos, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0)) / 100);
        }
           
    }

    public void SetPath(List<ScWayPoint> newPath)
    {
        path = newPath;

        for (int i = 0; i < path.Count; i++) {
            pathPositions.Add(path[i].wayPointId + new Vector3(0,0.4f,0));
        }
        pathPositions.Add(currentCell.wayPointId + new Vector3(0, 0.4f, 0));
    }

    public void ResetLine() {
        linePath.positionCount = 0;
        pathPositions.Clear();
    }

    public void DrawLine () {
        linePath.positionCount = pathPositions.Count;
        linePath.SetPosition(linePath.positionCount-1, myTrans.position);
        for (int u=0; u<pathPositions.Count; u++) { 
            linePath.SetPosition(u, pathPositions[u]);
        }
    }
}
