using CharactherStats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScCharacterData : MonoBehaviour
{


    public ScCharactereStats strength = new ScCharactereStats();
    public ScCharactereStats health = new ScCharactereStats();

    [Space]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider healthSlider;
    [SerializeField] ScHealthBar healthSliderScript;
    [SerializeField] public Image item;
    [SerializeField] public Image weapon;
    [SerializeField] ScStatsPanel statsPanel;

    [Space]
    public Image faceShoot;
    [Space]
    public string playerName;

    private Color colorDisable = Color.clear;
    private Color colorEnable = Color.white;

    private void Start()
    {
        statsPanel.SetStats(strength,health);
        healthText.text = health.valueSc.ToString();
        healthSliderScript.SetMaxHealth((int)health.baseValue);
        item.color = colorDisable;
        weapon.color = colorDisable;


    }

    public void TakeDamage(int damageValue)
    {
        health.valueSc -= damageValue;
        healthText.text = health.valueSc.ToString();
        healthSlider.value = health.valueSc;
        healthSliderScript.SetHealth((int)health.valueSc);
    }
    public void SetWeapon(Sprite sprite)
    {
        weapon.sprite = sprite;
        if (weapon.sprite == null)
            weapon.color = colorDisable;
        else
            weapon.color = colorEnable;
    }

    public void SetItem(Sprite sprite)
    {
        item.sprite = sprite;
        if (item.sprite == null)
            item.color = colorDisable;
        else
            item.color = colorEnable;
    }



}
