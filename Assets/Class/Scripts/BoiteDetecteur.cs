using UnityEngine;

public class BoiteDetecteur : MonoBehaviour
{
    // ... (toutes vos variables restent les mêmes)
    public Material materialNormal;
    public Material materialSurbrillance;
    private Renderer boiteRenderer;
    private bool contientObjet = false;
    
    void Start()
    {
        boiteRenderer = GetComponent<Renderer>();
        if (boiteRenderer != null && materialNormal != null)
        {
            boiteRenderer.material = materialNormal;
        }
        // La ligne FindObjectOfType n'est plus nécessaire si on enlève les appels directs
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && !contientObjet)
        {
            contientObjet = true;
            
            if (boiteRenderer != null && materialSurbrillance != null)
            {
                boiteRenderer.material = materialSurbrillance;
            }
            
            Debug.Log("Objet déposé dans : " + gameObject.name);
            
            // LIGNE SUPPRIMÉE : plus besoin de notifier, le gestionnaire vérifie en permanence
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && contientObjet)
        {
            contientObjet = false;
            
            if (boiteRenderer != null && materialNormal != null)
            {
                boiteRenderer.material = materialNormal;
            }
            
            Debug.Log("Objet retiré de : " + gameObject.name);
            
            // LIGNE SUPPRIMÉE : plus besoin de notifier
        }
    }
    
    public bool EstRemplie()
    {
        return contientObjet;
    }
}