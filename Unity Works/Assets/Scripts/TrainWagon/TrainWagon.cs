using UnityEngine;

public class TrainWagon : MonoBehaviour
{
    public RayDoser raySource;
    public float rayOffset = 5;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    void Update()
    {
        if (raySource == null || raySource.GetRailCount() <= rayOffset) return;

        Transform targetRail = raySource.GetRailTransformAtOffsetFromEnd(rayOffset);
        transform.position = Vector3.MoveTowards(transform.position, targetRail.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetRail.forward), Time.deltaTime * rotationSpeed);
    }
}
