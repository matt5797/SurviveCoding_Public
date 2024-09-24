using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.BuffSystem;
using System;

namespace SurviveCoding.LabSystem
{
    /// <summary>
    /// ������(Lab)�� ���� ȿ���� �����ϴ� ������Ʈ
    /// �����ǿ� ������ �����ϰ� �����ϴ� ����� ����մϴ�.
    /// ���� �Ŵ���(BuffManager)�� ��ȣ �ۿ��Ͽ� ���� ȿ���� ����ȭ�մϴ�.
    /// </summary>
    public class LabBuffComponent : MonoBehaviour, IBuffTarget
    {
        private Lab lab;
        private LabUpgradeComponent upgradeComponent;

        /// <summary>
        /// �����ǿ� ����� ���� ���
        /// </summary>
        private List<Buff> appliedBuffs = new List<Buff>();

        private List<Buff> buffsToRemove = new List<Buff>();

        /// <summary>
        /// ������ ����� �� �߻��ϴ� �̺�Ʈ
        /// </summary>
        public event Action<Buff> OnBuffApplied;
        /// <summary>
        /// ������ ���ŵ� �� �߻��ϴ� �̺�Ʈ
        /// </summary>
        public event Action<Buff> OnBuffRemoved;

        /// <summary>
        /// �� ������Ʈ�� ���� ������ (Lab) �ν��Ͻ�
        /// </summary>
        public Lab Lab => lab;
        /// <summary>
        /// ���� ���� ���� ���� ���
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
        /// ������ Ȱ��ȭ �� ȣ��Ǿ� ���� ȿ���� �����ϴ� �޼���
        /// </summary>
        private void OnLabActivated()
        {
            ApplyLevelBuffs();
        }

        /// <summary>
        /// ������ ��Ȱ��ȭ �� ȣ��Ǿ� ���� ȿ���� �����ϴ� �޼��� 
        /// </summary>
        private void OnLabDeactivated()
        {
            RemoveLevelBuffs();
        }

        /// <summary>
        /// BuffManager�� ���� ��� (IBuffTarget)���� ����ϴ� �޼���
        /// </summary>
        public void RegisterBuffTarget()
        {
            if (BuffManager.Instance != null)
            {
                BuffManager.Instance.RegisterBuffTarget(this);
            }
        }

        /// <summary>
        /// BuffManager���� ���� ��� (IBuffTarget) ����� �����ϴ� �޼���
        /// </summary>
        public void UnregisterBuffTarget()
        {
            if (BuffManager.Instance != null)
            {
                BuffManager.Instance.UnregisterBuffTarget(this);
            }
        }

        /// <summary>
        /// ���ο� ������ �����ǿ� �����ϴ� �޼���
        /// </summary>
        /// <param name="buff">������ ����</param>
        public void ApplyBuff(Buff buff)
        {
            appliedBuffs.Add(buff);
            buff.Apply(this);
            OnBuffApplied?.Invoke(buff);
        }

        /// <summary>
        /// �����ǿ� ����� ������ �����ϴ� �޼���
        /// </summary>
        /// <param name="buff">������ ����</param>
        public void RemoveBuff(Buff buff)
        {
            appliedBuffs.Remove(buff);
            buff.Remove(this);
            OnBuffRemoved?.Invoke(buff);
        }

        /// <summary>
        /// ���� ���� ���� ������ ������ ��ȯ�ϴ� �޼���
        /// </summary>
        /// <returns>���� ���� ���� ����</returns>
        public int GetBuffCount()
        {
            return appliedBuffs.Count;
        }

        /// <summary>
        /// Ư�� ������ �����ǿ� ����Ǿ� �ִ��� Ȯ���ϴ� �޼���
        /// </summary>
        /// <param name="buff">Ȯ���� ����</param>
        /// <returns>���� ���� ����</returns>
        public bool HasBuff(Buff buff)
        {
            return appliedBuffs.Contains(buff);
        }

        /// <summary>
        /// ������ ������ �ش��ϴ� ������ �����ϴ� �޼���
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
        /// �����ǿ� ����� ���� ������ �����ϴ� �޼���
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