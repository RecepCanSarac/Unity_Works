using UnityEngine;

public class TurretScanner : MonoBehaviour
{
    public Turret turret;
    public LayerMask targetMask;
    public float range = 10f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 15f;

    private float fireTimer = 0f;

    public Transform rotateBody;
    public ParticleSystem fireParticles;
    public float rotateSpeed = 5f;

    private Transform currentTarget;

    void Update()
    {
        fireTimer += Time.deltaTime;

        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, range, targetMask);
        if (targetsInRange.Length > 0)
        {
            currentTarget = targetsInRange[0].transform;

            RotateTowardsTarget();

            if (IsLookingAtTarget() && fireTimer >= 1f / fireRate)
            {
                Fire(currentTarget);
                fireTimer = 0f;
            }
        }
        else
        {
            currentTarget = null;
        }
    }

    void RotateTowardsTarget()
    {
        if (rotateBody == null || currentTarget == null) return;

        Vector3 dir = currentTarget.position - rotateBody.position;
        dir.y = 0f; 

        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            rotateBody.rotation = Quaternion.RotateTowards(rotateBody.rotation, targetRot, rotateSpeed * Time.deltaTime);
        }
    }

    bool IsLookingAtTarget()
    {
        if (rotateBody == null || currentTarget == null) return false;

        Vector3 toTarget = currentTarget.position - rotateBody.position;
        toTarget.y = 0f;

        float angle = Vector3.Angle(rotateBody.forward, toTarget.normalized);
        return angle < 5f; 
    }

    void Fire(Transform target)
    {
        if (bulletPrefab == null || shootPoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<BasicBullet>().damage = turret.damage;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 dir = (target.position - shootPoint.position).normalized;
            rb.linearVelocity = dir * bulletSpeed;
        }

        if (fireParticles != null)
            fireParticles.Play();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
