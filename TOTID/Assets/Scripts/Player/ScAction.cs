using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScAction : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    [SerializeField] private int maxActionPoint;

    [SerializeField] ScCharacter teamData;
    [SerializeField] TextMeshProUGUI actionPointText;


    private ScMovement movementScript;
    private ScAttack attackScript;
    
    private ScGps myTomTom = new ScGps();

    private int actionPoint;
    private bool canTriggerNewAction;
    private playerState mystate;


    private void Start()
    {
        movementScript = GetComponent<ScMovement>();
        attackScript = GetComponent<ScAttack>();
        actionPoint = maxActionPoint;
        UpdateActionPointDisplay();
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

                    case 7:
                        hit.transform.GetComponent<ScInteractible>().Interact();
                        break;

                    case 10:
                        // mob
                        ScMob mob = hit.collider.transform.GetComponent<ScMob>();
                        ScWayPoint mobWaypoint = mob.currentCell;
                        foreach(ScWayPoint neighbour in movementScript.currentCell.GetAllNeighbors())
                        {
                            if(neighbour == mobWaypoint)
                            {
                                attackScript.AttackPart();
                                hit.collider.transform.GetComponent<ScMob>().TakeDamage((int)teamData.characterData[teamData.characterIndex].strength.valueSc);
                                UseOneActionPoint();
                                break; 
                            }
                        }
                        break;
                }
            }
        }
        
    }

    public void UseOneActionPoint()
    {
        actionPoint--;
        UpdateActionPointDisplay();
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
        actionPoint = maxActionPoint;
        UpdateActionPointDisplay();
    }

    private void UpdateActionPointDisplay()
    {
        actionPointText.text = actionPoint.ToString();
    }
}


public enum playerState
{
    moving,
    idle
}