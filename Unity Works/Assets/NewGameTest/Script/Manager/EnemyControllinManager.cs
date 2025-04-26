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

   

}
