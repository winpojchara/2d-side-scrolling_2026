using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class SO_Item : ScriptableObject
{
    public string itemName;
    public int itemID;
    public Sprite itemIcon;
    public int maxStackSize = 1;
    private bool canUse = true;

    public virtual void OnUse(Character character)
    {
        if (canUse)
        {
            Debug.Log($"Using item: {itemName}");
        }
    }
}


