using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScCheckFovNode : EWNode
{

    ScMob mob;
    RaycastHit hitPlayer;
    Ray rayToPlayer;
    public ScCheckFovNode (ScMob mobRef)
    {

        mob = mobRef;
    }

    public override EWNodeState Evaluate()
    {
        if(Vector3.Distance(ScMovement.Instance.currentCell.wayPointId, mob.currentCell.wayPointId) < 20 )
        {
            
            if(Vector3.Angle(mob.myTrans.forward, ScMovement.Instance.currentCell.wayPointId - mob.currentCell.wayPointId) < 45 )
            {
                Debug.Log("playerInDistance");
                rayToPlayer = new Ray(mob.myTrans.position, ScMovement.Instance.currentCell.wayPointId + new Vector3(0,1,0));
                Physics.Raycast(rayToPlayer, out hitPlayer);
                if (hitPlayer.collider!= null)
                {
                    if(hitPlayer.collider.transform.gameObject.layer == 8)
                    {
                        Debug.Log("checkFov");
                        mob.SetState(MobState.Aggro);
                        return EWNodeState.FAILURE;
                    }
                }
            }
        }
        mob.SetState(MobState.Patrol);
        return EWNodeState.SUCCESS;
        
    }
}
