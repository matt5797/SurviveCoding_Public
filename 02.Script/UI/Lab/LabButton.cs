using SurviveCoding.LabSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SurviveCoding.UI.Lab
{
    public class LabButton : MonoBehaviour
    {
        private LabSystem.Lab targetLab;
        
        [SerializeField]
        private TMP_Text labNameText;

        [SerializeField]
        private Button selectButton;

        [SerializeField]
        private Button activateButton;

        [SerializeField]
        private Image HeaderIcon;

        [SerializeField]
        private Image activateIcon;

        [SerializeField]
        private Image deactivateIcon;

        [SerializeField]
        private Sprite bioIcon;
        [SerializeField]
        private Sprite techIcon;
        [SerializeField]
        private Sprite enviroIcon;
        [SerializeField]
        private Sprite energyIcon;

        public event System.Action<LabSystem.Lab> OnLabSelected;

        private void Awake()
        {
            selectButton.onClick.AddListener(OnSelectButtonClicked);
            activateButton.onClick.AddListener(OnActivateButtonClicked);
        }

        public void SetLab(LabSystem.Lab lab)
        {
            targetLab = lab;
            labNameText.text = lab.LabName;

            if (targetLab.IsActive)
            {
                activateIcon.gameObject.SetActive(true);
                deactivateIcon.gameObject.SetActive(false);
            }
            else
            {
                activateIcon.gameObject.SetActive(false);
                deactivateIcon.gameObject.SetActive(true);
            }

            switch (lab.Tags.ResearchField)
            {
                case ResearchField.Bio:
                    HeaderIcon.sprite = bioIcon;
                    break;
                case ResearchField.Tech:
                    HeaderIcon.sprite = techIcon;
                    break;
                case ResearchField.Environment:
                    HeaderIcon.sprite = enviroIcon;
                    break;
                case ResearchField.Energy:
                    HeaderIcon.sprite = energyIcon;
                    break;
            }
        }

        private void OnSelectButtonClicked()
        {
            OnLabSelected?.Invoke(targetLab);
            SFX_Manager.Instance.SFX_Button();
        }

        private void OnActivateButtonClicked()
        {
            targetLab.IsActive = !targetLab.IsActive;
            if (targetLab.IsActive)
            {
                activateIcon.gameObject.SetActive(true);
                deactivateIcon.gameObject.SetActive(false);
            }
            else
            {
                activateIcon.gameObject.SetActive(false);
                deactivateIcon.gameObject.SetActive(true);
            }
            SFX_Manager.Instance.SFX_Button();
        }
    }
}