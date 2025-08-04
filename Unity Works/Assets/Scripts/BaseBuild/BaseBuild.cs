using System;
using UnityEngine;

public class BaseBuild : MonoBehaviour
{
    public float Radius = 10f;
    public LayerMask targetMask;
    public Vector3 cameraLongLook;
    public Vector3 cameraShortLook;
    public bool insideTheBuild = false;
    public Transform targetPoint;
    public bool firstIn = true;
    public GameObject VagonKabin;
    public GameObject CabinUIObject;
    public Camera kabinCamera;
    public Camera mainCamera;
    private void Update()
    {
        if (firstIn)
        {
            CheckIfInside();
        }
    }


    private void CheckIfInside()
    {
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, Radius, targetMask);

        if (targetsInRange.Length > 0)
        {
            insideTheBuild = true;

            LokomotifController lokomotif = targetsInRange[0].GetComponent<LokomotifController>();
            if (lokomotif != null)
            {
                lokomotif.StartMoveToTarget(targetPoint);
            }
        }
        else
        {
            insideTheBuild = false;
            firstIn = true;
            SwitchToKabinCamera(false);
        }
    }

    public void SwitchToKabinCamera(bool isActiveCamera)
    {
        if (mainCamera != null)
        {
            CabinUIObject.SetActive(!isActiveCamera);;
            VagonKabin.SetActive(!isActiveCamera);
            mainCamera.gameObject.SetActive(!isActiveCamera);
        }

        if (kabinCamera != null)
        {
            CabinUIObject.SetActive(isActiveCamera);
            VagonKabin.SetActive(isActiveCamera);
            kabinCamera.gameObject.SetActive(isActiveCamera);
            VagonKabin.GetComponent<VagonCabin>().RefreshList();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}