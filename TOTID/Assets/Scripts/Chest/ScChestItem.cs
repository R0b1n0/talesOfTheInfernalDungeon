using UnityEngine;

public class ScChest : MonoBehaviour
{
    [SerializeField] ScItem item;
    [SerializeField] ScInventory inventory;
    [SerializeField] KeyCode itemPickUp = KeyCode.E;

    private bool isRange;
    private bool isEmpty;



    private void Update()
    {
        if (isRange && Input.GetKeyDown(itemPickUp))
        {
            if (!isEmpty)
            {
                inventory.AddItem(item);
                isEmpty = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        isRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isRange = false;
    }
}
