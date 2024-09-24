using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.Events;
using SurviveCoding.External.InventoryEngine.Item;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// 연구실(Lab)의 생산 기능을 관리하는 컴포넌트
    /// 업그레이드 단계별 데이터에 따라 물품을 생산하는 기능을 담당합니다.
    /// </summary>
    public class LabProductionComponent : MonoBehaviour
    {
        private Lab lab;
        private LabStatComponent statComponent;
        private LabUpgradeComponent upgradeComponent;
        private LabUpgradeData.UpgradeLevelData selectedLevelData;

        /// <summary>
        /// 선택된 생산 레벨
        /// </summary>
        private int selectedProductionLevel = 0;
        private float currentProductionTime = 0f;

        /// <summary>
        /// 생산된 아이템을 추가할 인벤토리
        /// </summary>
        private MoreMountains.InventoryEngine.Inventory targetInventory;
        
        /// <summary>
        /// 아이템 생산 시작 시 발생하는 이벤트
        /// </summary>
        public UnityEvent OnProductionStarted;

        /// <summary>
        /// 아이템 생산 완료 시 발생하는 이벤트
        /// </summary>
        public UnityEvent OnProductionCompleted;

        public int SelectedProductionLevel => selectedProductionLevel;

        public LabUpgradeData.UpgradeLevelData SelectedLevelData => selectedLevelData;

        private void Awake()
        {
            lab = GetComponent<Lab>();
            upgradeComponent = lab.UpgradeComponent;
            statComponent = lab.StatComponent;
        }

        private void OnEnable()
        {
            upgradeComponent.OnUpgradeSucceeded += OnUpgradeSucceeded;
        }

        private void Start()
        {
            selectedProductionLevel = upgradeComponent.CurrentLevel;
            UpdateSelectedLevelData();
        }

        private void OnDisable()
        {
            upgradeComponent.OnUpgradeSucceeded -= OnUpgradeSucceeded;
        }

        private void Update()
        {
            UpdateProduction();
        }

        /// <summary>
        /// 생산 관련 로직을 업데이트하는 메서드
        /// </summary>
        public void UpdateProduction()
        {
            if (lab.IsActive && selectedLevelData != null)
            {
                currentProductionTime += Time.deltaTime;

                if (currentProductionTime >= selectedLevelData.productionTime)
                {
                    ProduceItems();
                    currentProductionTime = 0f;
                }
            }
            else
            {
                currentProductionTime = 0f;
            }
        }

        /// <summary>
        /// 아이템을 생산하는 메서드
        /// </summary>
        private void ProduceItems()
        {
            OnProductionStarted.Invoke();
            
            foreach (var productionData in selectedLevelData.productionDataList)
            {
                float productionEfficiency = statComponent.GetStat(LabStatType.ProductionEfficiency).CalculateValue();
                float materialProductionEfficiency = statComponent.GetStat(LabStatType.MaterialProductionEfficiency).CalculateValue();
                float rareMaterialProductionEfficiency = statComponent.GetStat(LabStatType.RareMaterialProductionEfficiency).CalculateValue();

                float effectiveEfficiency = productionEfficiency;

                if (productionData.item is RareMaterialItem)
                {
                    effectiveEfficiency = rareMaterialProductionEfficiency;
                }
                else if (productionData.item is MaterialItem)
                {
                    effectiveEfficiency *= materialProductionEfficiency;
                }

                int producedAmount = Mathf.RoundToInt(productionData.amount * effectiveEfficiency);

                targetInventory = MoreMountains.InventoryEngine.Inventory.FindInventory(productionData.item.TargetInventoryName, "Player1");

                if (targetInventory != null)
                {
                    targetInventory.AddItem(productionData.item, producedAmount);
                    Debug.Log($"{productionData.item.name} {producedAmount}개 생산 완료");
                }
                else
                {
                    Debug.LogWarning("Target inventory is not assigned.");
                }
            }

            OnProductionCompleted.Invoke();
        }

        /// <summary>
        /// 업그레이드 성공 시 호출되는 메서드
        /// </summary>
        /// <param name="newLevel">새로운 업그레이드 레벨</param>
        private void OnUpgradeSucceeded(int newLevel)
        {
            // 업그레이드 성공 시 처리할 로직 추가
            // 예: 생산 가능한 물품 목록 갱신 등
        }

        /// <summary>
        /// 선택된 레벨 데이터를 업데이트하는 메서드
        /// </summary>
        private void UpdateSelectedLevelData()
        {
            selectedLevelData = upgradeComponent.GetSelectedLevelData(selectedProductionLevel);
        }

        /// <summary>
        /// 선택된 생산 레벨을 설정하는 메서드
        /// </summary>
        /// <param name="level">생산 레벨</param>
        public void SetSelectedProductionLevel(int level)
        {
            if (level >= 0 && level <= upgradeComponent.CurrentLevel)
            {
                selectedProductionLevel = level;
                UpdateSelectedLevelData();
            }
        }

        public float GetProductionProgress()
        {
            return selectedLevelData != null ? currentProductionTime / selectedLevelData.productionTime : 0f;
        }
    }
}