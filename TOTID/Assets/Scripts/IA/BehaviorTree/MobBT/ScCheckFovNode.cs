using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScCheckFovNode : EWNode
{

    ScMob mob;
    RaycastHit hitPlayer = new RaycastHit();
    Ray rayToPlayer;
    public ScCheckFovNode (ScMob mobRef)
    {
        mob = mobRef;
    }

    public override EWNodeState Evaluate()
    {
        if(Vector3.Distance(ScMovement.Instance.currentCell.wayPointId, mob.currentCell.wayPointId) < mob.agroDistance )
        {
            if (Vector3.Angle(mob.myTrans.forward, ScMovement.Instance.currentCell.wayPointId - mob.currentCell.wayPointId) < mob.fovMaxAngle)
            {
                rayToPlayer = new Ray(mob.eyesPos.position, (ScMovement.Instance.currentCell.wayPointId + new Vector3(0, 1, 0)) - mob.eyesPos.position);
                Physics.Raycast(rayToPlayer, out hitPlayer);

                if (hitPlayer.collider != null)
                {
                    if(hitPlayer.collider.transform.gameObject.layer == 8)
                    {
                        mob.SetState(MobState.Aggro);
                        return EWNodeState.SUCCESS;
                    }
                }
            }
        }

        
        return EWNodeState.FAILURE;
        
    }
}
