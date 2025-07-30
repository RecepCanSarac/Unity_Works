using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float stopDistance = 2f;
    public string trainTag = "TrainPart";
    public GameObject explosionEffect;
    public int damageAmount = 10;

    private Transform currentTarget;

    void Update()
    {
        FindClosestTrainPart();

        if (currentTarget == null) return;

        float distance = Vector3.Distance(transform.position, currentTarget.position);

        if (distance > stopDistance)
        {
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(currentTarget);
        }
        else
        {
            Explode();
        }
    }

    void FindClosestTrainPart()
    {
        GameObject[] trainParts = GameObject.FindGameObjectsWithTag(trainTag);
        float closestDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject part in trainParts)
        {
            float dist = Vector3.Distance(transform.position, part.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = part.transform;
            }
        }

        currentTarget = closest;
    }

    void Explode()
    {
        if (currentTarget != null)
        {
            TrainHealth health = currentTarget.GetComponent<TrainHealth>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }

        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}