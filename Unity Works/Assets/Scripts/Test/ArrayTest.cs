using System.Collections.Generic;
using UnityEngine;

public class ArrayTest : MonoBehaviour
{
    [SerializeField] private List<List<GameObject>> lines = new List<List<GameObject>>();

    [SerializeField] private List<GameObject> lineOne = new List<GameObject>();
    [SerializeField] private List<GameObject> lineTwo = new List<GameObject>();
    [SerializeField] private List<GameObject> lineTre = new List<GameObject>();


    public int Score = 0;
    int lineNUmber;
    string emptySTR;

    [SerializeField] private List<Color> colors = new List<Color>();

    private void Start()
    {
        lines.Add(lineOne);
        lines.Add(lineTwo);
        lines.Add(lineTre);


        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Count; j++)
            {
                Debug.Log(lines[i][j].GetComponent<BlockObject>().Block.blockNumber);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GetFillBlockValue();

        if (Input.GetKeyUp(KeyCode.G))
        {
            SetGame();
        }
    }


    //It is controlling
    private void GetFillBlockValue()
    {
        // Yatay kontrol
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i][0].GetComponent<BlockObject>().Block.color ==
                lines[i][1].GetComponent<BlockObject>().Block.color &&
                lines[i][1].GetComponent<BlockObject>().Block.color ==
                lines[i][2].GetComponent<BlockObject>().Block.color)
            {
                Score++;
            }
        }

        // Dikey kontrol
        for (int i = 0; i < lines[0].Count; i++)
        {
            if (lines[0][i].GetComponent<BlockObject>().Block.color ==
                lines[1][i].GetComponent<BlockObject>().Block.color &&
                lines[1][i].GetComponent<BlockObject>().Block.color ==
                lines[2][i].GetComponent<BlockObject>().Block.color)
            {
                Score++;
            }
        }

        Debug.Log("Score: " + Score);
    }


    private void SetGame()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Count; j++)
            {
                int randomColor = Random.RandomRange(0, colors.Count);

                lines[i][j].GetComponent<BlockObject>().Block.color = colors[randomColor];
                lines[i][j].GetComponent<BlockObject>().SetBlock();

            }
        }
    }
}
