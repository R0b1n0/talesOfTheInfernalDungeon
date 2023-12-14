using System;
using UnityEngine;
using CharactherStats;

public class ScTeam : MonoBehaviour
{
    public ScCharacter[] team = new ScCharacter[] { new ScCharacter(), new ScCharacter(), new ScCharacter() };
    
}

[Serializable]
public class ScInfoCharacter 
{
    public Sprite text;
    public int id; 
    public ScCharactereStats strengh;
    public ScCharactereStats health;
}
