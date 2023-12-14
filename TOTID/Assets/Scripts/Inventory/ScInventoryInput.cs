using UnityEngine;

public class ScInventoryInput : MonoBehaviour
{

    [SerializeField] GameObject characterStatGameObject;
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] GameObject tooltipsItem;
    [SerializeField] GameObject tooltipsStats;
    [SerializeField] KeyCode[] toggleInventoryKeys;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < toggleInventoryKeys.Length; i++)
        {
            if (Input.GetKeyDown(toggleInventoryKeys[i]))
            {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
                characterStatGameObject.SetActive(!characterStatGameObject.activeSelf);
                tooltipsItem.SetActive(tooltipsItem.activeSelf);
                tooltipsStats.SetActive(tooltipsStats.activeSelf);

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
