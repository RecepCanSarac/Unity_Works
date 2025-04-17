using UnityEngine;

[CreateAssetMenu(fileName ="Player",menuName ="Player/Player")]
public class PlayerData : ScriptableObject
{
    [Header("Player-Name")]
    public string playerName;

    [Header("Player-Stats")]
    public int playerHealth;
    public int playerDamage;
    public int playerArmor;
}
