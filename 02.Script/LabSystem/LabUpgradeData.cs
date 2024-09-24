using UnityEngine;
using System.Collections.Generic;
using SurviveCoding.BuffSystem;
using MoreMountains.InventoryEngine;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// ������ ���׷��̵忡 �ʿ��� �����͸� �����ϴ� ScriptableObject
    /// ���׷��̵� �ܰ躰�� �ʿ��� �ڿ�, ����, ���� ����, ���� ���� ������ �� �ֽ��ϴ�.
    /// </summary>
    [CreateAssetMenu(fileName = "New LabUpgradeData", menuName = "SurviveCoding/Lab System/Lab Upgrade Data", order = 52)]
    public class LabUpgradeData : ScriptableObject
    {
        /// <summary>
        /// ���׷��̵� �ܰ躰 �����͸� ��Ÿ���� Ŭ����
        /// </summary>
        [System.Serializable]
        public class UpgradeLevelData
        {
            /// <summary>���׷��̵� �ܰ�</summary>
            public int level;
            /// <summary>���׷��̵忡 �ʿ��� �ڿ� ���</summary>
            public List<RequiredMaterial> requiredMaterials;
            /// <summary>���׷��̵� ���� ���</summary>
            public List<UpgradeCondition> upgradeConditions;
            /// <summary>���׷��̵� �� ���� ������ ���</summary>
            public List<ProductionData> productionDataList;
            /// <summary>���׷��̵� �� ������ ���� ���</summary>
            public List<BuffData> upgradeBuffs;
            /// <summary>���� �ð� (��)</summary>
            public float productionTime;
        }

        /// <summary>
        /// ������ ���� ������ ��Ÿ���� Ŭ����
        /// </summary>
        [System.Serializable]
        public class ProductionData
        {
            public InventoryItem item;
            public int amount;
        }

        /// <summary>
        /// ���׷��̵忡 �ʿ��� �ڿ� ������ ��Ÿ���� Ŭ����
        /// </summary>
        [System.Serializable]
        public class RequiredMaterial
        {
            public InventoryItem item;
            public int amount;
        }

        /// <summary>
        /// ���׷��̵� ���� ������ ��Ÿ���� Ŭ����
        /// </summary>
        [System.Serializable]
        public class UpgradeCondition
        {
            public string conditionType;
            public string conditionValue;
        }

        /// <summary>���׷��̵� �ܰ躰 ������ ���</summary>
        [SerializeField]
        private List<UpgradeLevelData> upgradeLevels = new List<UpgradeLevelData>();

        /// <summary>
        /// ���׷��̵� �ܰ躰 ������ ��Ͽ� �����ϱ� ���� ������Ƽ
        /// </summary>
        public List<UpgradeLevelData> UpgradeLevels => upgradeLevels;

        /// <summary>
        /// ������ ���׷��̵� �ܰ��� �����͸� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <param name="level">������ ���׷��̵� �ܰ�</param>
        /// <returns>�ش� ���׷��̵� �ܰ��� ������</returns>
        public UpgradeLevelData GetUpgradeLevelData(int level)
        {
            return upgradeLevels.Find(data => data.level == level);
        }
    }
}