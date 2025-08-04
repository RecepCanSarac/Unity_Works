using UnityEngine;

[CreateAssetMenu(fileName = "WagonData", menuName = "Scriptable Objects/WagonData")]
public class WagonData : ScriptableObject
{
    public string WagonName;
    public float WagonHealth;
    public float WagonCapacity;
    public Sprite WagonSprite;
    public float WagonPrice;
    public GameObject WagonPrefab;
}