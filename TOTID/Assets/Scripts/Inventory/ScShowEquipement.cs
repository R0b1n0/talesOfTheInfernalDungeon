using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScShowEquipement : MonoBehaviour
{
    [SerializeField] List<GameObject> equipementPanel = new List<GameObject>();
    [SerializeField] ScCharacter characterScript;
    private int currentEquipement;

    // only change the GO activation, needs to change the logic 

    private void Start()
    {
        currentEquipement = 0;
        ShowCurrentCharacter();
    }

    public void ShowCurrentCharacter()
    {
        equipementPanel[currentEquipement].SetActive(true);
    }

    public void NextCharacter()
    {
        equipementPanel[currentEquipement].SetActive(false);
        currentEquipement++;

        if (currentEquipement >= equipementPanel.Count) 
        {
            currentEquipement = 0;
        }
        characterScript.NextPlayer(currentEquipement);
        ShowCurrentCharacter();
    }

    public void PreviousCharacter()
    {
        equipementPanel[currentEquipement].SetActive(false);
        currentEquipement--;

        if (currentEquipement < 0) 
        {
            currentEquipement = 2;
        }
        characterScript.NextPlayer(currentEquipement);
        ShowCurrentCharacter();
    }


}
