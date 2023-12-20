using UnityEngine;

[CreateAssetMenu(menuName = "Item/UsableItem/Effect Heal")]
public class ScHealItemEffect : ScUsableItemEffect
{
    public int healthAmmount;
    public override void ExecuteEffect(ScUsableItem parentItem, ScCharacter character)
    {
        character.characterData[character.characterIndex].health.valueSc += healthAmmount;
    }

    public override string GetDescription()
    {
        return "Heals for " + healthAmmount + "health.";
    }
}
