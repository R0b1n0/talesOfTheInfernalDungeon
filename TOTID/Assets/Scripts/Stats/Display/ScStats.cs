using UnityEngine;
using UnityEngine.UI;


public class ScStats : MonoBehaviour
{
    [Header("Health")] 
    public int maxHealth = 100;
    public int currentHealth;
    public int helathAugmentWarrior = 10;
    public Text healthText;

    [Header("Damage")]
    public int minDamageWarrior;
    public int minDamageRobber;
    public int currentDamageWarrior;
    public int damageUpgradeWarrior = 10;
    public Text damageText;
    public Text damageTextAfterUpgrade;

    [Header("Points de Stats")]
    public int statsPoint = 1;
    public int currentStatsPoint;
    public Text statsPointText;

    [Header("Incrementation Stats")]
    public GameObject buttonStatsAugment;
    private Button btn;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        minDamageWarrior = 10;
        minDamageRobber = 5;
        currentDamageWarrior = minDamageWarrior;
    
    }

    // Update is called once per frame
    void Update()
    {
        upgradeStats();
        displayStats();
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentStatsPoint += statsPoint;
        }
    }


    private void displayStats()
    {
        //Health stats display
        healthText.text = "HEALTH : " + currentHealth;
        //Damage stats display
        damageText.text = "DAMAGE : " + currentDamageWarrior;
        //StatsPoint display
        statsPointText.text = "Stats Point : " + currentStatsPoint;
    }

    public void upgradeStats()
    {
        if(currentStatsPoint > 0)
        {
            buttonStatsAugment.SetActive(true);
        }
        else
        {
            buttonStatsAugment.SetActive(false);
        }
    }

    public void statsAugment()
    {
        Debug.Log("appuyez");
        currentDamageWarrior += damageUpgradeWarrior;
        currentStatsPoint -= statsPoint;
    }
}
