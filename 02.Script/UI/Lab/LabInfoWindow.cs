using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.UI.Lab
{
    using Lab = LabSystem.Lab;
    
    public class LabInfoWindow : MonoBehaviour
    {
        [SerializeField]
        private LabDetailPanel labDetailPanel;

        [SerializeField]
        private Lab currentLab;

        private void OnEnable()
        {
            if (currentLab != null)
            {
                ShowLabInfo(currentLab);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HideLabInfo();
            }
        }

        public void ShowLabInfo()
        {
            ShowLabInfo(currentLab);
        }

        public void ShowLabInfo(Lab lab)
        {
            currentLab = lab;
            labDetailPanel.SetTargetLab(lab);
            gameObject.SetActive(true);
        }

        public void HideLabInfo()
        {
            // currentLab = null;
            gameObject.SetActive(false);
        }
    }
}