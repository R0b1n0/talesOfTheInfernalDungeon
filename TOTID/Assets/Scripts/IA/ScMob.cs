using CharactherStats;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScMob : MonoBehaviour
{
    [Header("Mob Parameters")]
    [SerializeField] public int hp;
    [SerializeField] protected int maxActionPoint;
    [SerializeField] protected int strenght;
    [SerializeField] public float fovMaxAngle;
    [SerializeField] public float agroDistance;
    [SerializeField] public float looseAgroDistance;
    [SerializeField] protected float patrolingSpeed; // !!! The higher this value is, the slower the mob will move 
    [SerializeField] protected float agroSpeed;     // !!! The higher this value is, the slower the mob will move
    [SerializeField] List<Transform> partolWayPos = new List<Transform>();
    [SerializeField] private ScSpriteTurn facePlayer;
    [SerializeField] private Sprite face;
    [SerializeField] private Sprite dos;

    [Header("Do not change")]
    public Transform myTrans;
    public ScWayPoint currentCell;
    [SerializeField] public Transform eyesPos;
    [SerializeField] SpriteRenderer myRenderer;
    

    protected int currentActionPoint;
    protected MobState myState;
    protected ScMovement playerMovementRef;
    protected ScGps myGps = new ScGps();
    protected ScWayPoint nextCell;

    private List<ScWayPoint> path = new List<ScWayPoint>();
    private List<ScWayPoint> patrolWayPoint = new List<ScWayPoint>();
    private Vector3 previousPos;
    private int currentPatrolBranch;
    private float onMoveLerp;
    private ScWayPoint playerPreviousCell = null;
    


    private void Awake()
    {
        SetState(MobState.Idle);
        myTrans = transform;
        playerMovementRef = ScMovement.Instance;
        Sccyclemanager.instance.ennemiActionEvent.AddListener(Behave);
        Sccyclemanager.instance.GetANewListener();
    }

    protected void MoveToNextDestination()
    {
        if (path.Count > 0)
        {
            if (Vector3.Distance(myTrans.position, path[path.Count - 1].wayPointId) < 0.1f)
            {
                
                previousPos = path[path.Count - 1].wayPointId;
                
                currentCell = path[path.Count - 1];
                
                path.Remove(path[path.Count - 1]);
                myRenderer.flipX = !myRenderer.flipX;
                currentActionPoint--;
            }
            else      
            {
                onMoveLerp = Vector3.Distance(previousPos, path[path.Count - 1].wayPointId);

                if (myState == MobState.Aggro)
                    myTrans.position = Vector3.Lerp(myTrans.position, path[path.Count - 1].wayPointId, onMoveLerp *agroSpeed *Time.deltaTime);
                else
                {
                    myTrans.position = Vector3.Lerp(myTrans.position, path[path.Count - 1].wayPointId, onMoveLerp * patrolingSpeed * Time.deltaTime);
                    myTrans.forward = (path[path.Count - 1].wayPointId - currentCell.wayPointId);
                }

                if (Vector3.Angle(myTrans.forward, ScMovement.Instance.currentCell.wayPointId - currentCell.wayPointId) > 90)
                {
                    myRenderer.sprite = dos;
                }
                else
                {
                    myRenderer.sprite = face;
                }
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
            Debug.Log("new branch ");

            if (currentPatrolBranch > (patrolWayPoint.Count - 1))
                currentPatrolBranch = 0;

            path = myGps.FindPath(currentCell, patrolWayPoint[currentPatrolBranch]);
            Debug.Log(path.Count);
        }
    }
    protected void CreatePatrolItinary()
    {
        foreach (Transform patrolPos in partolWayPos)
        {
            patrolWayPoint.Add(FindClosestWayPoint(patrolPos.position));
        }
    } //always put this one in the mob start 

    protected void Die()
    {
        Sccyclemanager.instance.AMobDied();
        Destroy(gameObject);
    }

    protected void FindPlayer()
    {
        if (ScMovement.Instance.currentCell != playerPreviousCell)
        {
            playerPreviousCell = ScMovement.Instance.currentCell;
            path.Clear();
            path = myGps.FindPath(currentCell, ScMovement.Instance.currentCell);
            path.RemoveAt(0);
        }
    }

    public void TakeDamage(int damageValue)
    {
        hp -= damageValue;
        if(hp <= 0)
        {
            Die();
        }
    }

    protected void AttackPlayer()
    {
        ScCharacter.Instance.takeDamage(strenght);
        Debug.Log("attackedPlayer");
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

    protected void FinishTurn()
    {
        Sccyclemanager.instance.answers++;
        myState = MobState.Idle;
    }
    public void SetState(MobState newState)
    {
        if(newState != myState)
        {
            path.Clear();
        }

        myState = newState;

        if (myState == MobState.Attack || myState == MobState.Aggro)
            facePlayer.enabled = true;
        else 
            facePlayer.enabled = false;
    }
    public MobState GetState()
    {
        return myState;
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
