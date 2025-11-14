using System;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class LogEventData
{
    public long timestamp;
    public string eventMessage;
}

[System.Serializable]
public class Vector3Data
{
    public float x, y, z;
    
    public Vector3Data(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }
}

[System.Serializable]
public class LogTransformData
{
    public long timestamp;
    public string label;
    public Vector3Data position;
    public Vector3Data rotation;
    public Vector3Data scale;
}

public class LogsManager : Singleton<LogsManager>
{
    [SerializeField] bool isLogging = true;
    private string directory;
    private string logFilePath;
    private string filename;
    private Queue logQueue = new Queue();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupDirectory();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isLogging) return;
        
        while (logQueue.Count > 0)
        {
            var logEntry = logQueue.Dequeue();
            System.IO.File.AppendAllText(logFilePath, logEntry + ",\n");
        }
    }

    public void LogEvent(string message)
    {
        try
        {
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            LogEventData logData = new LogEventData
            {
                timestamp = timestamp,
                eventMessage = message
            };
            string logEntry = JsonUtility.ToJson(logData);
            logQueue.Enqueue(logEntry);
        }
        catch (Exception e)
        {
            Debug.LogError("[LogsManager] Failed to log event: " + e.Message);
        }

    }

    public void LogTransform(Transform transform, string label = "")
    {
        try
        {
            // Timestamp is in seconds since the Unix epoch
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            LogTransformData logData = new LogTransformData
            {
                timestamp = timestamp,
                label = label == "" ? transform.name : label,
                position = new Vector3Data(transform.position),
                rotation = new Vector3Data(transform.rotation.eulerAngles),
                scale = new Vector3Data(transform.localScale)
            };
            string logEntry = JsonUtility.ToJson(logData);
            logQueue.Enqueue(logEntry);
        }
        catch (Exception e)
        {
            Debug.LogError("[LogsManager] Failed to log transform of label " + label + ": " + e.Message);
        }

    }

    private void SetupDirectory()
    {
        try
        {
            directory = System.IO.Path.Combine(Application.persistentDataPath, "logs");
            // string directoryPath = System.IO.Path.GetDirectoryName(directory);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("[LogsManager] Failed to create log directory: " + e.Message);
        }

        try
        {
            filename = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + "_log.json";
            logFilePath = System.IO.Path.Combine(directory, filename);
            // Create the log file at the start
            System.IO.File.WriteAllText(logFilePath, "[\n");
            Debug.Log("[LogsManager] Start Logging to " + logFilePath);
            // The JSON array will be closed in OnDestroy/OnApplicationQuit
        }
        catch (Exception e)
        {
            Debug.LogError("[LogsManager] Failed to create log directory: " + e.Message);
        }
    }

    private void OnDestroy()
    {
        if (logQueue != null)
        {
            // Close the JSON array
            System.IO.File.AppendAllText(logFilePath, "]");
            logQueue.Clear();
            logQueue = null;
            Debug.Log("[LogsManager] Disabled, Saving to: " + logFilePath);
        }
    }

    private void OnApplicationQuit()
    {
        System.IO.File.AppendAllText(logFilePath, "]");
        // System.IO.File.AppendAllText(logFilePath, "Log End\n");
        logQueue.Clear();
        logQueue = null;
        Debug.Log("[LogsManager] Application Quit, Saving to:" + logFilePath);
    }
}
