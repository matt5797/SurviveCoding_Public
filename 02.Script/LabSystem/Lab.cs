using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.BuffSystem;
using UnityEngine.Events;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// 연구실(Lab) 게임 오브젝트를 나타내는 클래스입니다.
    /// 연구실의 상태, 업그레이드, 생산 등 연구실과 관련된 주요 컴포넌트와 기능을 관리합니다.
    /// </summary>
    [RequireComponent(typeof(LabStatComponent))]
    [RequireComponent(typeof(LabBuffComponent))]
    [RequireComponent(typeof(LabProductionComponent))]
    [RequireComponent(typeof(LabUpgradeComponent))]
    public class Lab : MonoBehaviour
    {
        /// <summary>
        /// 연구실의 고유 식별자
        /// </summary>
        [SerializeField]
        private int id;

        /// <summary>
        /// 연구실의 이름
        /// </summary>
        [SerializeField]
        private string labName;

        /// <summary>
        /// 연구실의 활성화 상태
        /// </summary>
        [SerializeField]
        private bool isActive;

        /// <summary>
        /// 연구실에 할당된 태그
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
        /// 연구실이 활성화될 때 호출되는 이벤트
        /// </summary>
        public event Action OnLabActivated;

        /// <summary>
        /// 연구실이 비활성화될 때 호출되는 이벤트
        /// </summary>
        public event Action OnLabDeactivated;

        /// <summary>
        /// 연구실의 태그가 변경될 때 호출되는 이벤트
        /// </summary>
        public event Action OnTagsChanged;

        public string LabName => labName;

        /// <summary>
        /// 연구실의 활성화 상태 프로퍼티
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
        /// 연구실의 태그 프로퍼티
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
        /// 연구실의 태그 값을 변경하는 메서드
        /// </summary>
        /// <typeparam name="T">태그 값의 타입</typeparam>
        /// <param name="tagName">변경할 태그의 이름</param>
        /// <param name="value">변경할 태그의 값</param>
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
        /// 연구실의 태그 값이 변경되었을 때 호출되는 메서드
        /// </summary>
        private void TagsChanged()
        {
            OnTagsChanged?.Invoke();
        }

        /// <summary>
        /// 연구실의 stat 컴포넌트를 반환하는 메서드
        /// </summary>
        /// <returns>연구실의 LabStatComponent</returns>
        public LabStatComponent GetStatComponent()
        {
            return statComponent;
        }

        /// <summary>
        /// 연구실의 buff 컴포넌트를 반환하는 메서드 
        /// </summary>
        /// <returns>연구실의 LabBuffComponent</returns>
        public LabBuffComponent GetBuffComponent()
        {
            return buffComponent;
        }

        /// <summary>
        /// 연구실의 생산 컴포넌트를 반환하는 메서드
        /// </summary>
        /// <returns>연구실의 LabProductionComponent</returns>
        public LabProductionComponent GetProductionComponent()
        {
            return productionComponent;
        }

        /// <summary>
        /// 연구실의 업그레이드 컴포넌트를 반환하는 메서드
        /// </summary>
        /// <returns>연구실의 LabUpgradeComponent</returns>
        public LabUpgradeComponent GetUpgradeComponent()
        {
            return upgradeComponent;
        }
        
        /// <summary>
        /// 연구실의 업그레이드를 시도하는 메서드
        /// </summary>
        public void AttemptUpgrade()
        {
            upgradeComponent.AttemptUpgrade();
        }

        /// <summary>
        /// 연구실의 현재 업그레이드 단계를 반환하는 메서드
        /// </summary>
        /// <returns>연구실의 현재 업그레이드 단계</returns>
        public int GetCurrentUpgradeLevel()
        {
            return upgradeComponent.CurrentLevel;
        }

        /// <summary>
        /// 연구실이 활성화될 때 호출되는 메서드
        /// </summary>
        private void OnActivated()
        {
            Tags.OperationStatus = OperationStatus.Operating;
        }

        /// <summary>
        /// 연구실이 비활성화될 때 호출되는 메서드
        /// </summary>
        private void OnDeactivated()
        {
            Tags.OperationStatus = OperationStatus.Idle;
        }

        /// <summary>
        /// 연구실 업그레이드에 성공했을 때 호출되는 메서드
        /// </summary>
        /// <param name="level">업그레이드 단계</param>
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
        /// 연구실에 고장(Breakdown)이 발생했을 때 호출되는 메서드
        /// </summary>
        public void OnBreakdownOccurred()
        {
            Tags.EventOccurrence = EventOccurrence.Breakdown;
        }

        /// <summary>
        /// 연구실에 재난(Disaster)이 발생했을 때 호출되는 메서드
        /// </summary>
        public void OnDisasterOccurred()
        {
            Tags.EventOccurrence = EventOccurrence.Disaster;
        }
    }
}