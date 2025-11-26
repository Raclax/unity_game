using UnityEngine;
using TMPro;
using System.IO; // IMPORTANT : Pour la gestion des fichiers !
using System;    // IMPORTANT : Pour obtenir la date et l'heure actuelles !

public class GestionnaireTimer : MonoBehaviour
{
    // public TextMeshProUGUI texteTimer; // On garde cette ligne commentée pour l'instant
    
    private float tempsEcoule;
    private bool timerEstActif = false;
    
    // Pour ne logger qu'une fois par seconde
    private float tempsDepuisDernierLog = 0f;

    void OnEnable()
    {
        tempsEcoule = 0f;
        timerEstActif = false;
        
        Debug.Log("Chronomètre prêt. En attente de lancement.");
    }

    public void LancerTimer()
    {
        if (!timerEstActif)
        {
            timerEstActif = true;
            Debug.Log("--- Chronomètre lancé ! ---");
        }
    }

    // C'est ici que la magie se produit
    public void ArreterTimer()
    {
        if (timerEstActif)
        {
            timerEstActif = false;
            
            // 1. On récupère le temps final formaté
            string tempsFinal = FormaterTemps(tempsEcoule);
            
            // 2. On l'affiche clairement dans la console
            Debug.Log("======================================");
            Debug.Log("TIMER ARRÊTÉ ! Temps final : " + tempsFinal);
            Debug.Log("======================================");
            
            // 3. On appelle la fonction pour sauvegarder ce temps dans un fichier
            SauvegarderTempsDansFichier(tempsFinal);
        }
    }

    void Update()
    {
        if (timerEstActif)
        {
            tempsEcoule += Time.deltaTime;
            tempsDepuisDernierLog += Time.deltaTime;
            
            if (tempsDepuisDernierLog >= 1f)
            {
                // On logue le temps qui passe dans la console
                Debug.Log("Temps : " + FormaterTemps(tempsEcoule));
                tempsDepuisDernierLog -= 1f;
            }
        }
    }

    // Cette fonction ne fait que formater, elle est plus réutilisable comme ça
    string FormaterTemps(float tempsAffiche)
    {
        float minutes = Mathf.FloorToInt(tempsAffiche / 60);
        float secondes = Mathf.FloorToInt(tempsAffiche % 60);
        return string.Format("{0:00}:{1:00}", minutes, secondes);
    }

    // NOUVELLE FONCTION : pour écrire dans le fichier
    void SauvegarderTempsDansFichier(string temps)
    {
        // On choisit un endroit sûr pour enregistrer le fichier.
        // Application.persistentDataPath fonctionne sur toutes les plateformes (PC, Mac, Quest...)
        string chemin = Path.Combine(Application.dataPath, "..", "resultats_timer.txt");
        
        try
        {
            // 'true' signifie qu'on ajoute à la fin du fichier (append) au lieu de l'écraser.
            using (StreamWriter writer = new StreamWriter(chemin, true))
            {
                // On crée une ligne de log complète avec la date et l'heure
                string ligneDeLog = $"Session du {DateTime.Now} - Temps final : {temps}";
                
                // On écrit la ligne dans le fichier
                writer.WriteLine(ligneDeLog);
            }
            
            // On affiche un message de confirmation avec le chemin du fichier pour le trouver facilement
            Debug.Log("Temps sauvegardé avec succès dans le fichier : " + chemin);
        }
        catch (Exception e)
        {
            // Si quelque chose se passe mal (ex: pas les permissions d'écrire), on affiche une erreur
            Debug.LogError("Échec de la sauvegarde du fichier : " + e.Message);
        }
    }
}