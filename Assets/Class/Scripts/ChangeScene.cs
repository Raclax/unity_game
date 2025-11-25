using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeurDeScene : MonoBehaviour
{
    public void ChargerScene(string nomDeLaScene)
    {
        Debug.Log("BOUTON CLIQUÉ !"); // Test simple
        Debug.Log("Tentative de chargement de : " + nomDeLaScene);
        
        // Test avec le numéro de scène au lieu du nom
        SceneManager.LoadScene(1); // Charge la scène qui est à l'index 1 dans Build Profiles
    }
}