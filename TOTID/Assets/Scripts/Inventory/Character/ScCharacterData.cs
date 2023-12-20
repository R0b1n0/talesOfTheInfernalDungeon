using CharactherStats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScCharacterData : MonoBehaviour
{
    #region Variable

    public ScCharactereStats strength = new ScCharactereStats();
    public ScCharactereStats health = new ScCharactereStats();

    [SerializeField] Image highLight;

    #region Text, Slider, HealthBarScirpt, Image(Item and Weapon), StatsPanelScript,
    [Space]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider healthSlider;
    [SerializeField] ScHealthBar healthSliderScript;
    [SerializeField] public Image item;
    [SerializeField] public Image weapon;
    [SerializeField] ScStatsPanel statsPanel;
    #endregion


    [Space]
    public Image faceShoot;
    [Space]
    public string playerName;

    private Color colorDisable = Color.clear;
    private Color colorEnable = Color.white;

    #endregion


    private void Start()
    {
        statsPanel.SetStats(strength,health);
        healthText.text = health.Value.ToString();
        healthSliderScript.SetMaxHealth((int)health.baseValue);
        item.color = colorDisable;
        weapon.color = colorDisable;
    }


    public void UpdateTextValue()
    {
        UpdateHelthBarValue();
        healthText.text = health.Value.ToString();
        healthSliderScript.SetHealth((int)health.Value);
    }

    private void UpdateHelthBarValue()
    {
        if(health.Value > health.baseValue)
        {
            healthSliderScript.SetMaxHealth((int)health.Value);
        }
    }

    public void SetHighLight(bool IsHighLight)
    {
        highLight.enabled = IsHighLight;
    }

    #region Take Damage
    public void TakeDamage(int damageValue)
    {
        health.valueSc -= damageValue;
        healthText.text = health.Value.ToString();
        healthSlider.value = health.Value;
        healthSliderScript.SetHealth((int)health.Value);
    }
    #endregion

    #region Weapon And Item Sprite Set

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

    #endregion

}
