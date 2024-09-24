using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.BuffSystem
{
    /// <summary>
    /// 버프 효과의 기본 클래스
    /// </summary>
    public abstract class BuffEffectBase : ScriptableObject
    {
        /// <summary>
        /// 지속 효과 여부
        /// </summary>
        [SerializeField]
        private bool isPersistent;
        public bool IsPersistent => isPersistent;

        /// <summary>
        /// 효과 지속 시간 (초)
        /// </summary>
        [SerializeField]
        private float duration;
        public float Duration => duration;

        /// <summary>
        /// 주기적 효과 여부
        /// </summary>
        [SerializeField]
        private bool isPeriodic;
        public bool IsPeriodic => isPeriodic;

        /// <summary>
        /// 효과 발동 간격 (초)
        /// </summary>
        [SerializeField]
        private float interval;
        public float Interval => interval;

        /// <summary>
        /// 버프 효과를 대상에 적용하는 메서드
        /// </summary>
        /// <param name="target">버프 효과 적용 대상</param>
        public abstract void Apply(IBuffTarget target);
        
        /// <summary>
        /// 버프 효과를 대상에서 제거하는 메서드
        /// </summary>
        /// <param name="target">버프 효과 제거 대상</param>
        public abstract void Remove(IBuffTarget target);
        
        /// <summary>
        /// 버프 효과를 업데이트하는 메서드
        /// </summary>
        /// <param name="targets">버프 효과 적용 대상 목록</param>
        /// <param name="deltaTime">프레임 간 경과 시간 (초)</param>
        public abstract void UpdateEffect(List<IBuffTarget> targets, float deltaTime);

        /// <summary>
        /// 버프 효과 만료 여부
        /// </summary>
        public abstract bool IsExpired { get; }
    }
}
