using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scmeleeennemi : Scennemimanager
{
    // Start is called before the first frame update
    void Start()
    {
        this.ennemiDamage = 10;
        this.ennemiHealth = 5;
        this.ennemiSpeed = 1;
        this.ennemimeRange = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.ennemiHealth -= 1;
        }

        if(ennemiHealth <= 0)
        {
            death();
        }
    }
}