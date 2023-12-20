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

        if (myState == MobState.Aggro)
        {
            FindPlayer();
        }
    }

    private void Start()
    {
        root = new EWSelector(new List<EWNode>  {
                new ScBtBrawlAttack(this),
                new EWSelector(new List<EWNode> {new ScCheckFovNode(this), new ScHasAgrro(this)}), 
                new ScMoveNode(this)});
        
        currentCell = FindClosestWayPoint(transform.position);
        myTrans.position = currentCell.wayPointId;

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
                MoveToNextDestination();
                ActionEnd();
                break;
            case MobState.Attack:
                //AttackPlayer();
                FinishTurn();
                //ActionEnd();
                break;
        }
    }
}
