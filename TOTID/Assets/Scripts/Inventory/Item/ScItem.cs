using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = ("Item/Item/Item"))]
public class ScItem : ScriptableObject
{
    public string nameItem;
    public Sprite icon;

    protected static readonly StringBuilder sb = new StringBuilder();

    public virtual string GetItemType()
    {
        return "";
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
