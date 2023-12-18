using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScHasAgrro : EWNode
{
    ScMob mob;
    public ScHasAgrro (ScMob scriptRef)
    {
        mob = scriptRef;
    }

    public override EWNodeState Evaluate()
    {
        if (mob.GetState() == MobState.Aggro && Vector3.Distance(ScMovement.Instance.currentCell.wayPointId, mob.currentCell.wayPointId) < mob.looseAgroDistance)
        {
            return EWNodeState.SUCCESS;
        }
        else
        {
            mob.SetState(MobState.Patrol);
            return EWNodeState.FAILURE;
        }
            
    }
}
