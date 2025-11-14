using UnityEngine;

public class MirrorCameraConfig : MonoBehaviour
{
    public enum RenderTextureResolution
    {
        Lowest_256 = 256,
        Low_512 = 512,
        Medium_1024 = 1024,
        High_2048 = 2048
    }
    [SerializeField]
    private RenderTextureResolution renderTextureResolution = RenderTextureResolution.Medium_1024;

    [SerializeField] private Renderer mirrorRenderer;
    private Camera mirrorCamera;
    private RenderTexture mirrorRenderTexture;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mirrorCamera = GetComponentInChildren<Camera>();
        if (mirrorCamera == null)
        {
            Debug.LogError("MirrorCameraConfig: No Camera component found in children.");
            return;
        }
        mirrorRenderTexture = new RenderTexture((int)renderTextureResolution, (int)renderTextureResolution, 16);
        mirrorCamera.targetTexture = mirrorRenderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if (mirrorCamera == null)
        {
            Debug.LogError("MirrorCameraConfig: No Camera component found in children.");
            return;
        }

        if (mirrorRenderer == null)
        {
            Debug.LogError("MirrorCameraConfig: No Renderer component found in children.");
            return;
        }
        // Check if the object is within the camera's view
        // If the object is not in view, disable the mirror camera (optimization)
        if (!ObjectInCameraView())
        {
            mirrorCamera.enabled = false;
            return;
        }
        mirrorCamera.enabled = true;
        // Set the target texture of the mirror camera to the material's main texture
        mirrorRenderer.material.mainTexture = mirrorCamera.targetTexture;
    }

    private bool ObjectInCameraView()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }
}
