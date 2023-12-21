using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScChest : ScInteractible {
    private Animator animator;
    [SerializeField] GameObject[] loot;
    private bool isChestOpen = false;
    public float power = 5f;
    [SerializeField] Transform spawnPoint;

    private void Start(){
        animator = GetComponent<Animator>();
    }

    public void ChestOpen() {
        if (!isChestOpen) {
            isChestOpen = true;
            animator.SetBool("ChestOpenin", true);
        } else {
            return;
        }
    }

    public void ChestLooting (){
        for (int i = 0; i<loot.Length; i++) {
                GameObject newLoot = Instantiate(loot[i], spawnPoint.position, Quaternion.identity);
                Rigidbody body = newLoot.AddComponent<Rigidbody>();
            if (body != null) {
                Vector3 dir = new Vector3(0, Random.Range(0.1f, 0.5f), 0).normalized + spawnPoint.forward*Random.Range(0.5f,1f);
                print(dir);
                body.AddForce((dir * power), ForceMode.Impulse);
            }
        }
    }

    public void ChestFullOpened(){
        animator.SetBool("ChestFullopen", true);
    }

    public override void Interact() {
        ChestOpen();
    }
}
