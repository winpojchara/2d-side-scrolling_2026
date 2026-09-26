
[System.Serializable]
public class InventorySlot
{
    public SO_Item itemData;
    public int currentStack;

    public InventorySlot(SO_Item itemData, int currentStack = 1)
    {
        this.itemData = itemData;
        this.currentStack = currentStack;
    }

}
