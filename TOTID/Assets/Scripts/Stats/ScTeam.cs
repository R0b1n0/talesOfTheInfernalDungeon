using System;
using UnityEngine;

public class ScTeam : MonoBehaviour
{
    public ScCharacter[] team = new ScCharacter[] { new ScCharacter(),  new ScCharacter() , new ScCharacter()};
    
}

[Serializable]
public class ScCharacter 
{
    public Sprite text;
    public int id; 
    public ScCharactereStats strengh;
    public ScCharactereStats health;
}
