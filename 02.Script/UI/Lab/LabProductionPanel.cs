using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SurviveCoding.LabSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

namespace SurviveCoding.UI.Lab
{
    public class LabProductionPanel : MonoBehaviour
    {
        [SerializeField]
        private LabSystem.Lab targetLab;

        [SerializeField]
        private ProductionItemLine[] productionItemLines;

        [SerializeField]
        private Image[] producingItemImages;

        [SerializeField]
        private Slider productionProgressSlider;

        private void OnEnable()
        {
            if (targetLab != null)
            {
                UpdateProductionPanel();
                StartCoroutine(UpdateProduceState());
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void SetTargetLab(LabSystem.Lab lab)
        {
            targetLab = lab;
            UpdateProductionPanel();
        }
        
        public void UpdateProductionPanel()
        {
            ClearProductionItemLines();
            ClearProducingItemImages();

            if (targetLab != null)
            {
                LabProductionComponent productionComponent = targetLab.ProductionComponent;
                LabUpgradeComponent upgradeComponent = targetLab.UpgradeComponent;

                for (int i = 0; i < 3; i++)
                {
                    LabUpgradeData.UpgradeLevelData levelData = upgradeComponent.GetSelectedLevelData(i + 1);
                    if (levelData != null && i < productionItemLines.Length)
                    {
                        Sprite[] itemSprites = levelData.productionDataList
                            .Select(productionData => productionData.item.Icon)
                            .ToArray();
                        productionItemLines[i].SetItemSprites(itemSprites);
                        productionItemLines[i].ProductionLevel = i + 1;
                    }
                }

                int producingItemIndex = 0;
                productionComponent.SelectedLevelData.productionDataList.ForEach(productionData =>
                {
                    if (producingItemIndex < producingItemImages.Length)
                    {
                        producingItemImages[producingItemIndex].sprite = productionData.item.Icon;
                        producingItemImages[producingItemIndex].gameObject.SetActive(true);
                        producingItemIndex++;
                    }
                });

                UpdateProductionButton();
            }
        }

        private void ClearProductionItemLines()
        {
            foreach (var productionItemLine in productionItemLines)
            {
                productionItemLine.Clear();
            }
        }

        private void ClearProducingItemImages()
        {
            foreach (var itemImage in producingItemImages)
            {
                itemImage.sprite = null;
                itemImage.gameObject.SetActive(false);
            }
        }

        private void UpdateProductionButton()
        {
            if (targetLab != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i >= targetLab.UpgradeComponent.CurrentLevel)
                    {
                        productionItemLines[i].SetProduceButtonIcon(ProduceState.CannotProduce);
                    }
                    else if (productionItemLines[i].ProductionLevel == targetLab.ProductionComponent.SelectedProductionLevel)
                    {
                        productionItemLines[i].SetProduceButtonIcon(ProduceState.Producing);
                    }
                    else
                    {
                        productionItemLines[i].SetProduceButtonIcon(ProduceState.NotProducing);
                    }
                }
            }
        }

        private IEnumerator UpdateProduceState()
        {
            while (true)
            {
                if (targetLab != null)
                {
                    productionProgressSlider.value = targetLab.ProductionComponent.GetProductionProgress();
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        public void OnProduceButtonClicked(int level)
        {
            if (targetLab != null && level <= targetLab.UpgradeComponent.CurrentLevel)
            {
                targetLab.ProductionComponent.SetSelectedProductionLevel(level);
                UpdateProductionPanel();
            }
        }
    }
}