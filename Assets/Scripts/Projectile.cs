using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float attackPower;
    [SerializeField] public float scaleDiffer;
    [SerializeField] public Rigidbody2D rb;
    string EnemyTag = "Enemy";

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(EnemyTag))
        {
            IDamageable iDamageable = other.GetComponent<IDamageable>();
            if(iDamageable != null)
            {
                iDamageable.TakeDamage(GetRNGDamage());

                Destroy(gameObject);
            }
        }
    }

    float GetRNGDamage()
    {
        float attack = attackPower;
        float maxScale = scaleDiffer;

        float randomScale = Random.Range(0,maxScale);
        float subAttack = attackPower*randomScale;

        float totalDamage = attack + subAttack;
        return totalDamage;
    }
}