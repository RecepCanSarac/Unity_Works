using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LokomotifController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 120f;

    private Rigidbody rb;
    public bool isMove = true;
    private float turn;
    public Transform targetPoint;
    private bool moveToTarget = false;
    private BaseBuild baseBuild;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        baseBuild = GameObject.Find("BaseBuild").GetComponent<BaseBuild>();
    }

    void Update()
    {
        if (moveToTarget)
        {
            MoveTowardsTarget();
        }
        else if (isMove)
        {
            turn = Input.GetAxis("Horizontal");
            Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0f, turn * turnSpeed * Time.deltaTime, 0f);

            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * rotation);
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            RestartTrainMovement();
        }
    }

    private void MoveTowardsTarget()
    {
        if (targetPoint == null) return;

        Vector3 direction = (targetPoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPoint.position);

        if (distance < 0.5f)
        {
            moveToTarget = false;
            moveSpeed = 0;
            StopTrain();
            return;
        }

        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        rb.MoveRotation(smoothedRotation);
    }

    private void StopTrain()
    {
        Transform train = GetComponent<RayDoser>().train;
        List<Transform> childList = new List<Transform>();
        foreach (Transform child in train)
        {
            childList.Add(child);
        }
        foreach (Transform child in childList)
        {
            child.GetComponent<TrainWagon>().isMove = false;
        }
        
        baseBuild.SwitchToKabinCamera(true);
    }

    public void RestartTrainMovement()
    {
        isMove = true; 
        moveSpeed = 5f;
        baseBuild.firstIn = false;
        Transform train = GetComponent<RayDoser>().train;
        List<Transform> childList = new List<Transform>();

        foreach (Transform child in train)
        {
            childList.Add(child);
        }
        foreach (Transform child in childList)
        {
            child.GetComponent<TrainWagon>().isMove = true;
        }
        
        baseBuild.SwitchToKabinCamera(false);
    }

    public void StartMoveToTarget(Transform target)
    {
        isMove = false;
        moveToTarget = true;
        targetPoint = target;
    }
}