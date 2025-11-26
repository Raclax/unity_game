using UnityEngine;

public class BoiteDetecteur : MonoBehaviour
{
    // Le Material que la boîte aura quand elle sera vide
    public Material materialNormal;
    
    // Le Material que la boîte aura quand elle contiendra un objet (surbrillance)
    public Material materialSurbrillance;
    
    // Le Renderer de la boîte (pour changer son apparence)
    private Renderer boiteRenderer;
    
    // État de la boîte : contient-elle un objet ?
    private bool contientObjet = false;
    
    // Référence au gestionnaire central (on le trouvera automatiquement)
    private GestionnaireBoites gestionnaire;
    
    void Start()
    {
        // On récupère le Renderer de cet objet
        boiteRenderer = GetComponent<Renderer>();
        
        // On applique le material normal au départ
        if (boiteRenderer != null && materialNormal != null)
        {
            boiteRenderer.material = materialNormal;
        }
        
        // On trouve le gestionnaire central dans la scène
        gestionnaire = FindObjectOfType<GestionnaireBoites>();
    }
    
    // Cette fonction est appelée quand un objet ENTRE dans le trigger
    void OnTriggerEnter(Collider other)
    {
        // On vérifie que c'est bien un objet déposable (par exemple avec un tag)
        // Pour l'instant, on accepte tout objet qui a un Rigidbody
        if (other.GetComponent<Rigidbody>() != null && !contientObjet)
        {
            contientObjet = true;
            
            // On change l'apparence de la boîte
            if (boiteRenderer != null && materialSurbrillance != null)
            {
                boiteRenderer.material = materialSurbrillance;
            }
            
            Debug.Log("Objet déposé dans : " + gameObject.name);
            
            // On informe le gestionnaire central
            if (gestionnaire != null)
            {
                gestionnaire.VerifierSiToutesLesBoitesSontRemplies();
            }
        }
    }
    
    // OPTIONNEL : Si vous voulez détecter quand l'objet sort de la boîte
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && contientObjet)
        {
            contientObjet = false;
            
            // On remet l'apparence normale
            if (boiteRenderer != null && materialNormal != null)
            {
                boiteRenderer.material = materialNormal;
            }
            
            Debug.Log("Objet retiré de : " + gameObject.name);
            
            // On informe le gestionnaire
            if (gestionnaire != null)
            {
                gestionnaire.VerifierSiToutesLesBoitesSontRemplies();
            }
        }
    }
    
    // Fonction pour que le gestionnaire puisse vérifier l'état
    public bool EstRemplie()
    {
        return contientObjet;
    }
}