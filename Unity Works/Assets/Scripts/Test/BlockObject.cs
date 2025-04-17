using UnityEngine;
using UnityEngine.UI;

public class BlockObject : MonoBehaviour
{
    public Block Block;


    public void SetBlock()
    {
        GetComponent<Image>().color = Block.color;
    }
}
