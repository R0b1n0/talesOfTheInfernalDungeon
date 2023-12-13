using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scplayercycle : MonoBehaviour
{
    private float timer = 3;
    private Sccyclemanager sccyclemanager;
    bool tic = false;
    // Start is called before the first frame update

    private void playerDebugTest()
    {
       tic = true;
    }

    private void Start()
    {
        sccyclemanager = Sccyclemanager.instance;
        sccyclemanager.playerActionEvent.AddListener(playerDebugTest);
    }

    // Update is called once per frame
    private void Update()
    {

        //Modifier pour : Action du joueur qui déclanche le changement de cycle
        if (tic == true)
        {
            if (timer > 0)
            {                
                    timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    sccyclemanager.playerListener.Add(this.gameObject);
                    timer = 5;
                    tic = false;
                    Debug.Log("ennemi invoked");
                }
            }
        }  
    }
}
