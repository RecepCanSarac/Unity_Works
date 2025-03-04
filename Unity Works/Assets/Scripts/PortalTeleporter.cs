using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;  // Oyuncunun Transform bile�eni
    public Transform reciever; // Hedef portal�n Transform bile�eni

    private bool playerIsOverlapping = false;

    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, portalToPlayer);

            if (dotProduct < 0f) // Oyuncu portaldan ge�iyorsa
            {
                Debug.Log("Teleporting player to receiver...");

                // Oyuncunun portaldaki konumunu receiver'daki kar��l���na �evir
                Vector3 localPosition = transform.InverseTransformPoint(player.position);
                Vector3 newPosition = reciever.TransformPoint(localPosition);
                newPosition += reciever.forward * 1.0f; // Oyuncuyu biraz ileri al

                // Oyuncunun pozisyonunu ayarla
                player.position = newPosition;

                // Oyuncunun y�n�n� de receiver ile e�le�tir
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
