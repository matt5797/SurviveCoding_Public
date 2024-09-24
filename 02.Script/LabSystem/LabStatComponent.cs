using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// 연구실 스탯 정보의 타입을 나타내는 열거형
    /// </summary>
    public enum LabStatType
    {
        /// <summary>에너지 소비량</summary>
        EnergyConsumption,
        /// <summary>내구도</summary>
        Durability,
        /// <summary>오염도</summary>
        PollutionLevel,
        /// <summary>에너지 효율</summary>
        EnergyEfficiency,
        /// <summary>생산 효율</summary>
        ProductionEfficiency,
        /// <summary>재료 생산 효율</summary>
        MaterialProductionEfficiency,
        /// <summary>희귀 재료 생산 효율</summary>
        RareMaterialProductionEfficiency,
        /// <summary>오염도 증가량</summary>
        PollutionIncrease,
        /// <summary>내구도 감소량</summary>
        DurabilityDecrease
    }

    /// <summary>
    /// 연구실의 스탯 정보를 관리하는 컴포넌트
    /// 연구실의 에너지 소비량, 내구도, 오염도 등 다양한 스탯을 관리하고 업데이트합니다.
    /// </summary>
    public class LabStatComponent : MonoBehaviour
    {
        /// <summary>
        /// 주기적 스탯(내구도, 오염도) 갱신 간격 (초)
        /// </summary>
        [SerializeField] private float updateInterval = 1.0f;
        /// <summary>
        /// 주기적 스탯(내구도, 오염도)의 값 조정을 위한 스케일 계수
        /// </summary>
        static readonly private float scaleCoefficient = 100f;

        /// <summary>
        /// 에너지 소비량 스탯
        /// </summary>
        [SerializeField] private LabStat energyConsumption;
        /// <summary>
        /// 내구도 스탯
        /// </summary>
        [SerializeField] private LabStat durability;
        /// <summary>
        /// 오염도 스탯 
        /// </summary>
        [SerializeField] private LabStat pollutionLevel;
        /// <summary>
        /// 에너지 효율 스탯
        /// </summary>
        [SerializeField] private LabStat energyEfficiency;
        /// <summary>
        /// 생산 효율 스탯
        /// </summary>
        [SerializeField] private LabStat productionEfficiency;
        /// <summary>
        /// 재료 생산 효율 스탯
        /// </summary>
        [SerializeField] private LabStat materialProductionEfficiency;
        /// <summary>
        /// 희귀 재료 생산 효율 스탯
        /// </summary>
        [SerializeField] private LabStat rareMaterialProductionEfficiency;
        /// <summary>
        /// 오염도 증가량 스탯
        /// </summary>
        [SerializeField] private LabStat pollutionIncrease;
        /// <summary>
        /// 내구도 감소량 스탯
        /// </summary>  
        [SerializeField] private LabStat durabilityDecrease;

        /// <summary>
        /// 스탯 정보를 저장하는 딕셔너리
        /// </summary>
        private Dictionary<LabStatType, LabStat> statDictionary = new Dictionary<LabStatType, LabStat>();

        private void Awake()
        {
            // LabStat 객체 초기화
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
        /// 주어진 타입의 스탯 정보를 반환하는 메서드
        /// </summary>
        /// <param name="statType">얻고자 하는 스탯 정보의 타입</param>
        /// <returns>해당 타입의 스탯 정보 객체 (LabStat)</returns>
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
        /// 지정된 스탯 정보에 백분율 보정치를 추가하는 메서드
        /// </summary>
        /// <param name="statType">보정치를 추가할 스탯 정보의 타입</param>
        /// <param name="value">추가할 백분율 보정치 값</param>
        public void AddPercentModifierToStat(LabStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.AddPercentModifier(value));
        }

        /// <summary>
        /// 지정된 스탯 정보에서 백분율 보정치를 제거하는 메서드 
        /// </summary>
        /// <param name="statType">보정치를 제거할 스탯 정보의 타입</param>
        /// <param name="value">제거할 백분율 보정치 값</param>
        public void RemovePercentModifierFromStat(LabStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.RemovePercentModifier(value));
        }

        /// <summary>
        /// 지정된 스탯 정보에 고정값 보정치를 추가하는 메서드
        /// </summary>
        /// <param name="statType">보정치를 추가할 스탯 정보의 타입</param>
        /// <param name="value">추가할 고정값 보정치</param>
        public void AddFlatModifierToStat(LabStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.AddFlatModifier(value));
        }

        /// <summary>
        /// 지정된 스탯 정보에서 고정값 보정치를 제거하는 메서드
        /// </summary>
        /// <param name="statType">보정치를 제거할 스탯 정보의 타입</param>
        /// <param name="value">제거할 고정값 보정치</param>
        public void RemoveFlatModifierFromStat(LabStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.RemoveFlatModifier(value));
        }

        /// <summary>
        /// 지정된 스탯 정보를 수정하는 내부 메서드
        /// </summary>
        /// <param name="statType">수정할 스탯 정보의 타입</param>
        /// <param name="modifyAction">스탯 정보를 수정하는 액션</param>
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
        /// 지정된 스탯 정보의 보정치를 초기화하는 메서드
        /// </summary>
        /// <param name="statType">보정치를 초기화할 스탯 정보의 타입</param>
        public void ResetStatModifiers(LabStatType statType)
        {
            if (statDictionary.TryGetValue(statType, out var stat))
            {
                stat.ResetModifiers();
            }
        }

        /// <summary>
        /// 연구실 스탯 정보를 주기적으로 업데이트하는 코루틴
        /// </summary>
        /// <returns>코루틴의 IEnumerator</returns>
        private IEnumerator UpdateStats()
        {
            while (true)
            {
                // 내구도 감소 적용
                float durabilityDecreaseValue = GetStat(LabStatType.DurabilityDecrease).CalculateValue();
                GetStat(LabStatType.Durability).CurrentValue -= durabilityDecreaseValue / scaleCoefficient;

                // 오염도 증가 적용
                float pollutionIncreaseValue = GetStat(LabStatType.PollutionIncrease).CalculateValue();
                GetStat(LabStatType.PollutionLevel).CurrentValue += pollutionIncreaseValue / scaleCoefficient;
                
                yield return new WaitForSeconds(updateInterval);
            }
        }

        /// <summary>
        /// 에너지 효율 변경 시 호출되는 콜백 메서드
        /// </summary>
        /// <param name="value">변경된 에너지 효율 값</param>
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
        /// 생산 효율 변경 시 호출되는 콜백 메서드  
        /// </summary>
        /// <param name="value">변경된 생산 효율 값</param>
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
        /// 내구도 변경 시 호출되는 콜백 메서드
        /// </summary>
        /// <param name="value">변경된 내구도 값</param>
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
        /// 오염도 변경 시 호출되는 콜백 메서드
        /// </summary> 
        /// <param name="value">변경된 오염도 값</param>
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