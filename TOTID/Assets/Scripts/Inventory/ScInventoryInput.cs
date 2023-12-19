using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScInventoryInput : MonoBehaviour
{
    [SerializeField] GameObject characterStatGameObject;
    [SerializeField] GameObject characterDisplayStats;


    [SerializeField] GameObject inventoryGameObject;

    [SerializeField] GameObject tooltipsItem;
    [SerializeField] GameObject tooltipsStats;
    [SerializeField] GameObject background;
    [SerializeField] KeyCode[] toggleInventoryKeys;


    void Update()
    {
        for (int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);

                tooltipsItem.SetActive(tooltipsItem.activeSelf);
                tooltipsStats.SetActive(tooltipsStats.activeSelf);
                background.SetActive(!background.activeSelf);
                #region Stats Display
                characterDisplayStats.SetActive(!characterDisplayStats.activeSelf);
                characterStatGameObject.SetActive(!characterStatGameObject.activeSelf);
                #endregion

                if (inventoryGameObject.activeSelf)
                {
                    ShowMouseCursor();
                }
                else
                {
                    HideMouseCursor();
                }

                break;
            }
        }
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}