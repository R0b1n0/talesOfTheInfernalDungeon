using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScView : MonoBehaviour
{
    [SerializeField] private Transform cameraView;
    [SerializeField] private float xSensivity;
    [SerializeField] private float ySensivity;

    private float yRotation = 0;
    private float xRotation = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LookAround(Vector2 mouseMove)
    {
        yRotation += mouseMove.x * Time.deltaTime * xSensivity;
        xRotation -= mouseMove.y * Time.deltaTime * ySensivity;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cameraView.rotation = Quaternion.Euler(xRotation , yRotation , 0);
    }

}
