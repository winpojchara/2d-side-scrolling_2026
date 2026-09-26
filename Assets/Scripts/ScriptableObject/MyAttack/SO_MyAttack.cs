using UnityEngine;

public class SO_Myattack : ScriptableObject
{
    public float attackPower;
    public float scaleDiffer;
    public GameObject attackPrefab;

    public bool spendHealth = false;
    public float requireHp = 5;

    public virtual void OnAttack(Character character)
    {
        
    }
}
