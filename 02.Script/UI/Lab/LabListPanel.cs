using System.Collections.Generic;
using SurviveCoding.LabSystem;
using UnityEngine;
using UnityEngine.UI;

namespace SurviveCoding.UI.Lab
{
    using Lab = LabSystem.Lab;
    
    public class LabListPanel : MonoBehaviour
    {
        [SerializeField]
        private LabDetailPanel labDetailPanel;

        [SerializeField]
        private Transform labButtonContainer;

        [SerializeField]
        private LabButton labButtonPrefab;

        private List<LabButton> labButtons = new List<LabButton>();

        [SerializeField]
        private List<Lab> labs;

        private void Start()
        {
            GenerateLabButtons();
        }

        private void GenerateLabButtons()
        {
            foreach (var lab in labs)
            {
                LabButton labButton = Instantiate(labButtonPrefab, labButtonContainer);
                labButton.SetLab(lab);
                labButton.OnLabSelected += OnLabSelected;
                labButtons.Add(labButton);
            }
        }

        private void OnLabSelected(LabSystem.Lab selectedLab)
        {
            labDetailPanel.SetTargetLab(selectedLab);
        }
    }
}