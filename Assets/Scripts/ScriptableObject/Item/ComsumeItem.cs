using UnityEngine;

[CreateAssetMenu(fileName = "New Comsume Item", menuName = "Item/Comsume Item")]
public class ComsumeItem : SO_Item
{
    [SerializeField] private ComsumeItemType comsumeItemType;
    [SerializeField] private int value;
    
}

public enum ComsumeItemType
{
    HealthPotion,
    AttackPotion
}
