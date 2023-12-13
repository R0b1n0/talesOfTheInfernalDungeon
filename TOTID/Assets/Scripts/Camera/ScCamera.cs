using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraHolder;

    private Transform myTrans;
    void Start()
    {
        myTrans = transform;
    }


    void Update()
    {
        myTrans.position = cameraHolder.position;
        myTrans.rotation = cameraHolder.rotation;
    }
}
