using System;
using System.Collections.Generic;
using UnityEngine;

public class TrainWagon : MonoBehaviour
{
    public RayDoser raySource;
    public float rayOffset = 5f;
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;


    private void Start()
    {
        if (raySource == null)
            raySource = transform.root.GetComponentInChildren<RayDoser>();
    }

    void Update()
    {
        if (raySource == null || raySource.GetRailCount() <= 1) return;

        Transform target = GetTargetTransformByDistance(rayOffset);

        if (target == null) return;

        Vector3 targetPos = target.position + Vector3.up * 0.5f;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.forward), rotationSpeed * Time.deltaTime);
    }

    Transform GetTargetTransformByDistance(float distanceOffset)
    {
        List<Transform> rails = raySource.railPoints;

        float accumulatedDistance = 0f;

        for (int i = rails.Count - 1; i > 0; i--)
        {
            float segmentLength = Vector3.Distance(rails[i].position, rails[i - 1].position);
            accumulatedDistance += segmentLength;

            if (accumulatedDistance >= distanceOffset)
            {
                return rails[i - 1];
            }
        }

        return rails[0];
    }
}