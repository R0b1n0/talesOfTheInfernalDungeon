using UnityEngine;
using CharactherStats;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ScCharacter : MonoBehaviour
{

    public ScCharactereStats strength;
    public ScCharactereStats health;
    public ScCharactereStats a_p;

    [SerializeField] Slider healthSlider;
    [SerializeField] ScHealthBar healthBar;

    public int currentHealth;

    [SerializeField] ScInventory inventory;
    [SerializeField] ScEquipmentPanel equipmentPanel;
    [SerializeField] ScStatsPanel statsPanel;
    [SerializeField] ScItemToolTips itemToolTips;
    [SerializeField] Image draggableItem;

    [SerializeField] Image imageItemSlots;
    [SerializeField] Image imageWeaponSlots;

    private ScItemSlot draggedSLot;


    private void Start()
    {
        currentHealth = (int)health.Value;
        healthBar.SetMaxHealth((int)health.Value);
        healthSlider.maxValue = (int)health.Value;
        healthSlider.value = currentHealth;
    }
    private void OnValidate()
    {
        if (itemToolTips == null)
            itemToolTips = FindObjectOfType<ScItemToolTips>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            takeDamage(10);
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        healthSlider.value = currentHealth;
    }



    private void Awake()
    {
        statsPanel.SetStats(strength, health, a_p);
        statsPanel.UpdateStatsValue();

        // Setup Events:
        // Right Click
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;
        // Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        // Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        // End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        // Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
    }

    private void Equip(ScItemSlot itemSlot)
    {
        ScEquipableItem equipableItem = itemSlot.Item as ScEquipableItem;
        if ((equipableItem != null))
        {
            Equip(equipableItem);
        }
    }

    private void Unequip(ScItemSlot itemSlot)
    {
        ScEquipableItem equipableItem = itemSlot.Item as ScEquipableItem;
        if ((equipableItem != null))
        {
            Unequip(equipableItem);
        }
    }

    private void ShowTooltip(ScItemSlot itemSlot)
    {
        ScEquipableItem equipableItem = itemSlot.Item as ScEquipableItem;
        if ((equipableItem != null))
        {
            itemToolTips.ShowToolTip(equipableItem);
        }
    }
    private void HideTooltip(ScItemSlot itemSlot)
    {
        itemToolTips.HideToolTips();
    }

    private void BeginDrag(ScItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            draggedSLot = itemSlot;
            draggableItem.sprite = itemSlot.Item.icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.gameObject.SetActive(true);
        }
    }
    private void EndDrag(ScItemSlot itemSlot)
    {
        draggedSLot = null;
        draggableItem.gameObject.SetActive(false);
    }
    private void Drag(ScItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }
    private void Drop(ScItemSlot dropItemSlot)
    {
        if (dropItemSlot.CanReceiveItem(draggedSLot.Item) && draggedSLot.CanReceiveItem(dropItemSlot.Item))
        {
            ScEquipableItem dragItem = draggedSLot.Item as ScEquipableItem;
            ScEquipableItem dropItem = dropItemSlot.Item as ScEquipableItem;

            if (draggedSLot is ScEquipmentSlot)
            {
                if (dragItem != null) dragItem.Unequip(this);
                if (dropItem != null) dropItem.Equip(this);
            }

            if (dropItemSlot is ScEquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(this);
                if (dropItem != null) dropItem.Unequip(this);
            }
            statsPanel.UpdateStatsValue();

            ScItem draggedItem = draggedSLot.Item;
            draggedSLot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
            imageItemSlots.sprite = equipmentPanel.equipmentSlots[0].image.sprite;
            imageWeaponSlots.sprite = equipmentPanel.equipmentSlots[1].image.sprite;

        }
    }

    public void Equip(ScEquipableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            ScEquipableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statsPanel.UpdateStatsValue();
                }
                item.Equip(this);
                statsPanel.UpdateStatsValue();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(ScEquipableItem item)
    {
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statsPanel.UpdateStatsValue();
            inventory.AddItem(item);
        }
    }
}
