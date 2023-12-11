using UnityEngine;

[CreateAssetMenu(menuName = "Item Data")]
public class ScItemData : ScriptableObject
{
    [SerializeField] public string itemName;

    [SerializeField] public int stackMaxCount;
    
    [SerializeField] public Sprite icon; 
}