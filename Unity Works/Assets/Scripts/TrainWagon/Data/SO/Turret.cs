using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Scriptable Objects/Turret")]
public class Turret : ScriptableObject
{
    public string name;
    public float damage;
    public Sprite sprite;
    public int price;
    public GameObject prefab;
}
