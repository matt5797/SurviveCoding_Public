using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.BuffSystem;
using UnityEngine.Events;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// ������(Lab) ���� ������Ʈ�� ��Ÿ���� Ŭ�����Դϴ�.
    /// �������� ����, ���׷��̵�, ���� �� �����ǰ� ���õ� �ֿ� ������Ʈ�� ����� �����մϴ�.
    /// </summary>
    [RequireComponent(typeof(LabStatComponent))]
    [RequireComponent(typeof(LabBuffComponent))]
    [RequireComponent(typeof(LabProductionComponent))]
    [RequireComponent(typeof(LabUpgradeComponent))]
    public class Lab : MonoBehaviour
    {
        /// <summary>
        /// �������� ���� �ĺ���
        /// </summary>
        [SerializeField]
        private int id;

        /// <summary>
        /// �������� �̸�
        /// </summary>
        [SerializeField]
        private string labName;

        /// <summary>
        /// �������� Ȱ��ȭ ����
        /// </summary>
        [SerializeField]
        private bool isActive;

        /// <summary>
        /// �����ǿ� �Ҵ�� �±�
        /// </summary>
        [SerializeField]
        private LabTags tags = new LabTags();

        [SerializeField]
        private LabStatComponent statComponent;
        [SerializeField]
        private LabBuffComponent buffComponent;
        [SerializeField]
        private LabProductionComponent productionComponent;
        [SerializeField]
        private LabUpgradeComponent upgradeComponent;

        /// <summary>
        /// �������� Ȱ��ȭ�� �� ȣ��Ǵ� �̺�Ʈ
        /// </summary>
        public event Action OnLabActivated;

        /// <summary>
        /// �������� ��Ȱ��ȭ�� �� ȣ��Ǵ� �̺�Ʈ
        /// </summary>
        public event Action OnLabDeactivated;

        /// <summary>
        /// �������� �±װ� ����� �� ȣ��Ǵ� �̺�Ʈ
        /// </summary>
        public event Action OnTagsChanged;

        public string LabName => labName;

        /// <summary>
        /// �������� Ȱ��ȭ ���� ������Ƽ
        /// </summary>
        public bool IsActive
        {
            get => isActive;
            set
            {
                if (isActive != value)
                {
                    isActive = value;
                    if (isActive)
                    {
                        OnLabActivated?.Invoke();
                    }
                    else
                    {
                        OnLabDeactivated?.Invoke();
                    }
                }
            }
        }

        /// <summary>
        /// �������� �±� ������Ƽ
        /// </summary>
        public LabTags Tags
        {
            get => tags;
            set
            {
                tags = value;
                TagsChanged();
            }
        }

        public LabStatComponent StatComponent => statComponent;
        public LabBuffComponent BuffComponent => buffComponent;
        public LabProductionComponent ProductionComponent => productionComponent;
        public LabUpgradeComponent UpgradeComponent => upgradeComponent;

        private void Awake()
        {
            if (statComponent == null)
            {
                statComponent = GetComponent<LabStatComponent>();
            }
            if (buffComponent == null)
            {
                buffComponent = GetComponent<LabBuffComponent>();
            }
            if (productionComponent == null)
            {
                productionComponent = GetComponent<LabProductionComponent>();
            }
            if (upgradeComponent == null)
            {
                upgradeComponent = GetComponent<LabUpgradeComponent>();
            }

            OnLabActivated += OnActivated;
            OnLabDeactivated += OnDeactivated;
            upgradeComponent.OnUpgradeSucceeded += OnUpgradeSucceeded;

            tags.OnTagsChanged += TagsChanged;
        }

        /// <summary>
        /// �������� �±� ���� �����ϴ� �޼���
        /// </summary>
        /// <typeparam name="T">�±� ���� Ÿ��</typeparam>
        /// <param name="tagName">������ �±��� �̸�</param>
        /// <param name="value">������ �±��� ��</param>
        public void ChangeTagValue<T>(string tagName, T value)
        {
            var tagValues = tags.GetTagValues();
            if (tagValues.ContainsKey(tagName))
            {
                tags.SetTagValue(tagName, (Enum)(object)value);
                TagsChanged();
            }
        }

        /// <summary>
        /// �������� �±� ���� ����Ǿ��� �� ȣ��Ǵ� �޼���
        /// </summary>
        private void TagsChanged()
        {
            OnTagsChanged?.Invoke();
        }

        /// <summary>
        /// �������� stat ������Ʈ�� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <returns>�������� LabStatComponent</returns>
        public LabStatComponent GetStatComponent()
        {
            return statComponent;
        }

        /// <summary>
        /// �������� buff ������Ʈ�� ��ȯ�ϴ� �޼��� 
        /// </summary>
        /// <returns>�������� LabBuffComponent</returns>
        public LabBuffComponent GetBuffComponent()
        {
            return buffComponent;
        }

        /// <summary>
        /// �������� ���� ������Ʈ�� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <returns>�������� LabProductionComponent</returns>
        public LabProductionComponent GetProductionComponent()
        {
            return productionComponent;
        }

        /// <summary>
        /// �������� ���׷��̵� ������Ʈ�� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <returns>�������� LabUpgradeComponent</returns>
        public LabUpgradeComponent GetUpgradeComponent()
        {
            return upgradeComponent;
        }
        
        /// <summary>
        /// �������� ���׷��̵带 �õ��ϴ� �޼���
        /// </summary>
        public void AttemptUpgrade()
        {
            upgradeComponent.AttemptUpgrade();
        }

        /// <summary>
        /// �������� ���� ���׷��̵� �ܰ踦 ��ȯ�ϴ� �޼���
        /// </summary>
        /// <returns>�������� ���� ���׷��̵� �ܰ�</returns>
        public int GetCurrentUpgradeLevel()
        {
            return upgradeComponent.CurrentLevel;
        }

        /// <summary>
        /// �������� Ȱ��ȭ�� �� ȣ��Ǵ� �޼���
        /// </summary>
        private void OnActivated()
        {
            Tags.OperationStatus = OperationStatus.Operating;
        }

        /// <summary>
        /// �������� ��Ȱ��ȭ�� �� ȣ��Ǵ� �޼���
        /// </summary>
        private void OnDeactivated()
        {
            Tags.OperationStatus = OperationStatus.Idle;
        }

        /// <summary>
        /// ������ ���׷��̵忡 �������� �� ȣ��Ǵ� �޼���
        /// </summary>
        /// <param name="level">���׷��̵� �ܰ�</param>
        private void OnUpgradeSucceeded(int level)
        {
            switch (level)
            {
                case 1:
                    Tags.UpgradeStage = UpgradeStage.Stage1;
                    break;
                case 2:
                    Tags.UpgradeStage = UpgradeStage.Stage2;
                    break;
                case 3:
                    Tags.UpgradeStage = UpgradeStage.Stage3;
                    break;
                default:
                    Tags.UpgradeStage = UpgradeStage.Stage0;
                    break;
            }
        }

        /// <summary>
        /// �����ǿ� ����(Breakdown)�� �߻����� �� ȣ��Ǵ� �޼���
        /// </summary>
        public void OnBreakdownOccurred()
        {
            Tags.EventOccurrence = EventOccurrence.Breakdown;
        }

        /// <summary>
        /// �����ǿ� �糭(Disaster)�� �߻����� �� ȣ��Ǵ� �޼���
        /// </summary>
        public void OnDisasterOccurred()
        {
            Tags.EventOccurrence = EventOccurrence.Disaster;
        }
    }
}