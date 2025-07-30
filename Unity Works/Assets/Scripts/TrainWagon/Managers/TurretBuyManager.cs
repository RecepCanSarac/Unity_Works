using System.Collections.Generic;
using UnityEngine;

public class TurretBuyManager : MonoBehaviour
{
    public List<Turret> turrets = new List<Turret>();
    public List<VagonCard> cards = new List<VagonCard>();
    public GameObject vagonCard;
    public Transform vagonCardParent;

    private void Start()
    {
        for (int i = 0; i < turrets.Count; i++)
        {
            GameObject newCard = Instantiate(vagonCard, vagonCardParent);

            VagonCard vagonComponent = newCard.GetComponent<VagonCard>();

            if (vagonComponent != null)
            {
                vagonComponent.SetCart(turrets[i].name, turrets[i].price, turrets[i].sprite,
                    () => BuyTurret(turrets[i - 1]));


                cards.Add(vagonComponent);
            }
        }
    }

    public void BuyTurret(Turret turret)
    {
        Debug.Log(turret.name + " bought.");
    }
}