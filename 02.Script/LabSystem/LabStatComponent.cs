using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// ������ ���� ������ Ÿ���� ��Ÿ���� ������
    /// </summary>
    public enum LabStatType
    {
        /// <summary>������ �Һ�</summary>
        EnergyConsumption,
        /// <summary>������</summary>
        Durability,
        /// <summary>������</summary>
        PollutionLevel,
        /// <summary>������ ȿ��</summary>
        EnergyEfficiency,
        /// <summary>���� ȿ��</summary>
        ProductionEfficiency,
        /// <summary>��� ���� ȿ��</summary>
        MaterialProductionEfficiency,
        /// <summary>��� ��� ���� ȿ��</summary>
        RareMaterialProductionEfficiency,
        /// <summary>������ ������</summary>
        PollutionIncrease,
        /// <summary>������ ���ҷ�</summary>
        DurabilityDecrease
    }

    /// <summary>
    /// �������� ���� ������ �����ϴ� ������Ʈ
    /// �������� ������ �Һ�, ������, ������ �� �پ��� ������ �����ϰ� ������Ʈ�մϴ�.
    /// </summary>
    public class LabStatComponent : MonoBehaviour
    {
        /// <summary>
        /// �ֱ��� ����(������, ������) ���� ���� (��)
        /// </summary>
        [SerializeField] private float updateInterval = 1.0f;
        /// <summary>
        /// �ֱ��� ����(������, ������)�� �� ������ ���� ������ ���
        /// </summary>
        static readonly private float scaleCoefficient = 100f;

        /// <summary>
        /// ������ �Һ� ����
        /// </summary>
        [SerializeField] private LabStat energyConsumption;
        /// <summary>
        /// ������ ����
        /// </summary>
        [SerializeField] private LabStat durability;
        /// <summary>
        /// ������ ���� 
        /// </summary>
        [SerializeField] private LabStat pollutionLevel;
        /// <summary>
        /// ������ ȿ�� ����
        /// </summary>
        [SerializeField] private LabStat energyEfficiency;
        /// <summary>
        /// ���� ȿ�� ����
        /// </summary>
        [SerializeField] private LabStat productionEfficiency;
        /// <summary>
        /// ��� ���� ȿ�� ����
        /// </summary>
        [SerializeField] private LabStat materialProductionEfficiency;
        /// <summary>
        /// ��� ��� ���� ȿ�� ����
        /// </summary>
        [SerializeField] private LabStat rareMaterialProductionEfficiency;
        /// <summary>
        /// ������ ������ ����
        /// </summary>
        [SerializeField] private LabStat pollutionIncrease;
        /// <summary>
        /// ������ ���ҷ� ����
        /// </summary>  
        [SerializeField] private LabStat durabilityDecrease;

        /// <summary>
        /// ���� ������ �����ϴ� ��ųʸ�
        /// </summary>
        private Dictionary<LabStatType, LabStat> statDictionary = new Dictionary<LabStatType, LabStat>();

        private void Awake()
        {
            // LabStat ��ü �ʱ�ȭ
            energyConsumption.Initialize();
            durability.Initialize();
            pollutionLevel.Initialize();
            energyEfficiency.Initialize();
            productionEfficiency.Initialize();
            materialProductionEfficiency.Initialize();
            rareMaterialProductionEfficiency.Initialize();
            pollutionIncrease.Initialize();
            durabilityDecrease.Initialize();

            statDictionary.Add(LabStatType.EnergyConsumption, energyConsumption);
            statDictionary.Add(LabStatType.Durability, durability);
            statDictionary.Add(LabStatType.PollutionLevel, pollutionLevel);
            statDictionary.Add(LabStatType.EnergyEfficiency, energyEfficiency);
            statDictionary.Add(LabStatType.ProductionEfficiency, productionEfficiency);
            statDictionary.Add(LabStatType.MaterialProductionEfficiency, materialProductionEfficiency);
            statDictionary.Add(LabStatType.RareMaterialProductionEfficiency, rareMaterialProductionEfficiency);
            statDictionary.Add(LabStatType.PollutionIncrease, pollutionIncrease);
            statDictionary.Add(LabStatType.DurabilityDecrease, durabilityDecrease);

            energyEfficiency.OnValueChanged += OnEnergyEfficiencyChanged;
            productionEfficiency.OnValueChanged += OnProductionEfficiencyChanged;
            durability.OnValueChanged += OnDurabilityChanged;
            pollutionLevel.OnValueChanged += OnPollutionLevelChanged;

            StartCoroutine(UpdateStats());
        }

        /// <summary>
        /// �־��� Ÿ���� ���� ������ ��ȯ�ϴ� �޼���
        /// </summary>
        /// <param name="statType">����� �ϴ� ���� ������ Ÿ��</param>
        /// <returns>�ش� Ÿ���� ���� ���� ��ü (LabStat)</returns>
        public LabStat GetStat(LabStatType statType)
        {
            if (statDictionary.TryGetValue(statType, out var stat))
            {
                return stat;
            }
            else
            {
                Debug.LogError($"Stat '{statType}' not found in LabStatComponent.");
                return null;
            }
        }

        /// <summary>
        /// ������ ���� ������ ����� ����ġ�� �߰��ϴ� �޼���
        /// </summary>
        /// <param name="statType">����ġ�� �߰��� ���� ������ Ÿ��</param>
        /// <param name="value">�߰��� ����� ����ġ ��</param>
        public void AddPercentModifierToStat(LabStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.AddPercentModifier(value));
        }

        /// <summary>
        /// ������ ���� �������� ����� ����ġ�� �����ϴ� �޼��� 
        /// </summary>
        /// <param name="statType">����ġ�� ������ ���� ������ Ÿ��</param>
        /// <param name="value">������ ����� ����ġ ��</param>
        public void RemovePercentModifierFromStat(LabStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.RemovePercentModifier(value));
        }

        /// <summary>
        /// ������ ���� ������ ������ ����ġ�� �߰��ϴ� �޼���
        /// </summary>
        /// <param name="statType">����ġ�� �߰��� ���� ������ Ÿ��</param>
        /// <param name="value">�߰��� ������ ����ġ</param>
        public void AddFlatModifierToStat(LabStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.AddFlatModifier(value));
        }

        /// <summary>
        /// ������ ���� �������� ������ ����ġ�� �����ϴ� �޼���
        /// </summary>
        /// <param name="statType">����ġ�� ������ ���� ������ Ÿ��</param>
        /// <param name="value">������ ������ ����ġ</param>
        public void RemoveFlatModifierFromStat(LabStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.RemoveFlatModifier(value));
        }

        /// <summary>
        /// ������ ���� ������ �����ϴ� ���� �޼���
        /// </summary>
        /// <param name="statType">������ ���� ������ Ÿ��</param>
        /// <param name="modifyAction">���� ������ �����ϴ� �׼�</param>
        private void ModifyStat(LabStatType statType, Action<LabStat> modifyAction)
        {
            if (statDictionary.TryGetValue(statType, out var stat))
            {
                modifyAction(stat);
            }
            else
            {
                throw new ArgumentException($"Stat '{statType}' not found in LabStatComponent.");
            }
        }

        /// <summary>
        /// ������ ���� ������ ����ġ�� �ʱ�ȭ�ϴ� �޼���
        /// </summary>
        /// <param name="statType">����ġ�� �ʱ�ȭ�� ���� ������ Ÿ��</param>
        public void ResetStatModifiers(LabStatType statType)
        {
            if (statDictionary.TryGetValue(statType, out var stat))
            {
                stat.ResetModifiers();
            }
        }

        /// <summary>
        /// ������ ���� ������ �ֱ������� ������Ʈ�ϴ� �ڷ�ƾ
        /// </summary>
        /// <returns>�ڷ�ƾ�� IEnumerator</returns>
        private IEnumerator UpdateStats()
        {
            while (true)
            {
                // ������ ���� ����
                float durabilityDecreaseValue = GetStat(LabStatType.DurabilityDecrease).CalculateValue();
                GetStat(LabStatType.Durability).CurrentValue -= durabilityDecreaseValue / scaleCoefficient;

                // ������ ���� ����
                float pollutionIncreaseValue = GetStat(LabStatType.PollutionIncrease).CalculateValue();
                GetStat(LabStatType.PollutionLevel).CurrentValue += pollutionIncreaseValue / scaleCoefficient;
                
                yield return new WaitForSeconds(updateInterval);
            }
        }

        /// <summary>
        /// ������ ȿ�� ���� �� ȣ��Ǵ� �ݹ� �޼���
        /// </summary>
        /// <param name="value">����� ������ ȿ�� ��</param>
        private void OnEnergyEfficiencyChanged(float value)
        {
            LabTags tags = GetComponent<Lab>().Tags;
            EnergyEfficiency newEnergyEfficiency = EnergyEfficiency.None;
            if (value < 0.75f)
                newEnergyEfficiency = EnergyEfficiency.Low;
            else if (value < 1.5f)
                newEnergyEfficiency = EnergyEfficiency.Medium;
            else
                newEnergyEfficiency = EnergyEfficiency.High;

            if (newEnergyEfficiency != tags.EnergyEfficiency)
                tags.EnergyEfficiency = newEnergyEfficiency;
        }

        /// <summary>
        /// ���� ȿ�� ���� �� ȣ��Ǵ� �ݹ� �޼���  
        /// </summary>
        /// <param name="value">����� ���� ȿ�� ��</param>
        private void OnProductionEfficiencyChanged(float value)
        {
            LabTags tags = GetComponent<Lab>().Tags;
            ProductionEfficiency newProductionEfficiency = ProductionEfficiency.None;
            if (value < 0.75f)
                newProductionEfficiency = ProductionEfficiency.Low;
            else if (value < 1.5f)
                newProductionEfficiency = ProductionEfficiency.Medium;
            else
                newProductionEfficiency = ProductionEfficiency.High;
            if (newProductionEfficiency != tags.ProductionEfficiency)
                tags.ProductionEfficiency = newProductionEfficiency;
        }

        /// <summary>
        /// ������ ���� �� ȣ��Ǵ� �ݹ� �޼���
        /// </summary>
        /// <param name="value">����� ������ ��</param>
        private void OnDurabilityChanged(float value)
        {
            LabTags tags = GetComponent<Lab>().Tags;
            Durability newDurability = Durability.None;
            if (value < 30f)
                newDurability = Durability.Low;
            else if (value < 70f)
                newDurability = Durability.Medium;
            else
                newDurability = Durability.High;
            if (newDurability != tags.Durability)
                tags.Durability = newDurability;
        }

        /// <summary>
        /// ������ ���� �� ȣ��Ǵ� �ݹ� �޼���
        /// </summary> 
        /// <param name="value">����� ������ ��</param>
        private void OnPollutionLevelChanged(float value)
        {
            LabTags tags = GetComponent<Lab>().Tags;
            PollutionLevel newPollutionLevel = PollutionLevel.None;
            if (value < 30f)
                newPollutionLevel = PollutionLevel.Low;
            else if (value < 70f)
                newPollutionLevel = PollutionLevel.Medium;
            else
                newPollutionLevel = PollutionLevel.High;
            
            if (newPollutionLevel != tags.PollutionLevel)
                tags.PollutionLevel = newPollutionLevel;
        }
    }
}