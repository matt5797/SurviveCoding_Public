using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.Player
{
    [System.Serializable]
    public class PlayerStat
    {
        [SerializeField] private float baseValue;
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;
        private float percentModifier;
        private float flatModifier;
        private float currentValue;

        public event Action<float> OnValueChanged;

        public float BaseValue { get => baseValue; set => baseValue = value; }
        public float MinValue { get => minValue; set => minValue = value; }
        public float MaxValue { get => maxValue; set => maxValue = value; }
        public float PercentModifier { get => percentModifier; set => percentModifier = value; }
        public float FlatModifier { get => flatModifier; set => flatModifier = value; }
        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, MinValue, MaxValue);
                OnValueChanged?.Invoke(currentValue);
            }
        }

        public PlayerStat(float baseValue, float minValue, float maxValue)
        {
            BaseValue = baseValue;
            MinValue = minValue;
            MaxValue = maxValue;
            Initialize();
        }

        public void Initialize()
        {
            PercentModifier = 0f;
            FlatModifier = 0f;
            CurrentValue = baseValue;
        }

        public float CalculateValue()
        {
            float calculatedValue = BaseValue * (1 + PercentModifier) + FlatModifier;
            return Mathf.Clamp(calculatedValue, MinValue, MaxValue);
        }

        public void ResetCurrentValue()
        {
            currentValue = BaseValue;
        }

        public void AddPercentModifier(float value)
        {
            PercentModifier += value;
        }

        public void RemovePercentModifier(float value)
        {
            PercentModifier -= value;
        }

        public void AddFlatModifier(float value)
        {
            FlatModifier += value;
        }

        public void RemoveFlatModifier(float value)
        {
            FlatModifier -= value;
        }

        public void ResetModifiers()
        {
            PercentModifier = 0f;
            FlatModifier = 0f;
        }
    }
}