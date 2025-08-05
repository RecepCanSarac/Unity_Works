using System;
using UnityEngine;

public class VagonSelected : MonoBehaviour
{
    public WagonType Type;
    public WagonData Data;
    private CameraController cameraController;
    private LokomotifController lokomotifController;
    public GameObject turretObject;
    
    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        lokomotifController = GameObject.Find("Lokomotif").GetComponent<LokomotifController>();
    }

    private void Start()
    {
        if (MouseSelected.instance != null)
        {
            MouseSelected.instance.vagonSelected += OnVagonSelected;
        }
    }

    private void OnEnable()
    {
        MouseSelected.instance.vagonSelected += OnVagonSelected;
    }

    private void OnDisable()
    {
        MouseSelected.instance.vagonSelected -= OnVagonSelected;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Deselect();
        }
    }

    private void OnVagonSelected(GameObject obj)
    {
        lokomotifController.isMove = false;
        cameraController.target = obj.transform;
        cameraController.offset = new Vector3(2.5f, 3f, -3f);
    }

    public void Deselect()
    {
        lokomotifController.isMove = true;
        cameraController.target = lokomotifController.transform;
        cameraController.offset = new Vector3(0f, 25f, -10f);
       
    }
}