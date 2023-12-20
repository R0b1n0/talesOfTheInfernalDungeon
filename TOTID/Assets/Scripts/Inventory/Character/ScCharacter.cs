using UnityEngine;
using CharactherStats;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ScCharacter : MonoBehaviour
{
    public static ScCharacter Instance;

    [SerializeField] public List<ScCharacterData> characterData = new List<ScCharacterData>();
    [Space]


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
    public int characterIndex;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        UpdateUiInfo();
    }

    private void OnValidate()
    {
        if (itemToolTips == null)
            itemToolTips = FindObjectOfType<ScItemToolTips>();
    }

    private void Update()
    {
        statsPanel.UpdateStatsValue();
        if (Input.GetKeyDown(KeyCode.K))
            takeDamage(10);
    }

    public void takeDamage(int damage)
    {
        statsPanel.UpdateStatsValue();
        characterData[characterIndex].TakeDamage(damage);
    }



    public void NextPlayer(int indexValue)
    {
        characterIndex = indexValue;
        UpdateUiInfo();
        for(int i = 0; i < characterData.Count; i++)
        {
            if (i == characterIndex)
            {
                characterData[i].SetHighLight(true);
            }
            else
            {
                characterData[i].SetHighLight(false);
            }
        }
    }


    private void UpdateUiInfo()
    {
        characterData[characterIndex].UpdateTextValue();
        faceShoot.sprite = characterData[characterIndex].faceShoot.sprite;

        playerName.text = characterData[characterIndex].playerName;

        statsPanel.SetStats(characterData[characterIndex].strength, characterData[characterIndex].health);
        statsPanel.UpdateStatsValue();

        // Setup Events:
        // Right Click
        inventory.OnRightClickEvent += InventoryRightClick;
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
    private void InventoryRightClick(ScItemSlot itemSlot)
    {
        if (itemSlot.Item is ScEquipableItem)
        {
            characterData[characterIndex].UpdateTextValue();
            Equip((ScEquipableItem)itemSlot.Item);
            characterData[characterIndex].SetItem(characterEquipements[characterIndex].equipmentSlots[0].image.sprite);
            characterData[characterIndex].SetWeapon(characterEquipements[characterIndex].equipmentSlots[1].image.sprite);
        }
        else if (itemSlot.Item is ScUsableItem)
        {
            ScUsableItem usableItem = (ScUsableItem)itemSlot.Item;
            usableItem.Use(this);

            if(usableItem.IsConsumable)
            {
                characterData[characterIndex].UpdateTextValue();
                statsPanel.UpdateStatsValue();
                inventory.RemoveItem(usableItem);
                Destroy(usableItem);
            }
        }
    }

    private void UnequipItemSlot(ScItemSlot itemSlot)
    {
        ScEquipableItem equipableItem = itemSlot.Item as ScEquipableItem;
        if ((equipableItem != null))
        {
            characterData[characterIndex].UpdateTextValue();
            statsPanel.UpdateStatsValue();
            Unequip(equipableItem);
            characterData[characterIndex].SetItem(characterEquipements[characterIndex].equipmentSlots[0].image.sprite);
            characterData[characterIndex].SetWeapon(characterEquipements[characterIndex].equipmentSlots[1].image.sprite);
        }
    }

    private void ShowTooltip(ScItemSlot itemSlot)
    {
        ScEquipableItem equipableItem = itemSlot.Item as ScEquipableItem;
        if ((itemSlot.Item != null))
        {
            itemToolTips.ShowToolTip(itemSlot.Item);
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
