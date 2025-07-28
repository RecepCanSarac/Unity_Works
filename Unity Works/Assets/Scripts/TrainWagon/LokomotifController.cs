using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LokomotifController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 120f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        float forward = Input.GetAxis("Vertical");   // W/S
        float turn = Input.GetAxis("Horizontal");    // A/D

        Vector3 movement = transform.forward  * moveSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0f, turn * turnSpeed * Time.deltaTime, 0f);

        rb.MovePosition(rb.position + movement);
        rb.MoveRotation(rb.rotation * rotation);
    }
}