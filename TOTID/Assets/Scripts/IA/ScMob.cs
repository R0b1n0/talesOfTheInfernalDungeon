using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScMob : MonoBehaviour
{
    
    public int hp;
    public int maxActionPoint;
    protected int currentActionPoint;

    protected ScMovement playerMovementRef;
    protected Transform myTrans;
    protected ScGps myGps = new ScGps();

    protected ScMoveNode scMoveNode ;

    protected ScWayPoint currentCell;
    protected ScWayPoint nextCell;
    private List<ScWayPoint> path = new List<ScWayPoint>();
    private Vector3 previousPos;


    protected MobState myState;

    private void Awake()
    {
        myState = MobState.Aggro;
        myTrans = transform;
        playerMovementRef = ScMovement.Instance;
        Sccyclemanager.instance.ennemiActionEvent.AddListener(Behave);
    }

    protected void FindFirstWayPoint()
    {
        Ray groundCheck = new Ray(transform.position, Vector3.down);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(groundCheck, out hit);

        if (hit.collider != null)
        {
            currentCell = hit.transform.GetComponent<ScRoom>().FindClossestCell(hit.point);
            myTrans.position = currentCell.wayPointId + new Vector3(0, 1, 0);
        }
    }

    protected virtual void Behave() { }
    protected virtual void Attack() { }
    protected virtual void GetDamage() { }
    protected void RandomMove()
    {
        List<ScWayPoint> directNeighbors = new List<ScWayPoint>() ;
        directNeighbors = currentCell.GetAllNeighbors();
        nextCell = directNeighbors[Random.Range(0,directNeighbors.Count - 1)];
        path.Add(nextCell);
    }
    protected void MoveToNextDestination()
    {
        if (Vector3.Distance(myTrans.position, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0)) < 0.1f)
        {
            previousPos = path[path.Count - 1].wayPointId;
            currentCell = path[path.Count - 1];
            path.Remove(path[path.Count - 1]);
            currentActionPoint--;
            if (currentActionPoint > 0)
            {
                Behave();
            }
        }
        else
            myTrans.position = Vector3.Lerp(myTrans.position, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0), Vector3.Distance(previousPos, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0)) / 50);
        
        
    }
    public void SetState(MobState newState)
    {
        myState = newState;
    }
}


public enum MobState{
    Attack,
    Aggro,
    Idle,
    Patrol
}
