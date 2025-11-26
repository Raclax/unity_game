using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeurDeScene : MonoBehaviour
{
    public void ChargerScene(string nomDeLaScene)
    {
        Debug.Log("BOUTON CLIQUÉ !"); // Test simple
        Debug.Log("Tentative de chargement de : " + nomDeLaScene);
        
        SceneManager.LoadScene(1);
    }
}