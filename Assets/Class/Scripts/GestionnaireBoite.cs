using UnityEngine;

public class GestionnaireBoites : MonoBehaviour
{
    // Liste de toutes les boîtes à surveiller
    public BoiteDetecteur[] toutesLesBoites;
    
    // Référence au gestionnaire de timer
    public GestionnaireTimer gestionnaireTimer;
    
    // Pour éviter d'arrêter le timer plusieurs fois
    private bool objectifComplete = false;
    
    public void VerifierSiToutesLesBoitesSontRemplies()
    {
        // Si l'objectif est déjà accompli, on ne fait rien
        if (objectifComplete) return;
        
        // On compte combien de boîtes sont remplies
        int nombreDeBoitesRemplies = 0;
        
        foreach (BoiteDetecteur boite in toutesLesBoites)
        {
            if (boite.EstRemplie())
            {
                nombreDeBoitesRemplies++;
            }
        }
        
        Debug.Log(nombreDeBoitesRemplies + " / " + toutesLesBoites.Length + " boîtes remplies");
        
        // Si toutes les boîtes sont remplies
        if (nombreDeBoitesRemplies == toutesLesBoites.Length)
        {
            objectifComplete = true;
            Debug.Log("TOUTES LES BOÎTES SONT REMPLIES ! Arrêt du timer.");
            
            // On arrête le timer
            if (gestionnaireTimer != null)
            {
                gestionnaireTimer.ArreterTimer();
            }
        }
    }
}