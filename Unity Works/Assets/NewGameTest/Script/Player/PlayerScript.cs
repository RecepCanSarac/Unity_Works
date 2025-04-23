using UnityEngine;

public class PlayerScript : PlayerEvents
{
    public EnemyScript TargetEnemy;
    private GameTourSystemManager gameTourManager; 

    private void Start()
    {
        PlayerRestoreDefault();
        gameTourManager = FindFirstObjectByType<GameTourSystemManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(TargetEnemy);
            gameTourManager.SetGameTour(TourType.EnemyTour);
        }
    }
}