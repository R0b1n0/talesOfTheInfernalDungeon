using UnityEngine;

public class ScItemCollectable : MonoBehaviour
{
    [SerializeField] ScItem item;
    [SerializeField] ScInventory inventory;

    private bool isColected;
    private bool isRange;

    private void Update()
    {
        if (isRange && !isColected)
        {
            if (inventory.IsFull())
            {
                Debug.Log("Inventory is Full");
            }
            else if(!isColected)
            {
                Destroy(this);
                inventory.AddItem(item);
                Debug.Log("Item Collected");

                isRange = false;
                isColected = true;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            isRange = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            isRange = false;
    }

}
