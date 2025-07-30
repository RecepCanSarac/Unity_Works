using System;
using UnityEngine;

public class MouseSelected : MonoBehaviour
{
    public static MouseSelected instance;
    public event Action<GameObject> vagonSelected;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            RayHitMethod();
    }

    void RayHitMethod()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.CompareTag("TrainPart"))
            {
                vagonSelected?.Invoke(hit.collider.gameObject);
            }
        }
    }
}