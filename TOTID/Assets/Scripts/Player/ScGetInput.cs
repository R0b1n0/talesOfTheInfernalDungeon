using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour
{
    private ScView viewScript;
    private ScAction actionScript;

    private void Start()
    {
        viewScript = GetComponent<ScView>();
        actionScript = GetComponent<ScAction>();
    }

    public void GetMouseValue(InputAction.CallbackContext ctxt)
    {
        viewScript.LookAround(ctxt.ReadValue<Vector2>());
    }

    public void LeftClickAction(InputAction.CallbackContext ctxt)
    {
        if (ctxt.canceled)
            actionScript.ActionOnRelease();
    }
}
