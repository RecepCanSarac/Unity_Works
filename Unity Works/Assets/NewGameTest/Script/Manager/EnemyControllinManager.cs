using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllinManager : MonoBehaviour
{
    public List<EnemyScript> enemyList = new List<EnemyScript>();
    private GameTourSystemManager gameTourSystemManager;
    private PlayerScript player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerScript>();
        gameTourSystemManager = FindAnyObjectByType<GameTourSystemManager>();
    }

    public IEnumerator EnemiesAttack()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].TakeDamage(player);
            yield return new WaitForSeconds(1f);
        }
        gameTourSystemManager.SetGameTour(TourType.YourTour);

        yield return null;
    }

}
