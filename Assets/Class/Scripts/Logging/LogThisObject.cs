using UnityEngine;

public class LogThisObject : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10.0f)]
    float LoggingPeriod= 1.0f; // Frequency in seconds
    [SerializeField] string Label = "";
    private float timer = 0.0f;

    void Start()
    {
        timer = 0.0f;
        if (string.IsNullOrEmpty(Label))
        {
            Label = gameObject.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= LoggingPeriod)
        {
            LogsManager.Instance.LogTransform(gameObject.transform, Label);
            timer = 0.0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
