using System;
using System.Collections.Generic;

namespace SurviveCoding.BuffSystem
{
    /// <summary>
    /// 버프 효과를 받는 대상 객체가 구현해야 하는 인터페이스
    /// </summary>
    public interface IBuffTarget
    {
        /// <summary>
        /// 버프 대상으로 등록하는 메서드
        /// </summary>
        void RegisterBuffTarget();

        /// <summary>
        /// 버프 대상에서 해제하는 메서드
        /// </summary>
        void UnregisterBuffTarget();

        /// <summary>
        /// 버프가 적용될 때 발생하는 이벤트
        /// </summary>
        event Action<Buff> OnBuffApplied;

        /// <summary>
        /// 버프가 제거될 때 발생하는 이벤트
        /// </summary>
        event Action<Buff> OnBuffRemoved;

        /// <summary>
        /// 버프를 적용하는 메서드
        /// </summary>
        /// <param name="buff">적용할 버프</param>
        void ApplyBuff(Buff buff);

        /// <summary>
        /// 버프를 제거하는 메서드
        /// </summary>
        /// <param name="buff">제거할 버프</param>
        void RemoveBuff(Buff buff);

        /// <summary>
        /// 현재 적용 중인 버프의 개수를 반환하는 메서드
        /// </summary>
        /// <returns>적용 중인 버프 개수</returns>
        int GetBuffCount();

        /// <summary>
        /// 특정 버프가 적용 중인지 확인하는 메서드
        /// </summary>
        /// <param name="buff">확인할 버프</param>
        /// <returns>버프 적용 여부</returns>
        bool HasBuff(Buff buff);
    }
}