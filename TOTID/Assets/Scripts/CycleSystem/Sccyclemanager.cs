using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Sccyclemanager : MonoBehaviour
{
    public static Sccyclemanager instance;


    int ennemiAlive;
    bool ennemiListFull = false;
    bool playerListFull = false;

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
        ennemiAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && ennemiActionEvent != null)
        {
            ennemiActionEvent.Invoke();
        }

        if( ennemiListFull == false )
        {
            // remplacer le int part un int = le nombres d'ennemis en vie
            if (ennemiAlive == 3)
            {
                ennemiListFull = true;
            }
        }

        if(playerListFull == false )
        {
            if (ennemiAlive == 1)
            {
                playerListFull = true;
            }
        } 
    }

    public void PlayerTurnOver()
    {
        ennemiActionEvent.Invoke();
    }
}
