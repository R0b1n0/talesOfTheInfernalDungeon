using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour
{
    private ScView viewScript;
    private ScAction actionScript;
    private PlayerInput playerInput;
    [SerializeField] ScInventoryInput uiScript;

    private void Start()
    {
        viewScript = GetComponent<ScView>();
        actionScript = GetComponent<ScAction>();
        playerInput = GetComponent<PlayerInput>();
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

    public void MenueGameSwitch(InputAction.CallbackContext ctxt)
    {
        if (ctxt.canceled) 
        {
            if (playerInput.currentActionMap.name == "InGame")
                playerInput.SwitchCurrentActionMap("InMenu");
            else
                playerInput.SwitchCurrentActionMap("InGame");

            uiScript.GameMenuSwitch();
        }
    }
}
