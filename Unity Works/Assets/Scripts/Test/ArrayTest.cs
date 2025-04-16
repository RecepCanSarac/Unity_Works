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
            SetGame(false);
            SetGame();
        }
    }


    //It is controlling
    private void GetFillBlockValue()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            lineNUmber = 0;
            for (int j = 0; j < lines[i].Count; j++)
            {
                if (lines[i][j].GetComponent<BlockObject>().Block.isFill)
                    lineNUmber++;
            }
            Score += lineNUmber == 3 ? 1 : 0;
        }

        for (int i = 0; i < lines[0].Count; i++)
        {
            lineNUmber = 0;
            for (int j = 0; j < lines.Count; j++)
            {
                if (lines[j][i].GetComponent<BlockObject>().Block.isFill)
                    lineNUmber++;
            }
            Score += lineNUmber == 3 ? 1 : 0;
        }
        Debug.Log("Score" + Score);
    }


    private void SetGame(bool isGame)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Count; j++)
            {
                int randomNumber = Random.Range(0, 101);

                lines[i][j].GetComponent<BlockObject>().Block.isFill = isGame;
                lines[i][j].GetComponent<BlockObject>().SetBlock();
            }
        }
    }
    private void SetGame()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            for (int j = 0; j < lines[i].Count; j++)
            {
                int randomNumber = Random.Range(0, 101);

                if (randomNumber <= 10)
                {
                    lines[i][j].GetComponent<BlockObject>().Block.isFill = true;
                    lines[i][j].GetComponent<BlockObject>().SetBlock();
                }
            }
        }
    }
}
