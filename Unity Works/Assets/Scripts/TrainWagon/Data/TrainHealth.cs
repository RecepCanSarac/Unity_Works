using UnityEngine;

public class TrainHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Tren hasar aldı: " + amount + " ➤ Kalan: " + health);

        if (health <= 0)
        {
            Debug.Log("Tren yok oldu!");
            Destroy(gameObject);
        }
    }
}
