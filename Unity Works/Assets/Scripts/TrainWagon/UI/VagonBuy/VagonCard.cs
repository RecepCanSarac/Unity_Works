using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VagonCard : MonoBehaviour
{
    public Image turretImage;
    public TextMeshProUGUI turretName;
    public TextMeshProUGUI turretPrice;
    public Button buyButton;

    public void SetCart(string name, int price, Sprite image, Action buy
    )
    {
        turretImage.sprite = image;
        turretName.text = name;
        turretPrice.text = price.ToString();
        buyButton.onClick.AddListener(() =>
        {
            buy?.Invoke();
        });
    }
}