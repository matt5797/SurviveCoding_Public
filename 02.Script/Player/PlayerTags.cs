using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.Player
{
    [Serializable]
    public class PlayerTags
    {
        [SerializeField] private HealthLevel healthLevel;
        [SerializeField] private HungerLevel hungerLevel;
        [SerializeField] private PollutionLevel pollutionLevel;

        public event Action OnTagsChanged;

        public Dictionary<string, Enum> GetTagValues()
        {
            return new Dictionary<string, Enum>
            {
                { nameof(healthLevel), healthLevel },
                { nameof(hungerLevel), hungerLevel },
                { nameof(pollutionLevel), pollutionLevel },
            };
        }

        public void SetTagValue(string tag, Enum value)
        {
            switch (tag)
            {
                case nameof(healthLevel):
                    HealthLevel = (HealthLevel)value;
                    break;
                case nameof(hungerLevel):
                    HungerLevel = (HungerLevel)value;
                    break;
                case nameof(pollutionLevel):
                    PollutionLevel = (PollutionLevel)value;
                    break;
            }
        }

        public void SetTagValues(Dictionary<string, Enum> tagValues)
        {
            HealthLevel = (HealthLevel)tagValues[nameof(healthLevel)];
            HungerLevel = (HungerLevel)tagValues[nameof(hungerLevel)];
            PollutionLevel = (PollutionLevel)tagValues[nameof(pollutionLevel)];
        }

        private void NotifyTagsChanged()
        {
            OnTagsChanged?.Invoke();
        }

        public HealthLevel HealthLevel
        {
            get => healthLevel;
            set
            {
                healthLevel = value;
                NotifyTagsChanged();
            }
        }

        public HungerLevel HungerLevel
        {
            get => hungerLevel;
            set
            {
                hungerLevel = value;
                NotifyTagsChanged();
            }
        }

        public PollutionLevel PollutionLevel
        {
            get => pollutionLevel;
            set
            {
                pollutionLevel = value;
                NotifyTagsChanged();
            }
        }
    }

    [Serializable]
    [Flags]
    public enum HealthLevel
    {
        None = 0,
        Low = 1 << 0,
        Medium = 1 << 1,
        High = 1 << 2,
    }

    [Serializable]
    [Flags]
    public enum HungerLevel
    {
        None = 0,
        Low = 1 << 0,
        Medium = 1 << 1,
        High = 1 << 2,
    }

    [Serializable]
    [Flags]
    public enum PollutionLevel
    {
        None = 0,
        Low = 1 << 0,
        Medium = 1 << 1,
        High = 1 << 2,
    }
}