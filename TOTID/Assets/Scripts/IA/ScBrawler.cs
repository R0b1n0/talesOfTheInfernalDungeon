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

        switch (myState)
        {
            case MobState.Patrol:
                RandomMove();
                break;
        }
    }

    private void Start()
    {
        root = new EWSelector(new List<EWNode> { new ScMoveNode(this)});
        FindFirstWayPoint();
    }

    private void Update()
    {
        switch (myState) 
        {
            case MobState.Patrol:
                if (currentActionPoint > 0)
                {
                    MoveToNextDestination();
                }
                break;
        }
    }
}
