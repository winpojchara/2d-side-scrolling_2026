using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Attack/RangeAttack")]
public class RangeAttack : SO_Myattack
{
    public float attackCooldown = .3f;
    public float force = 50f;

    public override void OnAttack(Character character)
    {
        Transform attackPos = character.AttackPoint;
        Quaternion spawnRotation = (character.transform.localScale.x > 0) ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        GameObject attackProjectile = Instantiate(attackPrefab, attackPos.position, spawnRotation);

        character.SetAttackCooldown(attackCooldown);
        if(spendHealth)
        {
            character.TakeDamage(requireHp,false);
        }

        Projectile projectile = attackProjectile.GetComponent<Projectile>();
        if(projectile != null)
        {
            projectile.attackPower = attackPower;
            projectile.scaleDiffer = scaleDiffer;
            projectile.rb.AddForce(projectile.transform.right * force);
        }
        
    }
}
