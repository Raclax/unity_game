using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Unity.VRTemplate
{
    public class StepManager : MonoBehaviour
    {
        [Serializable]
        class Step
        {
            [SerializeField]
            public GameObject stepObject;

            [SerializeField]
            public string buttonText;
        }

        [SerializeField]
        public TextMeshProUGUI m_StepButtonTextField;

        [SerializeField]
        List<Step> m_StepList = new List<Step>();

        [SerializeField]
        public GameObject m_PageTimer; 

        [SerializeField]
        public GameObject m_BoutonContinue;

        [SerializeField]
        public GameObject m_BoutonLancerTimer;
        
        int m_CurrentStepIndex = 0;

        // --- DÉBUT DE LA MÉTHODE MODIFIÉE ---
        public void Next()
        {
            // D'abord, on désactive le panneau actuel
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);

            // CAS SPÉCIAL : Si on est sur le 7ème panneau (qui a l'index 6)
            if (m_CurrentStepIndex == 6)
            {
                // On remet l'index à 0 pour retourner au premier panneau
                m_CurrentStepIndex = 0;
            }
            // Si on est sur le dernier panneau de la liste (et que ce n'est pas le 7ème)
            else if (m_CurrentStepIndex >= m_StepList.Count - 1)
            {
                // On active la page du timer et on gère les boutons de fin
                m_PageTimer.SetActive(true);
                m_BoutonContinue.SetActive(false);
                m_BoutonLancerTimer.SetActive(true);

                // On sort de la fonction ici pour ne pas activer un nouveau panneau
                return; 
            }
            // Pour tous les autres cas (panneaux 1 à 6, et après le 7ème si la liste est plus longue)
            else
            {
                // On passe simplement au panneau suivant
                m_CurrentStepIndex++;
            }

            // On active le nouveau panneau (soit le suivant, soit le premier) et on met à jour le texte
            m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
            m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].buttonText;
        }
        // --- FIN DE LA MÉTHODE MODIFIÉE ---
    }
}
