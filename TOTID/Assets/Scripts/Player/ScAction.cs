using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAction : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    [SerializeField] private int maxActionPoint;
    [SerializeField] ScActionPointDisplay actionDisplay;


    private ScMovement movementScript;
    private ScGps myTomTom = new ScGps();

    public int actionPoint;
    private bool canTriggerNewAction;
    private playerState mystate;


    private void Start()
    {
        movementScript = GetComponent<ScMovement>();
        actionPoint = maxActionPoint;
        canTriggerNewAction = true;
        mystate = playerState.idle;
        Sccyclemanager.instance.playerActionEvent.AddListener(PlayerTurnsBegin);
    }

    private void Update()
    {
        if (actionPoint > 0)
        {
            switch (mystate)
            {
                case playerState.idle:

                    break;

                case playerState.moving:
                    movementScript.MoveToNextCell();
                    break;
            }
        }
    }

    public void ActionOnRelease()
    {
        if (canTriggerNewAction) 
        {
            Ray ray = new Ray(cameraHolder.position, cameraHolder.forward);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(ray, out hit);

            if (hit.collider != null)
            {
                switch (hit.transform.gameObject.layer)
                {
                    case 6:
                        mystate = playerState.moving;
                        ScWayPoint destination = hit.transform.GetComponent<ScRoom>().FindClossestCell(hit.point);
                        if (destination != null)
                        {
                            movementScript.SetPath(myTomTom.FindPath(movementScript.GetCurrentCell(), destination));
                            canTriggerNewAction = false;
                        }
                        else
                            mystate = playerState.idle;
                        break;
                }
            }
        }
        
    }

    public void UseOneActionPoint()
    {
        actionPoint--;
        actionDisplay.ActionPointText();
        if (actionPoint == 0)
        {
            Sccyclemanager.instance.PlayerTurnOver();
        }
    }

    public void CanTriggerNewAction( bool canAct)
    {
        canTriggerNewAction = canAct;
    }

    public void SetPlayerState(playerState newState)
    {
        mystate = newState;
    }

    private void PlayerTurnsBegin()
    {
        actionDisplay.ActionPointText();
        actionPoint = maxActionPoint;
    }
}


public enum playerState
{
    moving,
    idle
}