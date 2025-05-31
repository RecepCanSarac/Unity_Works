using System.Collections;
using UnityEngine;

public class FootController : MonoBehaviour
{
    [Header("Referanslar")]
    public Transform footA;
    public Transform footB;
    public Transform bodyTransform;

    [Header("Adým Ayarlarý")]
    public float stepDistance = 0.5f;
    public float stepDuration = 0.2f;
    public float stepHeight = 0.1f;

    [Header("Dönüþ")]
    public float rotationSpeed = 10f;

    private bool isFootAStepping = false;
    private bool isFootBStepping = false;
    private bool isFootATurn = true;

    private void Update()
    {
        // W/A/S/D yönünü topla
        Vector3 inputDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) inputDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) inputDirection += Vector3.back;
        if (Input.GetKey(KeyCode.A)) inputDirection += Vector3.left;
        if (Input.GetKey(KeyCode.D)) inputDirection += Vector3.right;

        if (inputDirection.magnitude > 0.1f)
        {
            inputDirection = inputDirection.normalized;
            TryStep(inputDirection);
        }

        UpdateBodyBetweenFeet();
    }

    void TryStep(Vector3 moveDirection)
    {
        if (!isFootAStepping && !isFootBStepping)
        {
            // Karakteri yönüne döndür
            Vector3 worldMoveDir = moveDirection.normalized;
            if (worldMoveDir != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(worldMoveDir);
                bodyTransform.rotation = Quaternion.Slerp(bodyTransform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }

            Quaternion footRot = Quaternion.LookRotation(worldMoveDir);

            if (isFootATurn)
            {
                Vector3 newTarget = GetStepTarget(worldMoveDir, -0.2f);
                StartCoroutine(MoveFoot(footA, newTarget, footRot, () => isFootATurn = false));
            }
            else
            {
                Vector3 newTarget = GetStepTarget(worldMoveDir, 0.2f);
                StartCoroutine(MoveFoot(footB, newTarget, footRot, () => isFootATurn = true));
            }
        }
    }


    Vector3 GetStepTarget(Vector3 moveDirWorld, float lateralOffset)
    {
        Vector3 center = bodyTransform.position;

        // Yan adým için çapraz vektör (sað/sol)
        Vector3 side = Vector3.Cross(Vector3.up, moveDirWorld).normalized * lateralOffset;

        // Hedef noktayý hesapla
        Vector3 rawTarget = center + moveDirWorld * stepDistance + side;

        // Zemine hizala (raycast)
        if (Physics.Raycast(rawTarget + Vector3.up, Vector3.down, out RaycastHit hit, 2f))
        {
            return hit.point;
        }

        return rawTarget;
    }

    IEnumerator MoveFoot(Transform foot, Vector3 targetPos, Quaternion targetRot, System.Action onComplete)
    {
        Vector3 startPos = foot.position;
        Quaternion startRot = foot.rotation;

        float timer = 0f;

        if (foot == footA) isFootAStepping = true;
        else isFootBStepping = true;

        while (timer < stepDuration)
        {
            float t = timer / stepDuration;
            float heightOffset = Mathf.Sin(t * Mathf.PI) * stepHeight;

            // Pozisyon
            Vector3 pos = Vector3.Lerp(startPos, targetPos, t);
            pos.y += heightOffset;
            foot.position = pos;

            // Rotasyon
            foot.rotation = Quaternion.Slerp(startRot, targetRot, t);

            timer += Time.deltaTime;
            yield return null;
        }

        foot.position = targetPos;
        foot.rotation = targetRot;

        if (foot == footA) isFootAStepping = false;
        else isFootBStepping = false;

        onComplete?.Invoke();
    }


    void UpdateBodyBetweenFeet()
    {
        Vector3 center = (footA.position + footB.position) / 2f;

        Vector3 bodyPos = bodyTransform.position;
        bodyPos.x = center.x;
        bodyPos.z = center.z;
        bodyPos.y = center.y; // gövde-yer yüksekliði karakterine göre ayarla

        bodyTransform.position = Vector3.Lerp(bodyTransform.position, bodyPos, 10f * Time.deltaTime);
    }
}
