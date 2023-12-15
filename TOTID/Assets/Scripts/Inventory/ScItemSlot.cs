using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;


public class ScItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public Image image;

    public event Action<ScItemSlot> OnPointerExitEvent;
    public event Action<ScItemSlot> OnPointerEnterEvent;
    public event Action<ScItemSlot> OnRightClickEvent;
    public event Action<ScItemSlot> OnBeginDragEvent;
    public event Action<ScItemSlot> OnEndDragEvent;
    public event Action<ScItemSlot> OnDragEvent;
    public event Action<ScItemSlot> OnDropEvent;

    private Color normalColor = Color.white;
    private Color disableColor = Color.clear;


    #region OnValidate
    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();

    }
    #endregion

    #region Item Set
    private ScItem _item;
    public ScItem Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
            {
                image.color = disableColor;
            }
            else
            {
                image.sprite = _item.icon;
                image.color = normalColor;
            }
        }
    }
    #endregion



    #region verifie si on clique sur l'item
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
                OnRightClickEvent(this);
        }
    }
    #endregion


    public virtual bool CanReceiveItem(ScItem item)
    {
        return true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
            OnPointerEnterEvent(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
            OnPointerExitEvent(this);
    }

    Vector2 originalPosition;

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
            OnDragEvent(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
            OnBeginDragEvent(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
            OnEndDragEvent(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
            OnDropEvent(this);
    }
}
