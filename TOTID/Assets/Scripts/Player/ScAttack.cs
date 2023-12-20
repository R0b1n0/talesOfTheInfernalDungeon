using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAttack : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem slash;
    

    // Update is called once per frame
    public void AttackPart()
    {
        //mobToAttack.TakeDamage(damage);
            slash.Play();   
    }
}
