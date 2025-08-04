using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WagonBuyCard : MonoBehaviour
{
    public WagonData data;
    public Image image;
    public TextMeshProUGUI name;
    public TextMeshProUGUI price;
    public TextMeshProUGUI capacity;
    public TextMeshProUGUI health;
    public Button buyButton;


    public void SetCard(WagonData data, Action buy)
    {
        this.data = data;
        image.sprite = data.WagonSprite;
        name.text = data.WagonName;
        price.text = data.WagonPrice.ToString();
        capacity.text = data.WagonCapacity.ToString();
        health.text = data.WagonHealth.ToString();

        buyButton.onClick.AddListener(() => { buy?.Invoke(); });
        
    }
}