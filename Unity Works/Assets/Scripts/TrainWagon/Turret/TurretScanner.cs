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

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate)
        {
            Collider[] targetsInRange = Physics.OverlapSphere(transform.position, range, targetMask);

            if (targetsInRange.Length > 0)
            {
                Transform target = targetsInRange[0].transform;
                Fire(target);
                fireTimer = 0f;
            }
        }
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
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}