using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScMoveNode : EWNode
{
    ScMob mob;

    public ScMoveNode( ScMob mobRef)
    {
        mob = mobRef;
    }

    public override EWNodeState Evaluate()
    {
        mob.SetState( MobState.Patrol );
        return EWNodeState.FAILURE;
    }
}
