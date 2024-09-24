using UnityEngine;
using System;
using System.Collections.Generic;
using SurviveCoding.BuffSystem;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// 연구실의 업그레이드 기능을 관리하는 컴포넌트
    /// 업그레이드 데이터를 기반으로 업그레이드 조건 확인, 업그레이드 수행 등을 처리합니다.
    /// </summary>
    public class LabUpgradeComponent : MonoBehaviour
    {
        /// <summary>
        /// 업그레이드 데이터
        /// </summary>
        [SerializeField]
        private LabUpgradeData upgradeData;

        /// <summary>
        /// 현재 업그레이드 레벨
        /// </summary>
        private int currentLevel = 0;
        
        /// <summary>
        /// 업그레이드 성공 시 발생하는 이벤트
        /// </summary>
        public event Action<int> OnUpgradeSucceeded;

        /// <summary>
        /// 현재 업그레이드 레벨을 반환하는 프로퍼티
        /// </summary>
        public int CurrentLevel => currentLevel;

        /// <summary>
        /// 업그레이드 시도 메서드
        /// </summary>
        public void AttemptUpgrade()
        {
            if (CanUpgrade())
            {
                PerformUpgrade();
            }
            else
            {
                Debug.Log("업그레이드 조건이 충족되지 않았습니다.");
            }
        }

        /// <summary>
        /// 업그레이드 가능 여부를 확인하는 메서드
        /// </summary>
        /// <returns>업그레이드 가능 여부</returns>
        private bool CanUpgrade()
        {
            LabUpgradeData.UpgradeLevelData nextLevelData = upgradeData.GetUpgradeLevelData(currentLevel + 1);

            if (nextLevelData == null)
            {
                Debug.Log("더 이상 업그레이드할 수 없습니다.");
                return false;
            }

            foreach (LabUpgradeData.RequiredMaterial requiredMaterial in nextLevelData.requiredMaterials)
            {
                var targetInventory = requiredMaterial.item.TargetInventory("Player1");
                int itemCount = targetInventory.GetQuantity(requiredMaterial.item.ItemID);
                if (itemCount < requiredMaterial.amount)
                {
                    Debug.Log($"필요한 재료가 부족합니다. {requiredMaterial.item.ItemName} 필요 수량: {requiredMaterial.amount}, 보유 수량: {itemCount}");
                    return false;
                }
            }

            foreach (LabUpgradeData.UpgradeCondition upgradeCondition in nextLevelData.upgradeConditions)
            {
                // 업그레이드 조건 충족 여부 확인 로직 추가
                // 조건이 충족되지 않은 경우 false 반환
            }

            return true;
        }

        /// <summary>
        /// 업그레이드를 수행하는 메서드
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
        /// 현재 레벨의 업그레이드 데이터를 반환하는 메서드
        /// </summary>
        /// <returns>현재 레벨의 업그레이드 데이터</returns>
        public LabUpgradeData.UpgradeLevelData GetCurrentLevelData()
        {
            return upgradeData.GetUpgradeLevelData(currentLevel);
        }

        /// <summary>
        /// 선택한 레벨의 업그레이드 데이터를 반환하는 메서드
        /// </summary>
        /// <param name="selectedLevel">선택한 레벨</param>
        /// <returns>선택한 레벨의 업그레이드 데이터</returns>
        public LabUpgradeData.UpgradeLevelData GetSelectedLevelData(int selectedLevel)
        {
            return upgradeData.GetUpgradeLevelData(selectedLevel);
        }
    }
}