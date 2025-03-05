using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject parkourPrefab;
    public int parkourCount = 5;
    private Transform lastExitPoint;
    private Quaternion lastExitRotation;
    private GameObject lastParkour;
    private int lastRandomIndex = -1;

    void Start()
    {
        GenerateParkour();
    }

    void GenerateParkour()
    {
        for (int i = 0; i < parkourCount; i++)
        {
            if (i == 0)
            {
                lastParkour = Instantiate(parkourPrefab, Vector3.zero, Quaternion.identity);
            }
            else
            {
                Parkour parkourScript = lastParkour.GetComponent<Parkour>();

                if (parkourScript == null || parkourScript.points.Count == 0)
                {
                    Debug.LogWarning("Parkour prefab has no valid exit points!");
                    return;
                }

                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, parkourScript.points.Count);
                } while (randomIndex == lastRandomIndex);

                lastRandomIndex = randomIndex;
                lastExitPoint = parkourScript.points[randomIndex];
                lastExitRotation = lastExitPoint.rotation;

                Quaternion modifiedRotation = Quaternion.Euler(
                    lastExitRotation.eulerAngles.x,
                    lastExitRotation.eulerAngles.y + 180,
                    lastExitRotation.eulerAngles.z);

                lastParkour = Instantiate(parkourPrefab, lastExitPoint.position, modifiedRotation);

            }
        }
    }
}