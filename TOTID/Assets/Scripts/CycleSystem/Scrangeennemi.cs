using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrangeennemi : Scennemimanager
{
    // Start is called before the first frame update
    void Start()
    {
        this.ennemiHealth = 2;
        this.ennemimeRange = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            this.ennemiHealth -= 1;
        }

        if(this.ennemiHealth <= 0)
        {
            death();
        }
    }
}
