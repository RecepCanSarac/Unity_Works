using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public PlayerData PlayerData;

    [SerializeField] private string playerName;
    [SerializeField] private int playerDamage;
    [SerializeField] private int playerHealth;
    [SerializeField] private int playerArmor;


    public void PlayerRestoreDefoult()
    {
        playerName = PlayerData.playerName;
        playerDamage = PlayerData.playerDamage;
        playerHealth = PlayerData.playerHealth;
        playerArmor = PlayerData.playerArmor;
    }

    public int TakenDamage(int damage)
    {
        if (playerArmor > 0)
        {
            int damageToArmor = Mathf.Min(playerArmor, damage);
            playerArmor -= damageToArmor;
            damage -= damageToArmor;
        }
        playerHealth -= damage;
        return playerHealth;
    }

    //Enemy TakenDamage
    public int TakeDamage(int damage, int armor, int health)
    {
        if (armor > 0)
        {
            int damageToArmor = Mathf.Min(health, damage);
            armor -= damageToArmor;
            damage -= damageToArmor;
        }

        health -= damage;
        return health;
    }

}
