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
    }

}
