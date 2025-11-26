using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controls the steps in the in coaching card.
    /// </summary>
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

        public void Next()
        {
            if (m_CurrentStepIndex >= m_StepList.Count - 1)
            {
                // OUI, C'EST LE DERNIER PAS : on passe au timer !
                
                // 1. On cache le dernier pas d'information
                m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);
                
                // 2. On active notre page de timer
                m_PageTimer.SetActive(true);
                
                // 3. L'ÉCHANGE MAGIQUE : on cache le bouton "Continue"
                m_BoutonContinue.SetActive(false);
                
                // 4. ET ON MONTRE le bouton "Lancer le Timer"
                m_BoutonLancerTimer.SetActive(true);
            }
            else
            {
                // NON, CE N'EST PAS ENCORE LA FIN : on continue normalement
                m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);
                m_CurrentStepIndex++;
                m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
                m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].buttonText;
            }
        }
    }
}
