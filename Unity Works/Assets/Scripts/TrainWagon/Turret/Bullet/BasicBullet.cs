using System;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float radius = 1f;

    public LayerMask targetMask;

    public float damage;

    private void Update()
    {
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (targetsInRange.Length > 0)
        {
            foreach (Collider target in targetsInRange)
            {
                target.GetComponent<EnemyChaser>().TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}