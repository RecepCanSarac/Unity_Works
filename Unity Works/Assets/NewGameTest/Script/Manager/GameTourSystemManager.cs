using UnityEngine;

public enum TourType
{
    playerTour,
    enemyTour
}

public class GameTourSystemManager : MonoBehaviour
{
    public TourType tourType;
    public void SetGameTour(TourType type)
    {
        tourType = type;
    }
}
