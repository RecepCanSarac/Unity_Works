using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public PlayerData PlayerData;

    [SerializeField] private string playerName;
    [SerializeField] private int playerDamage;
    [SerializeField] private int playerHealth;
    [SerializeField] private int playerArmor;


    public void PlayerRestoreDefault()
    {
        playerName = PlayerData.playerName;
        playerDamage = PlayerData.playerDamage;
        playerHealth = PlayerData.playerHealth;
        playerArmor = PlayerData.playerArmor;
    }

    public void TakenDamage(int damage)
    {
        if (playerArmor > 0)
        {
            int damageToArmor = Mathf.Min(playerArmor, damage);
            playerArmor -= damageToArmor;
            damage -= damageToArmor;
        }
        playerHealth -= damage;
    }

    public void TakeDamage(EnemyScript enemy)
    {
        enemy.TakenDamage(playerDamage);
    }
}
