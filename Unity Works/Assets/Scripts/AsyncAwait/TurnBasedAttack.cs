using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TurnBasedAttack : MonoBehaviour
{
    public GameObject enemy;
    public float moveSpeed = 4f;

    private Vector3 startPosition;

    public TextMeshProUGUI infoText;

    void Start()
    {
        startPosition = transform.position;
    }

    public async void OnPressAttack()
    {
        infoText.text = "The attack has begun";
        await MoveToEnemy();
        await DealDamage();
        await ShowDamageText();
        infoText.text = "The attack ended";
    }

    private async Task MoveToEnemy()
    {
        infoText.text = "Moving";

        while (Vector3.Distance(transform.position, enemy.transform.position) > 0.5f)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, enemy.transform.position, moveSpeed * Time.deltaTime);
            await Task.Yield();
        }
    }

    private async Task DealDamage()
    {
        infoText.text = "Dealing damage";
        await Task.Delay(1000);
        infoText.text = Random.Range(1, 11).ToString();
    }

    private async Task ShowDamageText()
    {
        infoText.text = "The damage was done";
        await Task.Delay(1000);
    }
    
    
}