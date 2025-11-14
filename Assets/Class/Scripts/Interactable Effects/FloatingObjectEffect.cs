using UnityEngine;

public class FloatingObjectEffect : MonoBehaviour
{
    public float rotationSpeed = 10f; // Degrees per second
    public float floatingAmplitude = 0.5f; // Amplitude of the floating effect
    public float floatingFrequency = 1f; // Frequency of the floating effect
    private bool Enabled = true;
    private Vector3 rotationAxis = Vector3.up; // Default rotation axis is Y-axis

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;
        rotate(rotationAxis, rotationSpeed);
        FloatingEffect(Vector3.up, floatingAmplitude, floatingFrequency);
    }

    void rotate(Vector3 axis, float speed)
    {
        transform.Rotate(axis, speed * Time.deltaTime);
    }

    void FloatingEffect(Vector3 axis, float amplitude, float frequency)
    {
        float offset = amplitude * Mathf.Sin(Time.time * frequency);
        transform.position += axis * offset * Time.deltaTime;
    }

    public void ToggleRotation()
    {
        Enabled = !Enabled;
    }
}
