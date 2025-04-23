using UnityEngine;

public class EnemyScript : EnemyEvents
{
    public PlayerScript player;

    private void Start()
    {
        EnemyRestoreDefault();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(player);
        }
    }
}
