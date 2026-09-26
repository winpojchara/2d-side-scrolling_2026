using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public bool isAlive = true;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetHealth();
    }

    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
    }

    void CheckIsdead()
    {
        if(IsDead() && isAlive)
        {
            isAlive = false;
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }
    
    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public void ReduceHealth(float amount)
    {
        currentHealth = Mathf.MoveTowards(currentHealth,0,amount);
        CheckIsdead();
    }

    public void RestoreHealth(float amount)
    {
        currentHealth = Mathf.MoveTowards(currentHealth,MaxHealth, amount);
    }
}
