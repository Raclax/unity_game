using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeurDeScene : MonoBehaviour
{
    public void ChargerScene1(string nomDeLaScene)
    {
        Debug.Log("Tentative de chargement de : " + nomDeLaScene);
        
        SceneManager.LoadScene(1);
    }
        public void ChargerScene2(string nomDeLaScene)
    {
        Debug.Log("Tentative de chargement de : " + nomDeLaScene);
        
        SceneManager.LoadScene(0);
    }
}