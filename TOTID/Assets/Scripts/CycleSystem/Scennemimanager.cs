using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scennemimanager : MonoBehaviour
{
    public int ennemiHealth;
    public int ennemiDamage;
    public int ennemimeRange;
    public int ennemiSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void death()
    {
        Destroy(gameObject);
    }
}
