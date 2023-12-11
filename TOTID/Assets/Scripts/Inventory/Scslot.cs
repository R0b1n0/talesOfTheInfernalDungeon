using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Scslot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private int index;
    private Vector3 initialImageLocalPosition;
    
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private Image itemImage;

    private ScInventoryDisplay InventoryDisplay;

    private Button button;
    
    public void Initialize(ScInventoryDisplay _inventoryDisplay, int _index)
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);

        index = _index;
        InventoryDisplay = _inventoryDisplay;
        initialImageLocalPosition = itemImage.transform.position;

    }
    private void OnClick()
    {
        InventoryDisplay.ClickSlot(index);
    }

    public void UpdateDisplay(ScItem item)
    {
        if(!item.Empty)
        {
            itemCountText.text = item.Count.ToString();
            itemImage.sprite = item.Data.icon;
            itemImage.color = Color.white;
            return;
        }

        itemCountText.text = "";
        itemImage.sprite = null;
        itemImage.color = new Color(0,0,0,0);
    } 

    #region Drag And Drop
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        InventoryDisplay.DragSlots(index);

        initialImageLocalPosition = itemImage.transform.localPosition;
        itemImage.transform.SetParent(InventoryDisplay.transform);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        itemImage.transform.position = eventData.position;
    }


    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        itemImage.transform.SetParent(transform);
        itemImage.transform.localPosition = initialImageLocalPosition;
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        InventoryDisplay.DropOnSlots(index);
    }

    #endregion
}
