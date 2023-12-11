using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Sccyclemanager : MonoBehaviour
{
    public static Sccyclemanager instance;
    public List<GameObject> ennemiListener = new List<GameObject>();
    public List<GameObject> playerListener = new List<GameObject>();
    bool ennemiListFull = false;
    bool playerListFull = false;
    private void Awake()
    {
       if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public UnityEvent ennemiActionEvent;
    public UnityEvent playerActionEvent;
    // Start is called before the first frame update
    void Start()
    {
        if(ennemiActionEvent == null)
        {
            ennemiActionEvent = new UnityEvent();
        }
       
    }


    private void ennemiListClear()
    {
        playerActionEvent.Invoke();
        ennemiListener.Clear();
        ennemiListFull = false;
    }

    private void playerListClear()
    {
        ennemiActionEvent.Invoke();
        playerListener.Clear();
        playerListFull = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && ennemiActionEvent != null)
        {
            ennemiActionEvent.Invoke();
        }

        if( ennemiListFull == false )
        {
            // remplacer le int part un int = le nombres d'ennemis en vie
            if (ennemiListener.Count == 3)
            {
                ennemiListFull = true;
                ennemiListClear();
            }
        }

        if(playerListFull == false )
        {
            if (playerListener.Count == 1)
            {
                playerListFull = true;
                playerListClear();
            }
        } 
    }
}
