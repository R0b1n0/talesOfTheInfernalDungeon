using UnityEngine;

public abstract class ScUsableItemEffect : ScriptableObject
{
    public abstract void ExecuteEffect(ScUsableItem parentItem, ScCharacter character);

    public abstract string GetDescription();
}
