using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// 연구실의 개별 스탯을 나타내는 클래스
    /// 기본값, 최소/최대값, 변경 이벤트 등을 관리합니다.
    /// </summary>
    [System.Serializable]
    public class LabStat
    {
        [SerializeField] private float baseValue;
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;
        private float percentModifier;
        private float flatModifier;
        private float currentValue;

        /// <summary>
        /// 스탯 값이 변경되었을 때 발생하는 이벤트
        /// </summary>
        public event Action<float> OnValueChanged;

        /// <summary>
        /// 스탯의 기본값
        /// </summary>
        public float BaseValue { get => baseValue; set => baseValue = value; }
        /// <summary>
        /// 스탯의 최소값
        /// </summary>
        public float MinValue { get => minValue; set => minValue = value; }
        /// <summary>
        /// 스탯의 최대값 
        /// </summary>
        public float MaxValue { get => maxValue; set => maxValue = value; }
        /// <summary>
        /// 스탯에 적용되는 퍼센트 모디파이어
        /// </summary>
        public float PercentModifier { get => percentModifier; set => percentModifier = value; }
        /// <summary>
        /// 스탯에 적용되는 고정값 모디파이어
        /// </summary>
        public float FlatModifier { get => flatModifier; set => flatModifier = value; }
        /// <summary>
        /// 스탯의 현재 값. 변경 시 MinValue와 MaxValue 사이로 제한되며 OnValueChanged 이벤트를 발생시킵니다.
        /// </summary>
        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, MinValue, MaxValue);
                OnValueChanged?.Invoke(currentValue);
            }
        }

        /// <summary>
        /// 주어진 기본값, 최소값, 최대값으로 새로운 LabStat 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="baseValue">기본값</param>
        /// <param name="minValue">최소값</param>
        /// <param name="maxValue">최대값</param>
        public LabStat(float baseValue, float minValue, float maxValue)
        {
            BaseValue = baseValue;
            MinValue = minValue;
            MaxValue = maxValue;
            Initialize();
        }

        /// <summary>
        /// LabStat을 초기 상태로 초기화합니다.
        /// </summary>
        public void Initialize()
        {
            PercentModifier = 0f;
            FlatModifier = 0f;
            CurrentValue = baseValue;
        }

        /// <summary>
        /// 모든 모디파이어를 적용하여 스탯의 최종값을 계산합니다.
        /// </summary>
        /// <returns>계산된 스탯의 최종값</returns>
        public float CalculateValue()
        {
            float calculatedValue = BaseValue * (1 + PercentModifier) + FlatModifier;
            return Mathf.Clamp(calculatedValue, MinValue, MaxValue);
        }

        /// <summary>
        /// 모든 모디파이어를 적용하여 스탯의 최종값을 계산합니다.
        /// </summary>
        /// <returns>계산된 스탯의 최종값</returns>
        public void ResetCurrentValue()
        {
            currentValue = BaseValue;
        }

        /// <summary>
        /// 스탯의 PercentModifier에 값을 더합니다.
        /// </summary>
        /// <param name="value">더할 값</param>
        public void AddPercentModifier(float value)
        {
            PercentModifier += value;
        }

        /// <summary>
        /// 스탯의 PercentModifier에서 값을 뺍니다.
        /// </summary>
        /// <param name="value">뺄 값</param>
        public void RemovePercentModifier(float value)
        {
            PercentModifier -= value;
        }

        /// <summary>
        /// 스탯의 FlatModifier에 값을 더합니다.
        /// </summary>
        /// <param name="value">더할 값</param>
        public void AddFlatModifier(float value)
        {
            FlatModifier += value;
        }

        /// <summary>
        /// 스탯의 FlatModifier에서 값을 뺍니다.
        /// </summary>
        /// <param name="value">뺄 값</param>
        public void RemoveFlatModifier(float value)
        {
            FlatModifier -= value;
        }

        /// <summary>
        /// 모든 모디파이어를 0으로 재설정합니다.
        /// </summary>
        public void ResetModifiers()
        {
            PercentModifier = 0f;
            FlatModifier = 0f;
        }
    }
}