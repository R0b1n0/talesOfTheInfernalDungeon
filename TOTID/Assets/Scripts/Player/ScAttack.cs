using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAttack : MonoBehaviour
{
    private int damage;

    public ScMob mob;
    Ray raytoIA;
    RaycastHit hitIA = new RaycastHit();
    private ScMovement movementScript;
    private void Start()
    {
        movementScript = GetComponent<ScMovement>();

    }

    // Update is called once per frame
    void Update()
    {
            raytoIA = new Ray(Vector3.forward, this.transform.position + new Vector3(0, 1, 0));
            if (Physics.Raycast(raytoIA, out hitIA))
            {
                Debug.Log(hitIA.collider);
                if (ScMovement.Instance.currentCell.GetAllNeighbors().Contains(mob.currentCell))
                {
                    Debug.Log("IACLOSE");
                    if (hitIA.collider != null)
                    {
                        mob.hp -= 10;
                    }
                }
            }
    }
}
