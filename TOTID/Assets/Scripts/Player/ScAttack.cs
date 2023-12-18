using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAttack : MonoBehaviour
{
    private int damage;
    public ScMob mob;
    private ScMovement movementScript;
    private void Start()
    {
        movementScript = GetComponent<ScMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mob != null) 
        {
            if (ScMovement.Instance.currentCell.GetAllNeighbors().Contains(mob.currentCell))
            {
                Debug.Log("Attack");
            }
        }
    }
}
