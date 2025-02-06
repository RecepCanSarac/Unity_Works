using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    public Camera cameraB;

    public Material CameraMatB;

    void Start()
    {
        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        CameraMatB.mainTexture = cameraB.targetTexture;
    }

}
