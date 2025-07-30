using System.Collections.Generic;
using UnityEngine;

public class RayDoser : MonoBehaviour
{
    public GameObject railPrefab;
    public float spawnDistance = 1f;
    public float maxRayLength = 30f;
    public Transform lastWagon;
    public List<Transform> railPoints = new();
    public Transform railTransform;
    private Vector3 lastSpawnPosition;

    void Start()
    {
        lastSpawnPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, lastSpawnPosition);
        if (distance >= spawnDistance)
        {
            SpawnRail();
            lastSpawnPosition = transform.position;
        }

        CleanupOldRails();
    }

    void SpawnRail()
    {
        if (railPrefab == null)
        {
            return;
        }

        GameObject newRail = Instantiate(railPrefab, railTransform.position, transform.rotation);
        railPoints.Add(newRail.transform);
    }

    void CleanupOldRails()
    {
        if (lastWagon == null || railPoints.Count == 0) return;

        while (railPoints.Count > 0)
        {
            Transform oldest = railPoints[0];

            float distanceToLastWagon = Vector3.Distance(oldest.position, lastWagon.position);

            Vector3 directionToRay = oldest.position - lastWagon.position;
            float dot = Vector3.Dot(lastWagon.forward, directionToRay);

            if (dot < 0 && distanceToLastWagon > 0.5f)
            {
                Destroy(oldest.gameObject);
                railPoints.RemoveAt(0);
            }
            else break;
        }
    }

    public Transform GetRailTransformAtOffsetFromEnd(float offset)
    {
        float index = Mathf.Clamp(railPoints.Count - 1 - offset, 0, railPoints.Count - 1);
        return railPoints[(int)index];
    }

    public int GetRailCount()
    {
        return railPoints.Count;
    }
}