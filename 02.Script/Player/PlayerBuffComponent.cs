using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.BuffSystem;
using System;

namespace SurviveCoding.Player
{
    public class PlayerBuffComponent : MonoBehaviour, IBuffTarget
    {
        private PlayerStatComponent playerStatComponent;

        private List<Buff> appliedBuffs = new List<Buff>();

        public event Action<Buff> OnBuffApplied;
        public event Action<Buff> OnBuffRemoved;

        public List<Buff> Buffs => appliedBuffs;

        private void Awake()
        {
            playerStatComponent = GetComponent<PlayerStatComponent>();
        }
        
        private void Start()
        {
            RegisterBuffTarget();
        }

        private void OnDestroy()
        {
            UnregisterBuffTarget();
        }

        public void RegisterBuffTarget()
        {
            if (BuffManager.Instance != null)
            {
                BuffManager.Instance.RegisterBuffTarget(this);
            }
        }

        public void UnregisterBuffTarget()
        {
            if (BuffManager.Instance != null)
            {
                BuffManager.Instance.UnregisterBuffTarget(this);
            }
        }

        public void ApplyBuff(Buff buff)
        {
            appliedBuffs.Add(buff);
            buff.Apply(this);
            OnBuffApplied?.Invoke(buff);
        }

        public void RemoveBuff(Buff buff)
        {
            appliedBuffs.Remove(buff);
            buff.Remove(this);
            OnBuffRemoved?.Invoke(buff);
        }

        public int GetBuffCount()
        {
            return appliedBuffs.Count;
        }

        public bool HasBuff(Buff buff)
        {
            return appliedBuffs.Contains(buff);
        }
    }
}