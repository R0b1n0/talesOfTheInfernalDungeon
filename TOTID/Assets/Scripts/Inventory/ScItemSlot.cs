using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;


public class ScItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    public event Action<ScItem> OnRightClickEvent;
    private ScItem _item;
    public ScItem Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _item.icon;
                image.enabled = true;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null && OnRightClickEvent != null)
                OnRightClickEvent(Item);
        }
    }
    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
