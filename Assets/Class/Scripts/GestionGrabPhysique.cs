using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // NÉCESSAIRE pour accéder aux événements XR

public class GestionGrabPhysique : MonoBehaviour
{
    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void Start()
    {
        // 1. État Initial FIXE (Priorité absolue)
        // L'objet est Kinematic par défaut, ignoré par la gravité.
        rb.isKinematic = true;
    }

    // Cette méthode est appelée quand l'objet est SÉLECTIONNÉ (commence à être grab)
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // 2. Pendant le Grab : Le XR Toolkit le gère. 
        // Il le passe Kinematic pour le coller à la main de l'utilisateur.
    }

    // Cette méthode est appelée quand l'objet est DÉSÉLECTIONNÉ (relâché)
    private void OnSelectExited(SelectExitEventArgs args)
    {
        // 3. Après le Relâchement : Le XR Toolkit le passe Non-Kinematic.
        // C'est ici que la gravité reprend le contrôle et que l'objet tombe avec velocity.
        
        // Optionnel mais important pour les objets lourds : 
        // Forcez le mode physique si le Toolkit ne l'a pas fait instantanément.
        rb.isKinematic = false; 
    }
    
    // Assurez-vous d'attacher et détacher les événements du XR Grab Interactable
    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }
    
    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }
}