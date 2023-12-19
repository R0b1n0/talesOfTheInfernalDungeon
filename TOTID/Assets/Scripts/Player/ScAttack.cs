using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAttack : MonoBehaviour
{
    private int damage;
    private ScAction actionScript;
    [SerializeField]
    private ParticleSystem slash;
    public ScMob mob;
    Ray raytoIA;
    RaycastHit hitIA = new RaycastHit();
    [SerializeField]
 
    private ScMovement movementScript;
    private void Start()
    {
        movementScript = GetComponent<ScMovement>();
        actionScript =  GetComponent<ScAction>();
        //slash = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            raytoIA = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (ScMovement.Instance.currentCell.GetAllNeighbors().Contains(mob.currentCell))
            {
                if (Physics.Raycast(raytoIA, out hitIA))
                {
                    if (hitIA.collider != null)
                    {
                        if(mob.hp >= 0 )
                        {
                            //slash.transform.position = this.transform.position;
                            actionScript.UseOneActionPoint();
                            mob.hp -= 10;
                            slash.Play();
                        }
                        
                    }
                }
            }
        }
            
    }
}
