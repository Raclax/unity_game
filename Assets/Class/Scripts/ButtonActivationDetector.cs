using UnityEngine;

public class ButtonActivationDetector : MonoBehaviour
{
    [SerializeField] private Transform buttonTransform;
    [SerializeField] private float activationThreshold = 0.06f; // Distance threshold
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonTransform == null)
        {
            Debug.LogError("ButtonActivationDetector: Button Transform is not assigned.");
            return;
        }

        // Check if the button is pressed (moved downwards beyond the threshold)
        if (buttonTransform.localPosition.y <= -activationThreshold)
        {
            Debug.Log("Button Pressed");
        }
    }
}
