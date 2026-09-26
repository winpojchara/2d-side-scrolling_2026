using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private int maxSlots = 4;
    [SerializeField] private List<InventorySlot> items = new List<InventorySlot>(4);
    [SerializeField] private InventorySlot selectedSlot;
    Character character;

    public List<InventorySlot> GetItems()
    {
        return items;
    }
    

    void Awake()
    {
        character = GetComponent<Character>();
    }

    public void AddItem(SO_Item item)
    {
        if (items.Find(slot => slot.itemData == item && slot.currentStack < item.maxStackSize) != null)
        {
            InventorySlot existingSlot = items.Find(slot => slot.itemData == item && slot.currentStack < item.maxStackSize);
            existingSlot.currentStack++;
            Debug.Log($"Added {item.itemName} to existing stack. Current stack: {existingSlot.currentStack}");
            return;
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemData == null)
            {
                items[i] = new InventorySlot(item);
                Debug.Log($"Added {item.itemName} to inventory.");
                return;
            }
        }

        Debug.Log("Inventory is full!");
    }

    public void RemoveItem(InventorySlot item)
    {
        item.currentStack--;
        if (item.currentStack <= 0)
        {
            items.Remove(item);
        }
    }

    public void UseItem(InventorySlot item)
    {
        item.itemData.OnUse(character);
    }

    public void Swap(int a, int b)
    {
        if (a < 0 || a >= items.Count || b < 0 || b >= items.Count)
        {
            Debug.Log("Invalid slot indices for swapping.");
            return;
        }

        InventorySlot temp = items[a];
        items[a] = items[b];
        items[b] = temp;
    }
}
