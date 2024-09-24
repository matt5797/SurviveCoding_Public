using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SurviveCoding.LabSystem;
using System.Collections.Generic;

namespace SurviveCoding.UI.Lab
{
    public class LabUpgradePanel : MonoBehaviour
    {
        [SerializeField]
        private LabSystem.Lab targetLab;
        
        [SerializeField]
        private List<Image> requiredMaterialIcons = new List<Image>();

        [SerializeField]
        private List<TMP_Text> requiredMaterialAmounts = new List<TMP_Text>();

        [SerializeField]
        private Button upgradeButton;

        private void OnEnable()
        {
            if (targetLab != null)
            {
                UpdateLabUpgradeInfo();
            }
        }

        public void SetTargetLab(LabSystem.Lab lab)
        {
            targetLab = lab;
            UpdateLabUpgradeInfo();
        }

        private void UpdateLabUpgradeInfo()
        {
            if (targetLab != null)
            {
                LabUpgradeComponent upgradeComponent = targetLab.UpgradeComponent;
                int currentLevel = upgradeComponent.CurrentLevel;
                int nextLevel = currentLevel + 1;

                LabUpgradeData.UpgradeLevelData nextLevelData = upgradeComponent.GetSelectedLevelData(nextLevel);

                if (nextLevelData != null)
                {
                    List<LabUpgradeData.RequiredMaterial> requiredMaterials = nextLevelData.requiredMaterials;

                    for (int i = 0; i < requiredMaterialIcons.Count; i++)
                    {
                        if (i < requiredMaterials.Count)
                        {
                            requiredMaterialIcons[i].sprite = requiredMaterials[i].item.Icon;
                            // 보유 수
                            int currentAmount = requiredMaterials[i].item.Quantity;
                            // 필요 수
                            int requiredAmount = requiredMaterials[i].amount;
                            requiredMaterialAmounts[i].text = $"{currentAmount}/{requiredAmount}";
                            requiredMaterialIcons[i].gameObject.SetActive(true);
                            requiredMaterialAmounts[i].gameObject.SetActive(true);
                        }
                        else
                        {
                            requiredMaterialIcons[i].gameObject.SetActive(false);
                            requiredMaterialAmounts[i].gameObject.SetActive(false);
                        }
                    }

                    upgradeButton.interactable = true;
                }
                else
                {
                    foreach (Image icon in requiredMaterialIcons)
                    {
                        icon.gameObject.SetActive(false);
                    }

                    foreach (TMP_Text amount in requiredMaterialAmounts)
                    {
                        amount.gameObject.SetActive(false);
                    }

                    upgradeButton.interactable = false;
                }
            }
        }

        public void OnUpgradeButtonClicked()
        {
            if (targetLab != null)
            {
                targetLab.AttemptUpgrade();
                UpdateLabUpgradeInfo();
                SFX_Manager.Instance.SFX_Button();
            }
        }
    }
}