using System;
using System.Linq;
using System.Collections.Generic;

namespace SurviveCoding.BuffSystem
{
    /// <summary>
    /// 버프 인스턴스를 나타내는 클래스
    /// 버프 데이터를 기반으로 생성되며, 버프의 효과와 지속 시간 등을 관리합니다.
    /// </summary>
    [Serializable]
    public class Buff
    {
        /// <summary>
        /// 버프 데이터
        /// </summary>
        private BuffData data;

        /// <summary>
        /// 지속 버프 여부
        /// </summary>
        private bool isPersistent;

        /// <summary>
        /// 버프의 지속 시간
        /// </summary>
        private float duration;

        /// <summary>
        /// 버프의 남은 지속 시간
        /// </summary>
        private float remainingDuration;

        /// <summary>
        /// 모든 효과 만료 시 버프 제거 여부
        /// </summary>
        private bool isRemoveWhenAllEffectsExpired;

        /// <summary>
        /// 버프의 효과 목록
        /// </summary>
        private List<BuffEffectBase> effects = new List<BuffEffectBase>();

        /// <summary>
        /// 버프 만료 시 발생하는 이벤트
        /// </summary>
        public event Action<Buff> OnBuffExpired;

        /// <summary>
        /// 버프 데이터 프로퍼티
        /// </summary>
        public BuffData Data => data;

        /// <summary>
        /// 지속 버프 여부 프로퍼티
        /// </summary>
        public bool IsPersistent => isPersistent;

        /// <summary>
        /// 버프의 지속 시간 프로퍼티
        /// </summary>
        public float Duration => duration;

        /// <summary>
        /// 버프의 남은 지속 시간 프로퍼티
        /// </summary>
        public float RemainingDuration => remainingDuration;

        /// <summary>
        /// 모든 효과 만료 시 버프 제거 여부 프로퍼티
        /// </summary>
        public bool IsRemoveWhenAllEffectsExpired => isRemoveWhenAllEffectsExpired;

        /// <summary>
        /// 버프 인스턴스 생성자
        /// </summary>
        /// <param name="data">버프 데이터</param>
        public Buff(BuffData data)
        {
            this.data = data;
            this.isPersistent = data.IsPersistent;
            this.duration = data.Duration;
            this.isRemoveWhenAllEffectsExpired = data.IsRemoveWhenAllEffectsExpired;
            this.remainingDuration = data.Duration;
        }

        /// <summary>
        /// 버프 효과를 추가하는 메서드
        /// </summary>
        /// <param name="effect">추가할 버프 효과</param>
        public void AddEffect(BuffEffectBase effect)
        {
            effects.Add(effect);
        }

        /// <summary>
        /// 버프 효과를 제거하는 메서드
        /// </summary>
        /// <param name="effect">제거할 버프 효과</param>
        public void RemoveEffect(BuffEffectBase effect)
        {
            effects.Remove(effect);
        }

        /// <summary>
        /// 버프를 대상에 적용하는 메서드
        /// </summary>
        /// <param name="target">버프를 적용할 대상</param>
        public void Apply(IBuffTarget target)
        {
            foreach (var effect in effects)
            {
                effect.Apply(target);
            }
        }

        /// <summary>
        /// 버프를 대상에서 제거하는 메서드
        /// </summary>
        /// <param name="target">버프를 제거할 대상</param>
        public void Remove(IBuffTarget target)
        {
            foreach (var effect in effects)
            {
                effect.Remove(target);
            }
        }

        /// <summary>
        /// 버프를 업데이트하는 메서드
        /// </summary>
        /// <param name="targets">버프 효과를 적용할 대상 목록</param>
        /// <param name="deltaTime">프레임 간 경과 시간</param>
        public void Update(List<IBuffTarget> targets, float deltaTime)
        {
            UpdateEffects(targets, deltaTime);
            CheckExpiration(deltaTime);
        }

        /// <summary>
        /// 버프 효과를 업데이트하는 메서드
        /// </summary>
        /// <param name="targets">버프 효과를 적용할 대상 목록</param>
        /// <param name="deltaTime">프레임 간 경과 시간</param>
        private void UpdateEffects(List<IBuffTarget> targets, float deltaTime)
        {
            foreach (var effect in effects.ToList())
            {
                effect.UpdateEffect(targets, deltaTime);
                if (effect.IsExpired)
                {
                    targets.ForEach(target => effect.Remove(target));
                    RemoveEffect(effect);
                }
            }
        }

        /// <summary>
        /// 버프 만료 여부를 확인하는 메서드
        /// </summary>
        /// <param name="deltaTime">프레임 간 경과 시간</param>
        private void CheckExpiration(float deltaTime)
        {
            if (!isPersistent)
            {
                remainingDuration -= deltaTime;
                if (remainingDuration <= 0f)
                {
                    OnBuffExpired?.Invoke(this);
                }
            }

            if (isRemoveWhenAllEffectsExpired && effects.Count == 0)
            {
                OnBuffExpired?.Invoke(this);
            }
        }
    }
}