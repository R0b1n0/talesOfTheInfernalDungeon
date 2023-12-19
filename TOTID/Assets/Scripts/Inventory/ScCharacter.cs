using UnityEngine;
using CharactherStats;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ScCharacter : MonoBehaviour
{
    [SerializeField] List<ScCharacterData> characterData = new List<ScCharacterData>();
    [Space]
    public ScCharactereStats a_p;

    [SerializeField] List<ScEquipmentPanel> characterEquipements = new List<ScEquipmentPanel>();
    [Space]
    [SerializeField] ScInventory inventory;
    [SerializeField] ScStatsPanel statsPanel;
    [SerializeField] ScItemToolTips itemToolTips;
    [SerializeField] Image draggableItem;

    [Space]
    [SerializeField] Image faceShoot;

    [Space]
    public TextMeshProUGUI playerName;


    private ScItemSlot draggedSLot;
    private int characterIndex;

    private void Awake()
    {
        UpdateUiInfo();
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
        characterData[characterIndex].TakeDamage(damage);
    }



    public void NextPlayer(int indexValue)
    {
        characterIndex = indexValue;
        UpdateUiInfo();
    }


    private void UpdateUiInfo()
    {
        faceShoot.sprite = characterData[characterIndex].faceShoot.sprite;

        playerName.text = characterData[characterIndex].playerName;

        statsPanel.SetStats(characterData[characterIndex].strength, characterData[characterIndex].health, a_p);
        statsPanel.UpdateStatsValue();

        // Setup Events:
        // Right Click
        inventory.OnRightClickEvent += Equip;
        characterEquipements[characterIndex].OnRightClickEvent += UnequipItemSlot;
        // Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        characterEquipements[characterIndex].OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        characterEquipements[characterIndex].OnPointerExitEvent += HideTooltip;
        // Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        characterEquipements[characterIndex].OnBeginDragEvent += BeginDrag;
        // End Drag
        inventory.OnEndDragEvent += EndDrag;
        characterEquipements[characterIndex].OnEndDragEvent += EndDrag;
        // Drag
        inventory.OnDragEvent += Drag;
        characterEquipements[characterIndex].OnDragEvent += Drag;
        // Drop
        inventory.OnDropEvent += Drop;
        characterEquipements[characterIndex].OnDropEvent += Drop;
    }

    #region equip unequip
    private void Equip(ScItemSlot itemSlot)
    {
        ScEquipableItem equipableItem = itemSlot.Item as ScEquipableItem;
        if ((equipableItem != null))
        {
            Equip(equipableItem);
            characterData[characterIndex].SetItem(characterEquipements[characterIndex].equipmentSlots[0].image.sprite);
            characterData[characterIndex].SetWeapon(characterEquipements[characterIndex].equipmentSlots[1].image.sprite);
        }
    }

    private void UnequipItemSlot(ScItemSlot itemSlot)
    {
        ScEquipableItem equipableItem = itemSlot.Item as ScEquipableItem;
        if ((equipableItem != null))
        {
            Unequip(equipableItem);
            characterData[characterIndex].SetItem(characterEquipements[characterIndex].equipmentSlots[0].image.sprite);
            characterData[characterIndex].SetWeapon(characterEquipements[characterIndex].equipmentSlots[1].image.sprite);
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
            characterData[characterIndex].SetItem(characterEquipements[characterIndex].equipmentSlots[0].image.sprite);
            characterData[characterIndex].SetWeapon(characterEquipements[characterIndex].equipmentSlots[1].image.sprite);
        }
    }

    public void Equip(ScEquipableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            ScEquipableItem previousItem;
            if (characterEquipements[characterIndex].AddItem(item, out previousItem))
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
        if (!inventory.IsFull() && characterEquipements[characterIndex].RemoveItem(item))
        {
            item.Unequip(characterData[characterIndex]);
            statsPanel.UpdateStatsValue();
            inventory.AddItem(item);
            
        }
    }

    #endregion
}
