using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/UsableItem/Effect Strenght")]
public class ScStrenghtPotion : ScUsableItemEffect
{
    public int strenghtAmmount;
    public override void ExecuteEffect(ScUsableItem parentItem, ScCharacter character)
    {
        character.characterData[character.characterIndex].strength.valueSc += strenghtAmmount;
    }

    public override string GetDescription()
    {
        return "Augment Strenght for " + strenghtAmmount + ".";
    }
}
