using UnityEngine;

public class Interaction : MonoBehaviour
{


    GameObject popup;


    void Start()
    {
        popup = transform.Find("Popup").gameObject;
        popup.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
    if(other.tag == "character") {
        popup.SetActive(true);
    }
    }

    void OnTriggerExit(Collider other)
    {
    if(other.tag == "character") {
        popup.SetActive(false);
    }
    }
}