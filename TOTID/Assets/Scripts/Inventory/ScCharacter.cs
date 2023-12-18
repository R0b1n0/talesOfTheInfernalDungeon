using UnityEngine;
using CharactherStats;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScCharacter : MonoBehaviour
{
    [SerializeField] List<ScCharacterData> characterData = new List<ScCharacterData>();
    public ScCharactereStats a_p;

    [SerializeField] Slider healthSlider;
    [SerializeField] ScHealthBar healthBar;
    
    public int currentHealth;

    [SerializeField] ScInventory inventory;
    [SerializeField] ScEquipmentPanel equipmentPanel;
    [SerializeField] ScStatsPanel statsPanel;
    [SerializeField] ScItemToolTips itemToolTips;
    [SerializeField] Image draggableItem;


    private ScItemSlot draggedSLot;
    private int characterIndex;


    private void Start()
    {
        characterIndex = 0;
        currentHealth = (int)characterData[characterIndex].health.Value;
        healthBar.SetMaxHealth((int)characterData[characterIndex].health.Value);
        healthSlider.maxValue = (int)characterData[characterIndex].health.Value;
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
        statsPanel.SetStats(characterData[characterIndex].strength, characterData[characterIndex].health, a_p);
        statsPanel.UpdateStatsValue();

        // Setup Events:
        // Right Click
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += UnequipItemSlot;
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

    private void UnequipItemSlot(ScItemSlot itemSlot)
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
                if (dragItem != null) dragItem.Unequip(characterData[characterIndex]);
                if (dropItem != null) dropItem.Equip(characterData[characterIndex]);
            }

            if (dropItemSlot is ScEquipmentSlot)
            {
                if (dragItem != null) dragItem.Equip(characterData[characterIndex]);
                if (dropItem != null) dropItem.Unequip(characterData[characterIndex]);
            }
            statsPanel.UpdateStatsValue();

            ScItem draggedItem = draggedSLot.Item;
            draggedSLot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
            characterData[characterIndex].item.sprite = equipmentPanel.equipmentSlots[0].image.sprite;
            characterData[characterIndex].weapon.sprite = equipmentPanel.equipmentSlots[1].image.sprite;

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
                    previousItem.Unequip(characterData[characterIndex]);
                    statsPanel.UpdateStatsValue();
                }
                item.Equip(characterData[characterIndex]);
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
            item.Unequip(characterData[characterIndex]);
            statsPanel.UpdateStatsValue();
            inventory.AddItem(item);
            

        }
    }
}
