using UnityEngine;

public class PlayerScript : PlayerEvents
{
    public EnemyScript TargetEnemy;

    private void Start()
    {
        PlayerRestoreDefault();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(TargetEnemy);
        }
    }
}