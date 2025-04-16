using UnityEngine;
using UnityEngine.UI;

public class BlockObject : MonoBehaviour
{
    public Block Block;


    public void SetBlock()
    {
        if (Block.isFill)
        {
            GetComponent<Image>().color = Color.red;
        }
        else if (!Block.isFill)
        {
            GetComponent<Image>().color = Color.white;
        }
    }
}
