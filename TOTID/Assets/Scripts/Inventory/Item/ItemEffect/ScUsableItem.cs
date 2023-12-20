using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = ("Item/UsableItem/Item Usable Effetcs"))]
public class ScUsableItem : ScItem
{
    public bool IsConsumable;

    [Space]
    public List<ScUsableItemEffect> effects;

    public virtual void Use(ScCharacter character)
    {
        foreach (ScUsableItemEffect effect in effects)
        {
            effect.ExecuteEffect(this, character);
        }
    }

    public override string GetItemType()
    {
        return IsConsumable ? "Comsumable" : "Usable";
    }

    public override string GetDescription()
    {
        sb.Length = 0;

        foreach(ScUsableItemEffect effect in effects)
        {
            sb.AppendLine(effect.GetDescription());
        }
        return sb.ToString();
    }
}
