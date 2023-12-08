using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScPlayerStats : MonoBehaviour
{
    [SerializeField] List<classStats> team = new List<classStats>();
    void Start()
    {
        
    }



    void Update()
    {
        
    }
}


[Serializable]
public struct classStats
{
    [SerializeField] private int damage;
    [SerializeField] private int hp;
    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;

}