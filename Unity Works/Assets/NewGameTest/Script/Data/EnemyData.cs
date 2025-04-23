using UnityEngine;

[CreateAssetMenu(fileName ="Enemy",menuName ="Enemy/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int enemyDamage;
    public int enemyHealth;
    public int enemyArmor;
}
