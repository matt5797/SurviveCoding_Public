using UnityEngine;
using System;
using System.Collections.Generic;
using SurviveCoding.BuffSystem;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// �������� ���׷��̵� ����� �����ϴ� ������Ʈ
    /// ���׷��̵� �����͸� ������� ���׷��̵� ���� Ȯ��, ���׷��̵� ���� ���� ó���մϴ�.
    /// </summary>
    public class LabUpgradeComponent : MonoBehaviour
    {
        /// <summary>
        /// ���׷��̵� ������
        /// </summary>
        [SerializeField]
        private LabUpgradeData upgradeData;

        /// <summary>
        /// ���� ���׷��̵� ����
        /// </summary>
        private int currentLevel = 0;
        
        /// <summary>
        /// ���׷��̵� ���� �� �߻��ϴ� �̺�Ʈ
        /// </summary>
        public event Action<int> OnUpgradeSucceeded;

        /// <summary>
        /// ���� ���׷��̵� ������ ��ȯ�ϴ� ������Ƽ
        /// </summary>
        public int CurrentLevel => currentLevel;

        /// <summary>
        /// ���׷��̵� �õ� �޼���
        /// </summary>
        public void AttemptUpgrade()
        {
            if (CanUpgrade())
            {
                PerformUpgrade();
            }
            else
            {
                Debug.Log("���׷��̵� ������ �������� �ʾҽ��ϴ�.");
            }
        }

        /// <summary>
        /// ���׷��̵� ���� ���θ� Ȯ���ϴ� �޼���
        /// </summary>
        /// <returns>���׷��̵� ���� ����</returns>
        private bool CanUpgrade()
        {
            LabUpgradeData.UpgradeLevelData nextLevelData = upgradeData.GetUpgradeLevelData(currentLevel + 1);

            if (nextLevelData == null)
            {
                Debug.Log("�� �̻� ���׷��̵��� �� �����ϴ�.");
                return false;
            }

            foreach (LabUpgradeData.RequiredMaterial requiredMaterial in nextLevelData.requiredMaterials)
            {
                var targetInventory = requiredMaterial.item.TargetInventory("Player1");
                int itemCount = targetInventory.GetQuantity(requiredMaterial.item.ItemID);
                if (itemCount < requiredMaterial.amount)
                {
                    Debug.Log($"�ʿ��� ��ᰡ �����մϴ�. {requiredMaterial.item.ItemName} �ʿ� ����: {requiredMaterial.amount}, ���� ����: {itemCount}");
                    return false;
                }
            }

            foreach (LabUpgradeData.UpgradeCondition upgradeCondition in nextLevelData.upgradeConditions)
            {
                // ���׷��̵� ���� ���� ���� Ȯ�� ���� �߰�
                // ������ �������� ���� ��� false ��ȯ
            }

            return true;
        }

        /// <summary>
        /// ���׷��̵带 �����ϴ� �޼���
        /// </summary>
        private void PerformUpgrade()
        {
            LabUpgradeData.UpgradeLevelData nextLevelData = upgradeData.GetUpgradeLevelData(currentLevel + 1);

            foreach (LabUpgradeData.RequiredMaterial requiredMaterial in nextLevelData.requiredMaterials)
            {
                var targetInventory = requiredMaterial.item.TargetInventory("Player1");
                targetInventory.RemoveItemByID(requiredMaterial.item.ItemID, requiredMaterial.amount);
            }

            currentLevel++;

            OnUpgradeSucceeded?.Invoke(currentLevel);
        }

        /// <summary>
        /// ���� ������ ���׷��̵� �����͸� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <returns>���� ������ ���׷��̵� ������</returns>
        public LabUpgradeData.UpgradeLevelData GetCurrentLevelData()
        {
            return upgradeData.GetUpgradeLevelData(currentLevel);
        }

        /// <summary>
        /// ������ ������ ���׷��̵� �����͸� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <param name="selectedLevel">������ ����</param>
        /// <returns>������ ������ ���׷��̵� ������</returns>
        public LabUpgradeData.UpgradeLevelData GetSelectedLevelData(int selectedLevel)
        {
            return upgradeData.GetUpgradeLevelData(selectedLevel);
        }
    }
}