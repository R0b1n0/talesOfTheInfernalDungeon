using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scprojectile : MonoBehaviour
{
    bool canMove;
    int speed = 3;
    
    Vector3 actualPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canMove)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            canMove = !canMove;
        }
    }
}
