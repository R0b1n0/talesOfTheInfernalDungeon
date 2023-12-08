using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scennemicycle : MonoBehaviour
{
    bool tic = false;
    public float timer = 5;
    private Sccyclemanager sccyclemanager;
   
    private void ennemiDebugTest()
    {
        tic = true;
    }

    private void Start()
    {
        sccyclemanager = Sccyclemanager.instance;
        sccyclemanager.ennemiActionEvent.AddListener(ennemiDebugTest);
    }
    private void Update()
    {
        // Modifier pour : Action des ennemis qui déclanche l'incrementation de la list pour le changement de cycle
        if (tic == true)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    sccyclemanager.ennemiListener.Add(this.gameObject);
                    timer = 5;
                    tic = false;
                    Debug.Log("player invoked");
                }
            }
        }
    }
}
