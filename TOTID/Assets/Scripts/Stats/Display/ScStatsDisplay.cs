using UnityEngine;
using UnityEngine.UI;


public class ScStatsDisplay : MonoBehaviour
{
    [Header("Health")]
    [Header("Warrior")]
    public int upgradedHealthWarrior;
    public int maxHealthWarrior = 100;
    public int currentHealthWarrior;
    public int healthAugmentWarrior = 10;
    public Text healthTextWarrior;
    public Text healthTextAfterUpgradeWarrior;
   /* [Header("Thief")]
    public int upgradedHealthThief;
    public int maxHealthThief = 50;
    public int currentHealthThief;
    public int healthAugmentThief = 5;
    public Text healthTextThief;*/



    [Header("Damage")]
    [Header("Warrior")]
    public int minDamageWarrior = 10;
    public int currentDamageWarrior;
    public int damageUpgradeWarrior = 5;
    public int damageAfterUpgradesWarrior;
    public Text damageTextWarrior;
    public Text damageTextAfterUpgradeWarrior;
    
    
    /*[Header("Thief")]
    public int minDamageThief = 15;
    public int currentDamageThief;
    public int damageUpgradeThief = 10;
    public int damageAfterUpgradesThief;
    public Text damageTextThief;
    public Text damageTextAfterUpgradeThief;*/


    [Header("Points de Stats")]
    public int statsPoint = 1;
    public int currentStatsPoint;
    public Text statsPointText;

    [Header("Stats Button")]
    public GameObject buttonStatsAugmentDamageWarrior;
    public GameObject buttonStatsAugmentHealthWarrior;
    public GameObject damageTextAfterUpgradesWarrior;
    public GameObject healthTextAfterUpgradesWarrior;
    /*  public GameObject buttonStatsAugmentDamageThief; 
      public GameObject buttonStatsAugmentHealthThief;
      public GameObject damageTextAfterUpgradesThief;*/

    [Header("class")]
    public GameObject warrior;
    public GameObject thief;
    public GameObject alchemiste;

    void Start()
    {
        currentHealthWarrior = maxHealthWarrior;
        currentDamageWarrior = minDamageWarrior;
    }

    // Update is called once per frame
    void Update()
    {
        //StatsPoint display
        statsPointText.text = "Stats Point : " + currentStatsPoint;
        buttonActivate();
        DisplayStatsWarrior();
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentStatsPoint += statsPoint;
        }

        if(currentStatsPoint > 0)
        {
            damageAfterUpgradesWarrior = currentDamageWarrior + damageUpgradeWarrior;
            damageTextAfterUpgradeWarrior.text = "" + damageAfterUpgradesWarrior;
            damageTextAfterUpgradesWarrior.SetActive(true);


            upgradedHealthWarrior = currentHealthWarrior + healthAugmentWarrior;
            healthTextAfterUpgradeWarrior.text = "" + upgradedHealthWarrior;
            healthTextAfterUpgradesWarrior.SetActive(true);
        }
        else
        {
            damageTextAfterUpgradesWarrior.SetActive(false);
            healthTextAfterUpgradesWarrior.SetActive(false);
        }
    }

    void buttonActivate()
    {
        if(currentStatsPoint > 0)
        {
            buttonStatsAugmentDamageWarrior.SetActive(true);
            buttonStatsAugmentHealthWarrior.SetActive(true);
        }
        else
        {
            buttonStatsAugmentDamageWarrior.SetActive(false);
            buttonStatsAugmentHealthWarrior.SetActive(false);
        }
    }


    public void DisplayStatsWarrior()
    {
        //Health stats display
        healthTextWarrior.text = "HEALTH : " + currentHealthWarrior;
        //Damage stats display
        damageTextWarrior.text = "DAMAGE : " + currentDamageWarrior;
    } 

    public void statsAugmentDamageWarrior()
    {
        Debug.Log("pressed");
        currentDamageWarrior += damageUpgradeWarrior;
        currentStatsPoint -= statsPoint;
    }

    public void statsAugmentHealthWarrior()
    {
        currentHealthWarrior += healthAugmentWarrior;
        currentStatsPoint -= statsPoint;
    }




    public void ChooseClassWarrior()
    {
        if (warrior)
        {
            warrior.SetActive(true);
            alchemiste.SetActive(false);
            thief.SetActive(false);
        }

    }
}
