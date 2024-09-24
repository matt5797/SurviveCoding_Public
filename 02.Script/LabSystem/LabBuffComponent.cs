using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.BuffSystem;
using System;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// 연구실(Lab)의 버프 효과를 관리하는 컴포넌트
    /// 연구실에 버프를 적용하고 제거하는 기능을 담당합니다.
    /// 버프 매니저(BuffManager)와 상호 작용하여 버프 효과를 동기화합니다.
    /// </summary>
    public class LabBuffComponent : MonoBehaviour, IBuffTarget
    {
        private Lab lab;
        private LabUpgradeComponent upgradeComponent;

        /// <summary>
        /// 연구실에 적용된 버프 목록
        /// </summary>
        private List<Buff> appliedBuffs = new List<Buff>();

        private List<Buff> buffsToRemove = new List<Buff>();

        /// <summary>
        /// 버프가 적용될 때 발생하는 이벤트
        /// </summary>
        public event Action<Buff> OnBuffApplied;
        /// <summary>
        /// 버프가 제거될 때 발생하는 이벤트
        /// </summary>
        public event Action<Buff> OnBuffRemoved;

        /// <summary>
        /// 이 컴포넌트가 속한 연구실 (Lab) 인스턴스
        /// </summary>
        public Lab Lab => lab;
        /// <summary>
        /// 현재 적용 중인 버프 목록
        /// </summary>
        public List<Buff> Buffs => appliedBuffs;
        
        private void OnEnable()
        {
            lab.OnLabActivated += OnLabActivated;
            lab.OnLabDeactivated += OnLabDeactivated;
            upgradeComponent = GetComponent<LabUpgradeComponent>();
            upgradeComponent.OnUpgradeSucceeded += OnUpgradeSucceeded;
            
            RegisterBuffTarget();
        }

        private void Awake()
        {
            lab = GetComponent<Lab>();
            lab.OnTagsChanged += OnTagsChanged;
        }

        private void OnDisable()
        {
            lab.OnLabActivated -= OnLabActivated;
            lab.OnLabDeactivated -= OnLabDeactivated;
            
            UnregisterBuffTarget();
        }

        private void OnDestroy()
        {
            lab.OnTagsChanged -= OnTagsChanged;
        }

        private void OnTagsChanged()
        {
            RevalidateBuffs();
        }

        private void RevalidateBuffs()
        {
            BuffManager.Instance.RevalidateBuffsForTarget(this);
        }

        /// <summary>
        /// 연구실 활성화 시 호출되어 버프 효과를 적용하는 메서드
        /// </summary>
        private void OnLabActivated()
        {
            ApplyLevelBuffs();
        }

        /// <summary>
        /// 연구실 비활성화 시 호출되어 버프 효과를 제거하는 메서드 
        /// </summary>
        private void OnLabDeactivated()
        {
            RemoveLevelBuffs();
        }

        /// <summary>
        /// BuffManager에 버프 대상 (IBuffTarget)으로 등록하는 메서드
        /// </summary>
        public void RegisterBuffTarget()
        {
            if (BuffManager.Instance != null)
            {
                BuffManager.Instance.RegisterBuffTarget(this);
            }
        }

        /// <summary>
        /// BuffManager에서 버프 대상 (IBuffTarget) 등록을 해제하는 메서드
        /// </summary>
        public void UnregisterBuffTarget()
        {
            if (BuffManager.Instance != null)
            {
                BuffManager.Instance.UnregisterBuffTarget(this);
            }
        }

        /// <summary>
        /// 새로운 버프를 연구실에 적용하는 메서드
        /// </summary>
        /// <param name="buff">적용할 버프</param>
        public void ApplyBuff(Buff buff)
        {
            appliedBuffs.Add(buff);
            buff.Apply(this);
            OnBuffApplied?.Invoke(buff);
        }

        /// <summary>
        /// 연구실에 적용된 버프를 제거하는 메서드
        /// </summary>
        /// <param name="buff">제거할 버프</param>
        public void RemoveBuff(Buff buff)
        {
            appliedBuffs.Remove(buff);
            buff.Remove(this);
            OnBuffRemoved?.Invoke(buff);
        }

        /// <summary>
        /// 현재 적용 중인 버프의 개수를 반환하는 메서드
        /// </summary>
        /// <returns>적용 중인 버프 개수</returns>
        public int GetBuffCount()
        {
            return appliedBuffs.Count;
        }

        /// <summary>
        /// 특정 버프가 연구실에 적용되어 있는지 확인하는 메서드
        /// </summary>
        /// <param name="buff">확인할 버프</param>
        /// <returns>버프 적용 여부</returns>
        public bool HasBuff(Buff buff)
        {
            return appliedBuffs.Contains(buff);
        }

        /// <summary>
        /// 연구실 레벨에 해당하는 버프를 적용하는 메서드
        /// </summary>
        private void ApplyLevelBuffs()
        {
            if (!lab.IsActive)
                return;
            
            LabUpgradeComponent upgradeComponent = GetComponent<LabUpgradeComponent>();
            LabUpgradeData.UpgradeLevelData currentLevelData = upgradeComponent.GetCurrentLevelData();

            foreach (BuffData buffData in currentLevelData.upgradeBuffs)
            {
                Buff newbuff = BuffManager.Instance.CreateAndApplyBuff(buffData);
                buffsToRemove.Add(newbuff);
            }
        }

        /// <summary>
        /// 연구실에 적용된 레벨 버프를 제거하는 메서드
        /// </summary>
        private void RemoveLevelBuffs()
        {
            foreach (Buff buff in buffsToRemove)
            {
                BuffManager.Instance.RemoveBuff(buff);
            }

            buffsToRemove.Clear();
        }

        private void OnUpgradeSucceeded(int level)
        {
            RemoveLevelBuffs();
            ApplyLevelBuffs();
        }
    }
}