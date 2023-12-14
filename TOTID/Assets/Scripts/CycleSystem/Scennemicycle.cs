using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scennemicycle : MonoBehaviour
{

    private Sccyclemanager sccyclemanager;
   

    private void Start()
    {
        sccyclemanager = Sccyclemanager.instance;
    }
    private void Update()
    {
        // Modifier pour : Action des ennemis qui déclanche l'incrementation de la list pour le changement de cycle
        
    }
}
