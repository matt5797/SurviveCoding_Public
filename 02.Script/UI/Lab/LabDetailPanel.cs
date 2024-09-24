using System.Collections;
using System.Collections.Generic;
using SurviveCoding.LabSystem;
using TMPro;
using UnityEngine;

namespace SurviveCoding.UI.Lab
{
    public class LabDetailPanel : MonoBehaviour
    {
        [SerializeField]
        private LabSystem.Lab targetLab;

        [SerializeField]
        private TMP_Text labNameText;
        
        [SerializeField]
        private TMP_Text energyConsumptionText;

        [SerializeField]
        private LabTagIcon energyEfficiencyIcon;

        [SerializeField]
        private LabTagIcon pollutionLevelIcon;

        [SerializeField]
        private LabTagIcon productionEfficiencyIcon;

        [SerializeField]
        private LabTagIcon durabilityIcon;

        [SerializeField]
        private LabUpgradePanel upgradePanel;

        [SerializeField]
        private LabProductionPanel productionPanel;

        [SerializeField]
        private LabStatePanel statePanel;

        private void OnEnable()
        {
            if (targetLab != null)
            {
                UpdateLabDetail();
            }
        }

        public void SetTargetLab(LabSystem.Lab lab)
        {
            targetLab = lab;
            UpdateLabDetail();
        }

        private void UpdateLabDetail()
        {
            if (targetLab != null)
            {
                labNameText.text = $"{targetLab.LabName} Lv.{(int)targetLab.Tags.SecurityLevel}";
                energyConsumptionText.text = targetLab.StatComponent.GetStat(LabStatType.EnergyConsumption).CurrentValue.ToString();

                energyEfficiencyIcon.SetTargetLab(targetLab);
                pollutionLevelIcon.SetTargetLab(targetLab);
                productionEfficiencyIcon.SetTargetLab(targetLab);
                durabilityIcon.SetTargetLab(targetLab);

                upgradePanel.SetTargetLab(targetLab);
                productionPanel.SetTargetLab(targetLab);

                statePanel.SetTargetLab(targetLab);
            }
        }
    }
}