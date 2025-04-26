using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TourType
{
    YourTour,
    EnemyTour
}

public class GameTourSystemManager : MonoBehaviour
{
    public TourType tourType;

    public TextMeshProUGUI tourTxt;
    private EnemyControllinManager enemyControllinManager;

    public List<EnemyScript> enemyList = new List<EnemyScript>();
    private PlayerScript player;
    private void Start()
    {
        player = FindAnyObjectByType<PlayerScript>();
        enemyControllinManager = FindFirstObjectByType<EnemyControllinManager>();
        SetGameTour(TourType.YourTour);
    }

    
    public void SetGameTour(TourType type)
    {
        tourType = type;
        tourTxt.text = tourType.ToString();
        if(type == TourType.EnemyTour)
            StartCoroutine(EnemiesAttack());
    }


    public IEnumerator EnemiesAttack()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].TakeDamage(player);
            yield return new WaitForSeconds(1f);
        }
        SetGameTour(TourType.YourTour);
        yield return null;
    }
}
