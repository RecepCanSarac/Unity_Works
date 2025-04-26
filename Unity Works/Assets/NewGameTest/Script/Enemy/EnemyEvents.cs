using UnityEngine;

public class EnemyEvents : MonoBehaviour
{
    public EnemyData EnemyData;
    [SerializeField] private string EnemyName;
    [SerializeField] private int enemyDamage;
    [SerializeField] private int enemyHealth;
    [SerializeField] private int enemyArmor;


    public void EnemyRestoreDefault()
    {
        this.EnemyName = EnemyData.enemyName;
        this.enemyDamage = EnemyData.enemyDamage;
        this.enemyHealth = EnemyData.enemyHealth;
        this.enemyArmor = EnemyData.enemyArmor;
    }


    public void TakenDamage(int damage)
    {
        if (enemyArmor > 0)
        {
            int damageToArmor = Mathf.Min(enemyArmor, damage);
            enemyArmor -= damageToArmor;
            damage -= damageToArmor;
        }
        enemyHealth -= damage;

        if (enemyHealth <= 0)
            Destroy(gameObject);
    }

    public void TakeDamage(PlayerScript player)
    {
        player.TakenDamage(enemyDamage);
    }
}
