using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VagonCabin : MonoBehaviour
{
    public Transform train;
    public Transform lokomotif;
    public Transform cameraPivot;

    private List<Transform> allParts = new List<Transform>();
    public int currentIndex = 0;

    public GameObject vagonBuyPanel;

    void Start()
    {
        RefreshList();
        MoveToCurrent();
    }

    void Update()
    {
    }
    
    public void RefreshList()
    {
        allParts.Clear();

        if (lokomotif != null)
            allParts.Add(lokomotif);

        foreach (Transform vagon in train)
            allParts.Add(vagon);

        currentIndex = Mathf.Clamp(currentIndex, 0, allParts.Count - 1);

        MoveToCurrent();
    }


    void MoveToCurrent()
    {
        if (vagonBuyPanel != null)
        {
            bool lokomotifteyiz = currentIndex == 0;
            vagonBuyPanel.SetActive(lokomotifteyiz);
        }

        StopAllCoroutines();
        StartCoroutine(SmoothMoveToCurrent());
    }

    IEnumerator SmoothMoveToCurrent()
    {
        if (cameraPivot == null || allParts.Count == 0)
            yield break;

        Transform target = allParts[currentIndex];

        Vector3 startPos = cameraPivot.position;
        Quaternion startRot = cameraPivot.rotation;

        Vector3 targetPos = target.position;
        Quaternion targetRot = target.rotation;

        float t = 0;
        float duration = 0.3f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            cameraPivot.position = Vector3.Lerp(startPos, targetPos, t);
            cameraPivot.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }
    }


    public void MoveNext()
    {
        currentIndex++;
        if (currentIndex >= allParts.Count)
            currentIndex = allParts.Count - 1;

        MoveToCurrent();
    }

    public void MovePrevious()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = 0;

        MoveToCurrent();
    }

    private void OnDisable()
    {
        vagonBuyPanel.SetActive(false);
    }
}