using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// 연구실의 태그를 나타내는 클래스입니다.
    /// 연구실의 연구 분야, 보안 수준, 운영 상태 등 다양한 속성을 태그로 관리합니다.
    /// </summary>
    [Serializable]
    public class LabTags
    {
        /// <summary>
        /// 연구 분야 태그
        /// </summary>
        [SerializeField]
        public ResearchField researchField;
        /// <summary>
        /// 보안 수준 태그 
        /// </summary>
        [SerializeField]
        public SecurityLevel securityLevel;
        /// <summary>
        /// 운영 상태 태그
        /// </summary>
        [SerializeField]
        public OperationStatus operationStatus;
        /// <summary>
        /// 업그레이드 단계 태그
        /// </summary>
        [SerializeField]
        public UpgradeStage upgradeStage;
        /// <summary>
        /// 에너지 효율 태그
        /// </summary>
        [SerializeField]
        public EnergyEfficiency energyEfficiency;
        /// <summary>
        /// 생산 효율 태그
        /// </summary>
        [SerializeField]
        public ProductionEfficiency productionEfficiency;
        /// <summary>
        /// 내구도 태그
        /// </summary>
        [SerializeField]
        public Durability durability;
        /// <summary>
        /// 오염도 태그
        /// </summary>
        [SerializeField]
        public PollutionLevel pollutionLevel;
        /// <summary>
        /// 이벤트 발생 태그
        /// </summary>
        [SerializeField]
        public EventOccurrence eventOccurrence;

        /// <summary>
        /// 태그가 변경되었을 때 호출되는 이벤트
        /// </summary>
        public event Action OnTagsChanged;

        /// <summary>
        /// 모든 태그 값을 Dictionary 형태로 반환하는 메서드
        /// </summary>
        /// <returns>태그 이름과 값을 포함하는 Dictionary</returns>
        public Dictionary<string, Enum> GetTagValues()
        {
            return new Dictionary<string, Enum>
            {
                { nameof(researchField), researchField },
                { nameof(securityLevel), securityLevel },
                { nameof(operationStatus), operationStatus },
                { nameof(upgradeStage), upgradeStage },
                { nameof(energyEfficiency), energyEfficiency },
                { nameof(productionEfficiency), productionEfficiency },
                { nameof(durability), durability },
                { nameof(pollutionLevel), pollutionLevel },
                { nameof(eventOccurrence), eventOccurrence },
            };
        }

        /// <summary>
        /// 특정 태그 값을 설정하는 메서드
        /// </summary>
        /// <param name="tag">설정할 태그의 이름</param>
        /// <param name="value">설정할 태그의 값</param>
        public void SetTagValue(string tag, Enum value)
        {
            switch (tag)
            {
                case nameof(researchField):
                    researchField = (ResearchField)value;
                    break;
                case nameof(securityLevel):
                    securityLevel = (SecurityLevel)value;
                    break;
                case nameof(operationStatus):
                    operationStatus = (OperationStatus)value;
                    break;
                case nameof(upgradeStage):
                    upgradeStage = (UpgradeStage)value;
                    break;
                case nameof(energyEfficiency):
                    energyEfficiency = (EnergyEfficiency)value;
                    break;
                case nameof(productionEfficiency):
                    productionEfficiency = (ProductionEfficiency)value;
                    break;
                case nameof(durability):
                    durability = (Durability)value;
                    break;
                case nameof(pollutionLevel):
                    pollutionLevel = (PollutionLevel)value;
                    break;
                case nameof(eventOccurrence):
                    eventOccurrence = (EventOccurrence)value;
                    break;
            }

            OnTagsChanged?.Invoke();
        }

        /// <summary>
        /// 여러 태그 값을 한 번에 설정하는 메서드
        /// </summary>
        /// <param name="tagValues">설정할 태그 값들을 포함하는 Dictionary</param>
        public void SetTagValues(Dictionary<string, Enum> tagValues)
        {
            researchField = (ResearchField)tagValues[nameof(researchField)];
            securityLevel = (SecurityLevel)tagValues[nameof(securityLevel)];
            operationStatus = (OperationStatus)tagValues[nameof(operationStatus)];
            upgradeStage = (UpgradeStage)tagValues[nameof(upgradeStage)];
            energyEfficiency = (EnergyEfficiency)tagValues[nameof(energyEfficiency)];
            productionEfficiency = (ProductionEfficiency)tagValues[nameof(productionEfficiency)];
            durability = (Durability)tagValues[nameof(durability)];
            pollutionLevel = (PollutionLevel)tagValues[nameof(pollutionLevel)];
            eventOccurrence = (EventOccurrence)tagValues[nameof(eventOccurrence)];
        }
        
        /// <summary>
        /// 태그 변경 이벤트를 발생시키는 메서드
        /// </summary>
        private void NotifyTagsChanged()
        {
            OnTagsChanged?.Invoke();
        }

        /// <summary>
        /// 연구 분야 태그 프로퍼티
        /// </summary>
        public ResearchField ResearchField
        {
            get => researchField;
            set
            {
                researchField = value;
                NotifyTagsChanged();
            }
        }

        /// <summary>
        /// 보안 수준 태그 프로퍼티
        /// </summary>
        public SecurityLevel SecurityLevel
        {
            get => securityLevel;
            set
            {
                securityLevel = value;
                NotifyTagsChanged();
            }
        }

        /// <summary>
        /// 운영 상태 태그 프로퍼티
        /// </summary>
        public OperationStatus OperationStatus
        {
            get => operationStatus;
            set
            {
                operationStatus = value;
                NotifyTagsChanged();
            }
        }

        /// <summary>
        /// 업그레이드 단계 태그 프로퍼티 
        /// </summary>
        public UpgradeStage UpgradeStage
        {
            get => upgradeStage;
            set
            {
                upgradeStage = value;
                NotifyTagsChanged();
            }
        }

        /// <summary>
        /// 에너지 효율 태그 프로퍼티
        /// </summary>
        public EnergyEfficiency EnergyEfficiency
        {
            get => energyEfficiency;
            set
            {
                energyEfficiency = value;
                NotifyTagsChanged();
            }
        }

        /// <summary>
        /// 생산 효율 태그 프로퍼티
        /// </summary>
        public ProductionEfficiency ProductionEfficiency
        {
            get => productionEfficiency;
            set
            {
                productionEfficiency = value;
                NotifyTagsChanged();
            }
        }

        /// <summary>
        /// 내구도 태그 프로퍼티
        /// </summary>
        public Durability Durability
        {
            get => durability;
            set
            {
                durability = value;
                NotifyTagsChanged();
            }
        }

        /// <summary>
        /// 오염도 태그 프로퍼티
        /// </summary>
        public PollutionLevel PollutionLevel
        {
            get => pollutionLevel;
            set
            {
                pollutionLevel = value;
                NotifyTagsChanged();
            }
        }

        /// <summary>
        /// 이벤트 발생 태그 프로퍼티 
        /// </summary>
        public EventOccurrence EventOccurrence
        {
            get => eventOccurrence;
            set
            {
                eventOccurrence = value;
                NotifyTagsChanged();
            }
        }
    }
    
    /// <summary>
    /// 연구 분야를 나타내는 열거형
    /// </summary>
    [Serializable]
    [Flags]
    public enum ResearchField
    {
        None = 0,
        Bio = 1 << 0, // 1
        Tech = 1 << 1, // 2
        Energy = 1 << 2, // 4
        Environment = 1 << 3  // 8
    }

    /// <summary>
    /// 보안 수준을 나타내는 열거형 
    /// </summary>
    [Serializable]
    [Flags]
    public enum SecurityLevel
    {
        None = 0,
        Level1 = 1 << 0, // 1
        Level2 = 1 << 1, // 2
        Level3 = 1 << 2, // 4
        Level4 = 1 << 3  // 8
    }

    /// <summary>
    /// 운영 상태를 나타내는 열거형
    /// </summary>
    [Serializable]
    [Flags]
    public enum OperationStatus
    {
        None = 0,
        Operating = 1 << 0, // 1
        Idle = 1 << 1, // 2
    }

    /// <summary>
    /// 업그레이드 단계를 나타내는 열거형
    /// </summary>
    [Serializable]
    [Flags]
    public enum UpgradeStage
    {
        None = 0,
        Stage0 = 1 << 0, // 1
        Stage1 = 1 << 1, // 2
        Stage2 = 1 << 2, // 4
        Stage3 = 1 << 3  // 8
    }

    /// <summary>
    /// 에너지 효율을 나타내는 열거형 
    /// </summary>
    [Serializable]
    [Flags]
    public enum EnergyEfficiency
    {
        None = 0,
        Low = 1 << 0, // 1
        Medium = 1 << 1, // 2
        High = 1 << 2, // 4
    }

    /// <summary>
    /// 생산 효율을 나타내는 열거형
    /// </summary>
    [Serializable]
    [Flags]
    public enum ProductionEfficiency
    {
        None = 0,
        Low = 1 << 0, // 1
        Medium = 1 << 1, // 2
        High = 1 << 2, // 4
    }

    /// <summary>
    /// 내구도를 나타내는 열거형
    /// </summary>
    [Serializable]
    [Flags]
    public enum Durability
    {
        None = 0,
        Low = 1 << 0, // 1
        Medium = 1 << 1, // 2
        High = 1 << 2, // 4
    }

    /// <summary>
    /// 오염 수준을 나타내는 열거형
    /// </summary>
    [Serializable]
    [Flags]
    public enum PollutionLevel
    {
        None = 0,
        Low = 1 << 0, // 1
        Medium = 1 << 1, // 2
        High = 1 << 2, // 4
    }

    /// <summary>
    /// 이벤트 발생을 나타내는 열거형
    /// </summary>
    [Serializable]
    [Flags]
    public enum EventOccurrence
    {
        None = 0,
        Breakdown = 1 << 0, // 1
        Disaster = 1 << 1, // 2
    }
}