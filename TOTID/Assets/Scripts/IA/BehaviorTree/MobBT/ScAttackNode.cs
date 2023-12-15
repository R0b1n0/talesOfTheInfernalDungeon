using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAttackNode : EWNode
{
    ScMob mob;
    public ScAttackNode(ScMob mobRef)
    {
        mob = mobRef;
    }

    public override EWNodeState Evaluate()
    {
        if (Vector3.Distance(ScMovement.Instance.currentCell.wayPointId, mob.currentCell.wayPointId) == 1)
        {
            mob.SetState(MobState.Attack);
            return EWNodeState.SUCCESS;
        }
        return EWNodeState.FAILURE;
    }
}
