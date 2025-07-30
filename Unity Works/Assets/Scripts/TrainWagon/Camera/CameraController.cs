using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;           
    public Vector3 offset = new Vector3(0f, 10f, -10f);
    public float followSpeed = 5f;
    public bool lookAtTarget = true;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);

        if (lookAtTarget)
            transform.LookAt(target);
    }
}