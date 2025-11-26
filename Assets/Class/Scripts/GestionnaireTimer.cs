using UnityEngine;
// using TMPro; // On n'en a plus besoin, on peut le supprimer !

public class GestionnaireTimer : MonoBehaviour
{
    // public TextMeshProUGUI texteTimer; // SUPPRIMÉ : On ne touche plus à l'interface.

    private float tempsEcoule;
    private bool timerEstActif = false;
    
    // NOUVEAU : Une variable pour ne logger qu'une fois par seconde
    private float tempsDepuisDernierLog = 0f;

    void OnEnable()
    {
        tempsEcoule = 0f;
        timerEstActif = false;
        
        // On affiche la valeur de départ "00:00" dans la console
        LoguerTemps(tempsEcoule);
    }

    public void LancerTimer()
    {
        if (!timerEstActif)
        {
            timerEstActif = true;
            Debug.Log("--- Chronomètre lancé ! ---");
        }
    }

    void Update()
    {
        if (timerEstActif)
        {
            tempsEcoule += Time.deltaTime;
            tempsDepuisDernierLog += Time.deltaTime;
            
            // NOUVEAU : On ne logue que si une seconde s'est écoulée
            if (tempsDepuisDernierLog >= 1f)
            {
                LoguerTemps(tempsEcoule);
                tempsDepuisDernierLog -= 1f; // On retire une seconde au compteur
            }
        }
    }

    // RENOMMÉ : "AfficherTemps" est devenu "LoguerTemps" pour plus de clarté
    void LoguerTemps(float tempsAffiche)
    {
        float minutes = Mathf.FloorToInt(tempsAffiche / 60);
        float secondes = Mathf.FloorToInt(tempsAffiche % 60);

        string tempsFormate = string.Format("{0:00}:{1:00}", minutes, secondes);
        
        // MODIFIÉ : Au lieu de mettre à jour un texte, on écrit dans la console
        Debug.Log("Temps écoulé : " + tempsFormate);
    }
    // AJOUTEZ CETTE FONCTION à votre GestionnaireTimer
   // AJOUTEZ CETTE FONCTION à votre GestionnaireTimer
    public void ArreterTimer()
    {
        if (timerEstActif)
        {
            timerEstActif = false;
            Debug.Log("=== TIMER ARRÊTÉ ! Temps final : ===" );
            LoguerTemps(tempsEcoule);
        }
    }
}