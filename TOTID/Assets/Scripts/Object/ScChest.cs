using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScChest : ScInteractible {
    private Animator animator;
    [SerializeField] GameObject[] loot;


    private bool isChestOpen = false;

    public float power = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChestOpen() {
        if (!isChestOpen) {
            Debug.Log("Open");
            isChestOpen = true;
            animator.SetBool("ChestOpenin", true);
            for (int i = 0; i<loot.Length; i++) {
                GameObject newLoot = Instantiate(loot[i], transform.position + Vector3.up, Quaternion.identity);
                Rigidbody body = newLoot.GetComponent<Rigidbody>();
                if (body != null ) {
                    Vector3 dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 2f), Random.Range(-1f, 1f)).normalized;
                    body.AddForce(dir * power, ForceMode.Impulse);
                }
            }
        } else {
            return;
        }
    }

    public void ChestFullOpened(){
        animator.SetBool("ChestFullopen", true);
    }

    public override void Interact() {
        Debug.Log("Open1A");
        ChestOpen();
    }
}
