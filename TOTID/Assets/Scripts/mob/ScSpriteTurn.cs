using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScSpriteTurn : MonoBehaviour
{
    private Transform playerTrans;
    private Transform myTrans;

    private Vector3 rotation;

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
        rotation.Set(playerTrans.position.x - myTrans.position.x, 0, playerTrans.position.z - myTrans.position.z);
        myTrans.forward = rotation;
    }
}
