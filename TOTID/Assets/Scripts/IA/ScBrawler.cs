using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ScBrawler : ScMob
{
    private EWNode root;

    protected override void Behave()
    {
        if (currentActionPoint == 0)
            currentActionPoint = maxActionPoint;

        root.Evaluate();

        
    }

    private void Start()
    {
        root = new EWSelector(new List<EWNode> { new ScMoveNode(this)});
        
        currentCell = FindClosestWayPoint(transform.position);
        myTrans.position = currentCell.wayPointId + new Vector3(0, 1, 0);

        CreatePatrolItinary();
    }

    private void Update()
    {
        switch (myState)
        {
            case MobState.Patrol:
                FindPatrollingItinary();
                MoveToNextDestination();
                ActionEnd();
                break;
            case MobState.Aggro:
                ChasePlayer();
                MoveToNextDestination();
                if(Vector3.Distance(ScMovement.Instance.currentCell.wayPointId, currentCell.wayPointId) > 30)
                {
                    SetState(MobState.Idle);
                }
                ActionEnd();
                break;
            case MobState.Attack:  
                break;
        }
    }
}
