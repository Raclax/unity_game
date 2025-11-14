using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    private Renderer objectRenderer;
    [SerializeField] Color OnEmissionColor = new Color(1f, 0.0f, 1f);
    [SerializeField] Color OffEmissionColor = Color.black;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectRenderer = GetComponentInChildren<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("MaterialChange: No Renderer component found on this GameObject.");
            return;
        }
    }

    public void SetActiveMaterial(bool isActive)
    {
        if (objectRenderer == null) return;
        if (isActive)
        {
            objectRenderer.material.SetColor("_EmissionColor", OnEmissionColor);
        }
        else
        {
            objectRenderer.material.SetColor("_EmissionColor", OffEmissionColor);
        }
    }
}
