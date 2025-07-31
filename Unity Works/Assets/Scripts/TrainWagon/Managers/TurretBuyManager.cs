using System.Collections.Generic;
using UnityEngine;

public class TurretBuyManager : MonoBehaviour
{
    public List<Turret> turrets = new List<Turret>();
    public List<VagonCard> cards = new List<VagonCard>();
    public GameObject vagonCard;
    public Transform vagonCardParent;

    private Transform currentVagon;
    
    private void OnDisable()
    {
        MouseSelected.instance.vagonSelected -= OnVagonSelected;
    }

    private void Start()
    {
        MouseSelected.instance.vagonSelected += OnVagonSelected;
        for (int i = 0; i < turrets.Count; i++)
        {
            GameObject newCard = Instantiate(vagonCard, vagonCardParent);
            VagonCard vagonComponent = newCard.GetComponent<VagonCard>();

            if (vagonComponent != null)
            {
                int index = i; 
                vagonComponent.SetCart(turrets[index].name, turrets[index].price, turrets[index].sprite,
                    () => BuyTurret(turrets[index], vagonComponent));
                cards.Add(vagonComponent);
            }
        }
    }

    private void OnVagonSelected(GameObject obj)
    {
        currentVagon = obj.transform;
    }

    public void BuyTurret(Turret turret, VagonCard vagonComponent)
    {
        if (currentVagon == null)
        {
            Debug.LogWarning("No vagon selected.");
            return;
        }

        VagonCapacity capacity = currentVagon.GetComponent<VagonCapacity>();
        if (capacity == null)
        {
            Debug.LogWarning("Selected vagon does not have VagonCapacity script.");
            return;
        }

        if (!capacity.CanAddTurret())
        {
            Debug.LogWarning("This vagon has reached its turret slot limit.");
            return;
        }

        Transform spawnPoint = capacity.GetNextSlot();
        if (spawnPoint == null)
        {
            Debug.LogWarning("No available spawn point.");
            return;
        }

        GameObject newTower = Instantiate(turret.prefab, spawnPoint.position, spawnPoint.rotation, currentVagon);
        capacity.AddTurret();

        Debug.Log($"{turret.name} bought and placed at slot.");
    }

}