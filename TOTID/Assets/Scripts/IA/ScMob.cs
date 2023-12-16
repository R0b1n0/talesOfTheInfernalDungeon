using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScMob : MonoBehaviour
{
    
    public int hp;
    public int maxActionPoint;
    [SerializeField] List<Transform> partolWayPos = new List<Transform>();
    protected int currentActionPoint;

    protected ScMovement playerMovementRef;
    public Transform myTrans;
    protected ScGps myGps = new ScGps();

    protected ScMoveNode scMoveNode ;

    public ScWayPoint currentCell;
    protected ScWayPoint nextCell;
    private List<ScWayPoint> path = new List<ScWayPoint>();
    private List<ScWayPoint> patrolWayPoint = new List<ScWayPoint>();
    private Vector3 previousPos;
    private int currentPatrolBranch;

    protected MobState myState;

    private void Awake()
    {
        myState = MobState.Idle;
        myTrans = transform;
        playerMovementRef = ScMovement.Instance;
        Sccyclemanager.instance.ennemiActionEvent.AddListener(Behave);
        Sccyclemanager.instance.GetANewListener();

        
    }

    protected void MoveToNextDestination()
    {
        if (path.Count > 0)
        {
            if (Vector3.Distance(myTrans.position, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0)) < 0.1f)
            {
                previousPos = path[path.Count - 1].wayPointId;
                currentCell = path[path.Count - 1];
                path.Remove(path[path.Count - 1]);
                currentActionPoint--;
            }
            else      
            {
                myTrans.position = Vector3.Lerp(myTrans.position, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0), Vector3.Distance(previousPos, path[path.Count - 1].wayPointId + new Vector3(0, 1, 0)) / 50);
                myTrans.forward = (path[path.Count - 1].wayPointId - currentCell.wayPointId);
            }
        }
        

    }
    protected ScWayPoint FindClosestWayPoint(Vector3 pos)
    {
        Ray groundCheck = new Ray(pos, Vector3.down);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(groundCheck, out hit);

        if (hit.collider != null)
        {
            return hit.transform.GetComponent<ScRoom>().FindClossestCell(hit.point);
        }
        else
        {
            Debug.Log("didn't find waypoint");
            return null;
        }

    }

    protected void FindPatrollingItinary()
    {
        if(path.Count == 0)
        {
            currentPatrolBranch++;

            if (currentPatrolBranch > (patrolWayPoint.Count - 1))
                currentPatrolBranch = 0;

            path = myGps.FindPath(currentCell, patrolWayPoint[currentPatrolBranch]);
        }
    }
    protected void CreatePatrolItinary()
    {
        foreach (Transform patrolPos in partolWayPos)
        {
            patrolWayPoint.Add(FindClosestWayPoint(patrolPos.position));
        }
    } //always put this one in the mob start 


    protected void FindPlayer()
    {
        if(path.Count == 0)
        {
            path = myGps.FindPath(currentCell, ScMovement.Instance.currentCell);
            path.Remove(path[0]);
        }
    }

    protected void ChasePlayer()
    {
        
    }


    protected void ActionEnd()
    { 
        if (currentActionPoint == 0)
        {
            Sccyclemanager.instance.answers++;
            myState = MobState.Idle;
        }
        else
        {
            Behave();
        }
    }
    public void SetState(MobState newState)
    {
        if(newState != myState)
        {
            path.Clear();
        }
        myState = newState;
    }

    protected virtual void Behave() { }
    protected virtual void Attack() { }
    protected virtual void GetDamage() { }


}


public enum MobState{
    Attack,
    Aggro,
    Idle,
    Patrol
}
