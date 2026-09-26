using UnityEngine;

public class Entities : MonoBehaviour, IDamageable
{
    [SerializeField] protected Health health;
    [SerializeField] protected float attackPower;
    [SerializeField] protected State currentState;
    protected string PlayerTag = "Player";

    public virtual void TakeDamage(float damage, bool fatal = true)
    {
        if (health == null)
        {
            print("Health component is missing.");
            return;
        }

        health.ReduceHealth(damage);

        if (health.IsDead())
        {
            Death();
        }
    }

    public virtual void Death()
    {
        // Handle death logic here (e.g., play animation, disable the entity, etc.)
        print("Entity has died.");
    }
}

public enum State
{
    Idle,
    Attack
}
