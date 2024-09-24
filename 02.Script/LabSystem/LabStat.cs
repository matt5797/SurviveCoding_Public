using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// �������� ���� ������ ��Ÿ���� Ŭ����
    /// �⺻��, �ּ�/�ִ밪, ���� �̺�Ʈ ���� �����մϴ�.
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
        /// ���� ���� ����Ǿ��� �� �߻��ϴ� �̺�Ʈ
        /// </summary>
        public event Action<float> OnValueChanged;

        /// <summary>
        /// ������ �⺻��
        /// </summary>
        public float BaseValue { get => baseValue; set => baseValue = value; }
        /// <summary>
        /// ������ �ּҰ�
        /// </summary>
        public float MinValue { get => minValue; set => minValue = value; }
        /// <summary>
        /// ������ �ִ밪 
        /// </summary>
        public float MaxValue { get => maxValue; set => maxValue = value; }
        /// <summary>
        /// ���ȿ� ����Ǵ� �ۼ�Ʈ ������̾�
        /// </summary>
        public float PercentModifier { get => percentModifier; set => percentModifier = value; }
        /// <summary>
        /// ���ȿ� ����Ǵ� ������ ������̾�
        /// </summary>
        public float FlatModifier { get => flatModifier; set => flatModifier = value; }
        /// <summary>
        /// ������ ���� ��. ���� �� MinValue�� MaxValue ���̷� ���ѵǸ� OnValueChanged �̺�Ʈ�� �߻���ŵ�ϴ�.
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
        /// �־��� �⺻��, �ּҰ�, �ִ밪���� ���ο� LabStat �ν��Ͻ��� �����մϴ�.
        /// </summary>
        /// <param name="baseValue">�⺻��</param>
        /// <param name="minValue">�ּҰ�</param>
        /// <param name="maxValue">�ִ밪</param>
        public LabStat(float baseValue, float minValue, float maxValue)
        {
            BaseValue = baseValue;
            MinValue = minValue;
            MaxValue = maxValue;
            Initialize();
        }

        /// <summary>
        /// LabStat�� �ʱ� ���·� �ʱ�ȭ�մϴ�.
        /// </summary>
        public void Initialize()
        {
            PercentModifier = 0f;
            FlatModifier = 0f;
            CurrentValue = baseValue;
        }

        /// <summary>
        /// ��� ������̾ �����Ͽ� ������ �������� ����մϴ�.
        /// </summary>
        /// <returns>���� ������ ������</returns>
        public float CalculateValue()
        {
            float calculatedValue = BaseValue * (1 + PercentModifier) + FlatModifier;
            return Mathf.Clamp(calculatedValue, MinValue, MaxValue);
        }

        /// <summary>
        /// ��� ������̾ �����Ͽ� ������ �������� ����մϴ�.
        /// </summary>
        /// <returns>���� ������ ������</returns>
        public void ResetCurrentValue()
        {
            currentValue = BaseValue;
        }

        /// <summary>
        /// ������ PercentModifier�� ���� ���մϴ�.
        /// </summary>
        /// <param name="value">���� ��</param>
        public void AddPercentModifier(float value)
        {
            PercentModifier += value;
        }

        /// <summary>
        /// ������ PercentModifier���� ���� ���ϴ�.
        /// </summary>
        /// <param name="value">�� ��</param>
        public void RemovePercentModifier(float value)
        {
            PercentModifier -= value;
        }

        /// <summary>
        /// ������ FlatModifier�� ���� ���մϴ�.
        /// </summary>
        /// <param name="value">���� ��</param>
        public void AddFlatModifier(float value)
        {
            FlatModifier += value;
        }

        /// <summary>
        /// ������ FlatModifier���� ���� ���ϴ�.
        /// </summary>
        /// <param name="value">�� ��</param>
        public void RemoveFlatModifier(float value)
        {
            FlatModifier -= value;
        }

        /// <summary>
        /// ��� ������̾ 0���� �缳���մϴ�.
        /// </summary>
        public void ResetModifiers()
        {
            PercentModifier = 0f;
            FlatModifier = 0f;
        }
    }
}