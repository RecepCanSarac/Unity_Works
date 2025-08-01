using System.Collections.Generic;
using UnityEngine;

public class TrainWagon : MonoBehaviour
{
    public RayDoser raySource;
    public Transform leadingWagon;
    public float followDistance = 3f;
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;

    private void Start()
    {
        if (raySource == null)
            raySource = transform.root.GetComponentInChildren<RayDoser>();
    }

    void Update()
    {
        if (raySource == null || raySource.GetRailCount() <= 1 || leadingWagon == null)
            return;

        Vector3 behindTarget = leadingWagon.position - leadingWagon.forward * followDistance;

        Transform closestRail = GetClosestRailToPoint(behindTarget);
        if (closestRail == null) return;

        transform.position = Vector3.MoveTowards(transform.position, closestRail.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(closestRail.forward), rotationSpeed * Time.deltaTime);
    }

    Transform GetClosestRailToPoint(Vector3 point)
    {
        List<Transform> rails = raySource.railPoints;
        if (rails == null || rails.Count == 0) return null;

        Transform closest = rails[0];
        float minDist = Vector3.Distance(point, closest.position);

        foreach (Transform rail in rails)
        {
            float dist = Vector3.Distance(point, rail.position);
            if (dist < minDist)
            {
                closest = rail;
                minDist = dist;
            }
        }

        return closest;
    }
}
