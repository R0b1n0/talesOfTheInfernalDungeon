using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item inventory")]
public class ScItemData : ScriptableObject
{
    [SerializeField] public string itemName;

    [SerializeField] public int stackMaxCount;

    [SerializeField] public Sprite icon;

    [SerializeField] public string description;
    [SerializeField] public int id;
}