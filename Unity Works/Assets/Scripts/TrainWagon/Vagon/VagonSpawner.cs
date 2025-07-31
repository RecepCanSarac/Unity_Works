using System;
using UnityEngine;

public class VagonSpawner : MonoBehaviour
{
    public GameObject vagonPrefab;
    public Transform trainParent; 
    public RayDoser rayDoser;
    public float rayOffsetPerVagon = 3f;

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
            : rayDoser.transform;

        GameObject newVagon = Instantiate(vagonPrefab, trainParent);

        Vector3 spawnOffset = -referenceTransform.forward * 2f;
        newVagon.transform.position = referenceTransform.position + spawnOffset;
        newVagon.transform.rotation = referenceTransform.rotation;

        float offset = (vagonCount + 1) * rayOffsetPerVagon;

        TrainWagon wagon = newVagon.GetComponent<TrainWagon>();
        wagon.rayOffset = offset;
        wagon.raySource = rayDoser;

        rayDoser.lastWagon = newVagon.transform;

        vagonCount++;
    }
}
