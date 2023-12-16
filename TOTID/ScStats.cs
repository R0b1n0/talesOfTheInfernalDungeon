
using UnityEngine;
using UnityEngine.UI;

public class ScStats : MonoBehaviour
{
     
    public int maxHealth = 100;
    public int currentHealth;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HEALTH : " + currentHealth;
    }
}
