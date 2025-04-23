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

    private void Start()
    {
        enemyControllinManager = FindFirstObjectByType<EnemyControllinManager>();
        SetGameTour(TourType.YourTour);
    }

    private void Update()
    {
        switch (tourType)
        {
            case TourType.YourTour:
                //Bir þeyler Aktif edilecek
                break;
            case TourType.EnemyTour:
                StartCoroutine(enemyControllinManager.EnemiesAttack());
                break;
        }
    }

    public void SetGameTour(TourType type)
    {
        tourType = type;
        tourTxt.text = tourType.ToString();
    }
}
