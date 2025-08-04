using System;
using System.Collections.Generic;
using UnityEngine;

public class VagonSpawner : MonoBehaviour
{
    [Header("Vagon Ayarları")] public GameObject vagonPrefab;
    public Transform trainParent;
    public RayDoser rayDoser;
    public float spawnGap = 1f;
    private Vector3 lastSpawnPosition;
    private Quaternion lastSpawnRotation;
    private bool hasSpawned = false;
    private int vagonCount = 0;

    [Header("Vagon List")] public List<WagonData> wagonData = new List<WagonData>();

    [Header("WagonBuyCard")] public WagonBuyCard wagonBuyCard;
    public Transform Wagonardparent;
    
    private void Start()
    {
        for (int i = 0; i < wagonData.Count; i++)
        {
            Debug.Log($"Kart oluşturuluyor: {wagonData[i].WagonName}");

            int index = i;
            WagonBuyCard newWagonCard = Instantiate(wagonBuyCard, Wagonardparent);
            newWagonCard.SetCard(wagonData[i], () => { AddNewVagon(wagonData[index]); });
        }
    }


   
    public void AddNewVagon(WagonData data)
    {
        if (!hasSpawned)
        {
            Transform first = rayDoser.lastWagon != null ? rayDoser.lastWagon : trainParent.GetChild(0);
            lastSpawnPosition = first.position;
            lastSpawnRotation = first.rotation;
            hasSpawned = true;
        }

        GameObject newVagon = Instantiate(data.WagonPrefab, trainParent);

        lastSpawnPosition += (lastSpawnRotation) * (-Vector3.forward * spawnGap);

        newVagon.transform.position = lastSpawnPosition;
        newVagon.transform.rotation = lastSpawnRotation;

        TrainWagon wagon = newVagon.GetComponent<TrainWagon>();
        wagon.raySource = rayDoser;
        wagon.followDistance = spawnGap;
        wagon.leadingWagon = rayDoser.lastWagon != null ? rayDoser.lastWagon : trainParent.GetChild(0);

        rayDoser.lastWagon = newVagon.transform;
        vagonCount++;
    }
}