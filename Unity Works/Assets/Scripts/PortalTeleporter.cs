using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;  // Oyuncunun Transform bileþeni
    public Transform reciever; // Hedef portalýn Transform bileþeni

    private bool playerIsOverlapping = false;

    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, portalToPlayer);

            if (dotProduct < 0f) // Oyuncu portaldan geçiyorsa
            {
                Debug.Log("Teleporting player to receiver...");

                // Oyuncunun portaldaki konumunu receiver'daki karþýlýðýna çevir
                Vector3 localPosition = transform.InverseTransformPoint(player.position);
                Vector3 newPosition = reciever.TransformPoint(localPosition);
                newPosition += reciever.forward * 1.0f; // Oyuncuyu biraz ileri al

                // Oyuncunun pozisyonunu ayarla
                player.position = newPosition;

                // Oyuncunun yönünü de receiver ile eþleþtir
                Quaternion rotationOffset = reciever.rotation * Quaternion.Inverse(transform.rotation);
                player.rotation = rotationOffset * player.rotation;

                playerIsOverlapping = false;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = true;
            Debug.Log("Player entered portal.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsOverlapping = false;
            Debug.Log("Player exited portal.");
        }

    }
}
