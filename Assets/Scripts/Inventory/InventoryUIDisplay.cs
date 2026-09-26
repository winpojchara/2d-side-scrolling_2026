using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUIDisplay : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventorySlotUI[] slotUIs = new InventorySlotUI[4];

    public List<InventorySlot> GetInventorySlots()
    {
        return inventory.GetItems();
    }

    public void UpdateInventoryUI()
    {
        List<InventorySlot> inventorySlots = inventory.GetItems();

        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (i < inventorySlots.Count)
            {
                DisplayInventory(inventorySlots[i], slotUIs[i]);
            }
            else
            {
                DisplayInventory(null, slotUIs[i]);
            }
        }
    }

    private void DisplayInventory(InventorySlot inventorySlot, InventorySlotUI slotUI)
    {
        if (inventorySlot != null && inventorySlot.itemData != null)
        {
            slotUI.DisplayItem(inventorySlot.itemData, inventorySlot.currentStack, slotUI.isSelected);
        }
        else
        {
            slotUI.DisplayItem(null, 0, slotUI.isSelected);
        }
    }

}
[System.Serializable]
public class InventorySlotUI
{
    public Image itemIcon;
    public TextMeshProUGUI itemCountText;
    public bool isSelected;
    public GameObject slotBoarder;

    public void DisplayItem(SO_Item item, int count, bool selected)
    {
        if (item != null)
        {
            itemIcon.sprite = item.itemIcon;
            itemIcon.enabled = true;
            itemCountText.text = count > 1 ? count.ToString() : "";
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
            itemCountText.text = "";
        }

        if (selected)
        {
            slotBoarder.SetActive(true);
        }
        else
        {
            slotBoarder.SetActive(false);
        }
    }
}
