using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuyManager : MonoBehaviour
{
    public List<Turret> turrets = new List<Turret>();
    public List<VagonCard> cards = new List<VagonCard>();
    public GameObject vagonCard;
    public Transform vagonCardParent;
    public GameObject VagonPanelPrefab;
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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VagonPanelPrefab.SetActive(false);
        }
    }

    private void OnVagonSelected(GameObject obj)
    {
        currentVagon = obj.transform;
        
        VagonPanelPrefab.SetActive(true);
    }

    public void BuyTurret(Turret turret, VagonCard vagonComponent)
    {
        if (currentVagon == null)
        {
            return;
        }

        VagonCapacity capacity = currentVagon.GetComponent<VagonCapacity>();
        if (capacity == null)
        {
            return;
        }

        if (!capacity.CanAddTurret())
        {
            return;
        }

        Transform spawnPoint = capacity.GetNextSlot();
        if (spawnPoint == null)
        {
            return;
        }

        GameObject newTower = Instantiate(turret.prefab, spawnPoint.position, spawnPoint.rotation, currentVagon);
        capacity.AddTurret();

    }

}