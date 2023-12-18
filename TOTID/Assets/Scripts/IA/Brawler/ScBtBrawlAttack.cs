using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBtBrawlAttack : EWNode
{
    ScMob mob;
    List <ScWayPoint> neighbors;

    public ScBtBrawlAttack (ScMob mobRef)
    {
        mob = mobRef;
    }

    public override EWNodeState Evaluate()
    {
        neighbors = mob.currentCell.GetAllNeighbors();

        if (neighbors.Contains(ScMovement.Instance.currentCell))
        {
            Debug.Log("à l'attaque ");
            return EWNodeState.SUCCESS;
        }
        else 
            return EWNodeState.FAILURE;
    }
}
