using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace SurviveCoding.BuffSystem
{
    /// <summary>
    /// 버프 시스템을 관리하는 클래스
    /// 버프 생성, 적용, 제거 등의 기능을 제공합니다.
    /// </summary>
    public class BuffManager : Singleton<BuffManager>
    {
        /// <summary>
        /// 버프 적용 대상 목록
        /// </summary>
        [SerializeField]
        private List<IBuffTarget> buffTargets = new List<IBuffTarget>();

        /// <summary>
        /// 현재 적용 중인 버프 목록
        /// </summary>
        [SerializeField]
        private List<Buff> buffs = new List<Buff>();

        /// <summary>
        /// 버프 생성 시 발생하는 유니티 이벤트
        /// </summary>
        [SerializeField]
        private UnityEvent<Buff> onBuffCreated = new UnityEvent<Buff>();
        public UnityEvent<Buff> OnBuffCreated => onBuffCreated;

        /// <summary>
        /// 버프 제거 시 발생하는 유니티 이벤트
        /// </summary>
        [SerializeField]
        private UnityEvent<Buff> onBuffRemoved = new UnityEvent<Buff>();
        public UnityEvent<Buff> OnBuffRemoved => onBuffRemoved;

        /// <summary>
        /// 버프 적용 대상 목록에 대한 프로퍼티
        /// </summary>
        public List<IBuffTarget> BuffTargets => buffTargets;

        /// <summary>
        /// 현재 적용 중인 버프 목록에 대한 프로퍼티
        /// </summary>
        public List<Buff> Buffs => buffs;

        private void Update()
        {
            UpdateBuffs(Time.deltaTime);
        }

        /// <summary>
        /// 버프 대상을 등록하는 메서드
        /// </summary>
        /// <param name="target">등록할 버프 대상</param>
        public void RegisterBuffTarget(IBuffTarget target)
        {
            buffTargets.Add(target);
            ApplyBuffsToTarget(target);
        }

        /// <summary>
        /// 버프 대상을 등록 해제하는 메서드
        /// </summary>
        /// <param name="target">등록 해제할 버프 대상</param>
        public void UnregisterBuffTarget(IBuffTarget target)
        {
            buffTargets.Remove(target);
        }

        /// <summary>
        /// 만료된 버프를 처리하는 메서드
        /// </summary>
        /// <param name="expiredBuff">만료된 버프</param>
        private void OnBuffExpired(Buff expiredBuff)
        {
            RemoveExpiredBuff(expiredBuff);
        }

        /// <summary>
        /// ID로 버프를 찾는 메서드
        /// </summary>
        /// <param name="buffId">찾을 버프의 ID</param>
        /// <returns>찾은 버프 (없을 경우 null)</returns>
        public Buff FindBuffByID(string buffId)
        {
            Buff buff = buffs.Find(b => b.Data.BuffId == buffId);
            if (buff == null)
            {
                Debug.Log($"Buff {buffId} not found.");
            }
            return buff;
        }

        /// <summary>
        /// 버프 데이터를 기반으로 새로운 버프를 생성하고 적용하는 메서드
        /// </summary>
        /// <param name="buffData">생성할 버프의 데이터</param>
        public Buff CreateAndApplyBuff(BuffData buffData)
        {
            Buff newBuff = CreateBuffFromData(buffData);
            if (newBuff != null)
            {
                buffs.Add(newBuff);
                ApplyBuffToTargets(newBuff);
                onBuffCreated?.Invoke(newBuff);
            }
            return newBuff;
        }

        /// <summary>
        /// 버프 데이터를 기반으로 새로운 버프 객체를 생성하는 메서드
        /// </summary>
        /// <param name="buffData">생성할 버프의 데이터</param>
        /// <returns>생성된 버프 객체</returns>
        private Buff CreateBuffFromData(BuffData buffData)
        {
            // buffData를 기반으로 새로운 Buff 객체 생성 후 반환
            Buff newBuff = new Buff(buffData);

            foreach (var effectData in buffData.Effects)
            {
                BuffEffectBase effect = Instantiate(effectData);
                newBuff.AddEffect(effect);
            }

            newBuff.OnBuffExpired += OnBuffExpired;

            return newBuff;
        }

        /// <summary>
        /// 버프를 대상들에게 적용하는 메서드
        /// </summary>
        /// <param name="buff">적용할 버프</param>
        private void ApplyBuffToTargets(Buff buff)
        {
            foreach (var target in buffTargets)
            {
                if (IsBuffApplicable(target, buff))
                {
                    target.ApplyBuff(buff);
                }
            }
        }

        /// <summary>
        /// 버프를 제거하는 메서드
        /// </summary>
        /// <param name="buff">제거할 버프</param>
        public void RemoveBuff(Buff buff)
        {
            if (buffs.Contains(buff))
            {
                buffs.Remove(buff);
                RemoveBuffFromTargets(buff);
                onBuffRemoved?.Invoke(buff);
            }
        }

        /// <summary>
        /// 버프를 대상들로부터 제거하는 메서드
        /// </summary>
        /// <param name="buff">제거할 버프</param>
        private void RemoveBuffFromTargets(Buff buff)
        {
            foreach (var target in buffTargets)
            {
                if (target.HasBuff(buff))
                {
                    target.RemoveBuff(buff);
                }
            }
        }

        /// <summary>
        /// 버프가 대상에게 적용 가능한지 확인하는 메서드
        /// </summary>
        /// <param name="target">확인할 버프 대상</param>
        /// <param name="buff">적용 가능성을 확인할 버프</param>
        /// <returns>버프 적용 가능 여부</returns>
        private bool IsBuffApplicable(IBuffTarget target, Buff buff)
        {
            return buff.Data.Conditions.All(condition => condition.IsValid(target));
        }

        /// <summary>
        /// 버프 업데이트 메서드 (매 프레임 호출)
        /// </summary>
        /// <param name="deltaTime">프레임 간 시간 간격</param>
        private void UpdateBuffs(float deltaTime)
        {
            foreach (var buff in buffs.ToList())
            {
                buff.Update(buffTargets.FindAll(target => target.HasBuff(buff)).ToList(), deltaTime);
            }
        }

        /// <summary>
        /// 만료된 버프를 제거하는 메서드
        /// </summary>
        /// <param name="buff">제거할 만료된 버프</param>
        private void RemoveExpiredBuff(Buff buff)
        {
            buff.OnBuffExpired -= OnBuffExpired;
            RemoveBuffFromTargets(buff);
            buffs.Remove(buff);
        }

        public void ApplyBuffsToTarget(IBuffTarget target)
        {
            foreach (var buff in buffs.ToList())
            {
                if (IsBuffApplicable(target, buff))
                {
                    target.ApplyBuff(buff);
                }
            }
        }

        public void RevalidateBuffsForTarget(IBuffTarget target)
        {
            foreach (var buff in buffs.ToList())
            {
                if (target.HasBuff(buff) && !IsBuffApplicable(target, buff))
                {
                    target.RemoveBuff(buff);
                }
                else if (!target.HasBuff(buff) && IsBuffApplicable(target, buff))
                {
                    target.ApplyBuff(buff);
                }
            }
        }
    }
}