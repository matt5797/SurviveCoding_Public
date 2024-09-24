using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.Events;
using SurviveCoding.External.InventoryEngine.Item;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// ������(Lab)�� ���� ����� �����ϴ� ������Ʈ
    /// ���׷��̵� �ܰ躰 �����Ϳ� ���� ��ǰ�� �����ϴ� ����� ����մϴ�.
    /// </summary>
    public class LabProductionComponent : MonoBehaviour
    {
        private Lab lab;
        private LabStatComponent statComponent;
        private LabUpgradeComponent upgradeComponent;
        private LabUpgradeData.UpgradeLevelData selectedLevelData;

        /// <summary>
        /// ���õ� ���� ����
        /// </summary>
        private int selectedProductionLevel = 0;
        private float currentProductionTime = 0f;

        /// <summary>
        /// ����� �������� �߰��� �κ��丮
        /// </summary>
        private MoreMountains.InventoryEngine.Inventory targetInventory;
        
        /// <summary>
        /// ������ ���� ���� �� �߻��ϴ� �̺�Ʈ
        /// </summary>
        public UnityEvent OnProductionStarted;

        /// <summary>
        /// ������ ���� �Ϸ� �� �߻��ϴ� �̺�Ʈ
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
        /// ���� ���� ������ ������Ʈ�ϴ� �޼���
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
        /// �������� �����ϴ� �޼���
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
                    Debug.Log($"{productionData.item.name} {producedAmount}�� ���� �Ϸ�");
                }
                else
                {
                    Debug.LogWarning("Target inventory is not assigned.");
                }
            }

            OnProductionCompleted.Invoke();
        }

        /// <summary>
        /// ���׷��̵� ���� �� ȣ��Ǵ� �޼���
        /// </summary>
        /// <param name="newLevel">���ο� ���׷��̵� ����</param>
        private void OnUpgradeSucceeded(int newLevel)
        {
            // ���׷��̵� ���� �� ó���� ���� �߰�
            // ��: ���� ������ ��ǰ ��� ���� ��
        }

        /// <summary>
        /// ���õ� ���� �����͸� ������Ʈ�ϴ� �޼���
        /// </summary>
        private void UpdateSelectedLevelData()
        {
            selectedLevelData = upgradeComponent.GetSelectedLevelData(selectedProductionLevel);
        }

        /// <summary>
        /// ���õ� ���� ������ �����ϴ� �޼���
        /// </summary>
        /// <param name="level">���� ����</param>
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