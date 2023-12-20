using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Sccyclemanager : MonoBehaviour
{
    public static Sccyclemanager instance;


    public int ennemiAlive;
    public int answers;
    public UnityEvent ennemiActionEvent;
    public UnityEvent playerActionEvent;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        if(ennemiActionEvent == null)
        {
            ennemiActionEvent = new UnityEvent();
        }
        playerActionEvent.Invoke();
    }

    void Update()
    {
        if (answers == ennemiAlive)
        {
            answers = 0;
            playerActionEvent.Invoke();
        }//all mobs finished their moves 
    }

    public void GetANewListener()
    {
        ennemiAlive += 1;
    }

    public void AMobDied()
    {
        ennemiAlive --;
    }

    public void PlayerTurnOver()
    {
        if (ennemiAlive > 0)
        {
            ennemiActionEvent.Invoke();
            answers = 0;
        }
        else 
            playerActionEvent.Invoke();
    }
}
