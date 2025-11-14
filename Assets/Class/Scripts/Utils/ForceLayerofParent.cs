using UnityEngine;

public class ForceLayerOfParent : MonoBehaviour
{
    float count = 0;
    [SerializeField] float Delay = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //wait Delay seconds before getting children
        if (count < Delay)
        {
            count += Time.deltaTime;
            return;
        }
        
        // Apply layer to all children and sub-children recursively
        SetLayerRecursively(transform, gameObject.layer);
    }
    
    /// <summary>
    /// Recursively sets the layer for all children and sub-children
    /// </summary>
    /// <param name="parent">The parent transform to start from</param>
    /// <param name="layer">The layer to apply</param>
    void SetLayerRecursively(Transform parent, int layer)
    {
        foreach (Transform child in parent)
        {
            if (child != null)
            {
                child.gameObject.layer = layer;
                // Recursively apply to sub-children
                SetLayerRecursively(child, layer);
            }
        }
    }
}