using UnityEngine;
using System.Collections.Generic;
using SurviveCoding.BuffSystem;
using MoreMountains.InventoryEngine;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// 연구실 업그레이드에 필요한 데이터를 정의하는 ScriptableObject
    /// 업그레이드 단계별로 필요한 자원, 조건, 생산 정보, 버프 등을 설정할 수 있습니다.
    /// </summary>
    [CreateAssetMenu(fileName = "New LabUpgradeData", menuName = "SurviveCoding/Lab System/Lab Upgrade Data", order = 52)]
    public class LabUpgradeData : ScriptableObject
    {
        /// <summary>
        /// 업그레이드 단계별 데이터를 나타내는 클래스
        /// </summary>
        [System.Serializable]
        public class UpgradeLevelData
        {
            /// <summary>업그레이드 단계</summary>
            public int level;
            /// <summary>업그레이드에 필요한 자원 목록</summary>
            public List<RequiredMaterial> requiredMaterials;
            /// <summary>업그레이드 조건 목록</summary>
            public List<UpgradeCondition> upgradeConditions;
            /// <summary>업그레이드 시 생산 데이터 목록</summary>
            public List<ProductionData> productionDataList;
            /// <summary>업그레이드 시 적용할 버프 목록</summary>
            public List<BuffData> upgradeBuffs;
            /// <summary>생산 시간 (초)</summary>
            public float productionTime;
        }

        /// <summary>
        /// 아이템 생산 정보를 나타내는 클래스
        /// </summary>
        [System.Serializable]
        public class ProductionData
        {
            public InventoryItem item;
            public int amount;
        }

        /// <summary>
        /// 업그레이드에 필요한 자원 정보를 나타내는 클래스
        /// </summary>
        [System.Serializable]
        public class RequiredMaterial
        {
            public InventoryItem item;
            public int amount;
        }

        /// <summary>
        /// 업그레이드 조건 정보를 나타내는 클래스
        /// </summary>
        [System.Serializable]
        public class UpgradeCondition
        {
            public string conditionType;
            public string conditionValue;
        }

        /// <summary>업그레이드 단계별 데이터 목록</summary>
        [SerializeField]
        private List<UpgradeLevelData> upgradeLevels = new List<UpgradeLevelData>();

        /// <summary>
        /// 업그레이드 단계별 데이터 목록에 접근하기 위한 프로퍼티
        /// </summary>
        public List<UpgradeLevelData> UpgradeLevels => upgradeLevels;

        /// <summary>
        /// 지정된 업그레이드 단계의 데이터를 반환하는 메서드
        /// </summary>
        /// <param name="level">가져올 업그레이드 단계</param>
        /// <returns>해당 업그레이드 단계의 데이터</returns>
        public UpgradeLevelData GetUpgradeLevelData(int level)
        {
            return upgradeLevels.Find(data => data.level == level);
        }
    }
}