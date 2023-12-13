using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScSpriteTurn : MonoBehaviour
{
    private Transform playerTrans;
    private Transform myTrans;

    private float rotation;

    void Start()
    {
        playerTrans = ScMovement.Instance.transform;
        myTrans = transform;
    }

    void Update()
    {
        FacePlayer();
    }

    private void FacePlayer()
    {
        rotation = Vector3.Angle(Vector3.forward, (playerTrans.position - myTrans.position));
        if (playerTrans.position.x < myTrans.position.x)
            rotation = 360f - rotation;

        myTrans.rotation = Quaternion.Euler(0,rotation,0);
    }
}
