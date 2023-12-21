using UnityEngine;

[CreateAssetMenu(menuName = "Item/UsableItem/Effect Stranght")]
public class ScDamageBoost : ScUsableItemEffect
{
    public int damageAmmount;
    public override void ExecuteEffect(ScUsableItem parentItem, ScCharacter character)
    {
        character.characterData[character.characterIndex].strength.valueSc += damageAmmount;
    }

    public override string GetDescription()
    {
        return "Increase strength for " + damageAmmount + "strength.";
    }
}
