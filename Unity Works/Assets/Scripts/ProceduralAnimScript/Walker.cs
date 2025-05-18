using UnityEngine;

public class Walker : MonoBehaviour
{
    public Transform leftFootTarget;
    public Transform rightFootTarget;
    public AnimationCurve horizontalCurve;
    public AnimationCurve verticalCurve;


    private Vector3 leftTargetOffset;
    private Vector3 rightTargetOffset;


    private float leftLegLast = 0f;
    private float rightLegLast = 0f;
    void Start()
    {
        leftTargetOffset = leftFootTarget.localPosition;
        rightTargetOffset = rightFootTarget.localPosition;
    }

    void Update()
    {
        float leftLegForwardMovment = horizontalCurve.Evaluate(Time.time);
        float rightLegForwardMovment = horizontalCurve.Evaluate(Time.time - 1);

        leftFootTarget.localPosition = leftTargetOffset +
            this.transform.InverseTransformVector(leftFootTarget.forward) * leftLegForwardMovment +
            this.transform.InverseTransformVector(leftFootTarget.up) * verticalCurve.Evaluate(Time.time + .5f);
        rightFootTarget.localPosition = rightTargetOffset +
            this.transform.InverseTransformVector(rightFootTarget.forward) * rightLegForwardMovment +
            this.transform.InverseTransformVector(rightFootTarget.up) * verticalCurve.Evaluate(Time.time - .5f);

        float leftLegDirection = leftLegForwardMovment - leftLegLast;
        float rightLegDirection = rightLegForwardMovment - rightLegLast;

        RaycastHit hit;
        if (leftLegDirection < 0 && Physics.Raycast(leftFootTarget.position + leftFootTarget.up, -leftFootTarget.up, out hit, Mathf.Infinity))
        {
            leftFootTarget.position = hit.point;
            this.transform.position += this.transform.forward * Mathf.Abs(leftLegDirection);
        }
        if (rightLegDirection < 0 && Physics.Raycast(rightFootTarget.position + rightFootTarget.up, -rightFootTarget.up, out hit, Mathf.Infinity))
        {
            rightFootTarget.position = hit.point;
            this.transform.position += this.transform.forward * Mathf.Abs(rightLegDirection);
        }

        leftLegLast = leftLegForwardMovment;
        rightLegLast = rightLegForwardMovment;


    }
}
