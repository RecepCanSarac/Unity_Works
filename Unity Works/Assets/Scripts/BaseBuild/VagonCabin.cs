using System.Collections.Generic;
using UnityEngine;

public class VagonCabin : MonoBehaviour
{
    public Transform train;
    public Transform lokomotif;
    public Transform cameraPivot;

    private List<Transform> allParts = new List<Transform>();
    private int currentIndex = 0;

    void Start()
    {
        RefreshList();
        currentIndex = 0;
        MoveToCurrent();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Y tuşuna basıldı");
            MoveNext();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("U tuşuna basıldı");
            MovePrevious();
        }
    }

    public void RefreshList()
    {
        currentIndex = 0;
        
        allParts.Clear();

        if (lokomotif != null)
            allParts.Add(lokomotif);

        foreach (Transform vagon in train)
        {
            allParts.Add(vagon);
        }

        currentIndex = Mathf.Clamp(currentIndex, 0, allParts.Count - 1);
    }

    void MoveToCurrent()
    {
        if (cameraPivot != null && allParts.Count > 0)
        {
            Transform target = allParts[currentIndex];

            cameraPivot.position = target.position;
            cameraPivot.rotation = target.rotation;

            Debug.Log("Kabin pozisyonu güncellendi: " + target.name);
        }
    }



    public void MoveNext()
    {
        Debug.Log("Y tuşuna basıldı");
        currentIndex++;
        if (currentIndex >= allParts.Count)
            currentIndex = allParts.Count - 1;

        MoveToCurrent();
    }

    public void MovePrevious()
    {
        Debug.Log("U tuşuna basıldı");
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = 0;

        MoveToCurrent();
    }
}