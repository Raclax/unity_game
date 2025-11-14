using UnityEngine;

public class MatchTransform : MonoBehaviour
{
    [SerializeField] Transform targetTransform;

    void Start()
    {
        if (targetTransform == null)
        {
            targetTransform = GameObject.FindAnyObjectByType<OVRManager>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
        }
    }
}
