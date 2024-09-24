using UnityEngine;

namespace SurviveCoding.BuffSystem
{
    /// <summary>
    /// 버프 적용 조건의 기반 클래스
    /// 버프가 적용되기 위한 조건을 정의하는 추상 클래스입니다.
    /// </summary>
    public abstract class BuffConditionBase : ScriptableObject
    {
        /// <summary>
        /// 버프 대상이 조건을 만족하는지 확인하는 메서드
        /// </summary>
        /// <param name="target">버프 적용 대상</param>
        /// <returns>조건 만족 여부</returns>
        public abstract bool IsValid(IBuffTarget target);
    }
}