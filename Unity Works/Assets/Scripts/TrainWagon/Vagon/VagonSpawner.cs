using System;
using UnityEngine;

public class VagonSpawner : MonoBehaviour
{
    [Header("Vagon Ayarları")] 
    public GameObject vagonPrefab;
    public Transform trainParent;
    public RayDoser rayDoser;
    public float spawnGap = 1f;

    private int vagonCount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            AddNewVagon();
        }
    }

    public void AddNewVagon()
    {
        Transform referenceTransform = rayDoser.lastWagon != null
            ? rayDoser.lastWagon
            : trainParent.GetChild(0);

        GameObject newVagon = Instantiate(vagonPrefab, trainParent);

        Vector3 spawnOffset = -referenceTransform.forward * spawnGap;
        newVagon.transform.position = referenceTransform.position + spawnOffset;
        newVagon.transform.rotation = referenceTransform.rotation;

        TrainWagon wagon = newVagon.GetComponent<TrainWagon>();
        wagon.raySource = rayDoser;
        wagon.followDistance = spawnGap;
        wagon.leadingWagon = referenceTransform;

        rayDoser.lastWagon = newVagon.transform;

        vagonCount++;
    }
}