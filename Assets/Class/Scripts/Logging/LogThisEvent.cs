using UnityEngine;

public class LogThisEvent : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10.0f)]
    float Cooldown = 0f; // Frequency in seconds

    public string MessageToLog = "";
    private float timer = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0.0f;
        if (string.IsNullOrEmpty(MessageToLog))
        {
            MessageToLog = "Event from " + gameObject.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }
    }

    public void LogEvent()
    {
        if (timer <= 0f)
        {
            LogsManager.Instance.LogEvent(MessageToLog);
            timer = Cooldown;
        }
    }
}
