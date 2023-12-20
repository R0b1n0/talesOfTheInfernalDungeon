using UnityEngine;
using UnityEngine.UI;

public class ScItemToolTips : MonoBehaviour
{
    [SerializeField] Text ItemNameText;
    [SerializeField] Text ItemTypeText;
    [SerializeField] Text ItemDescriptionText;
    [SerializeField] GameObject toolTipsItem;


    public void ShowToolTip(ScItem item)
    {
        ItemNameText.text = item.name;
        ItemTypeText.text = item.GetItemType();
        ItemDescriptionText.text = item.GetDescription();

        toolTipsItem.SetActive(true);


    }



    public void HideToolTips()
    {
        toolTipsItem.SetActive(false);
    }

}
