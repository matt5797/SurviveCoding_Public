using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.BuffSystem
{
    /// <summary>
    /// 버프의 타입을 나타내는 열거형
    /// </summary>
    public enum BuffType
    {
        Buff,   // 버프
        Debuff,  // 디버프
    }

    /// <summary>
    /// 버프의 기본 데이터를 정의하는 ScriptableObject 클래스
    /// 버프의 속성과 효과, 조건 등을 설정할 수 있습니다.
    /// </summary>
    [CreateAssetMenu(fileName = "New BuffData", menuName = "SurviveCoding/Buff System/Buff Data", order = 51)]
    public class BuffData : ScriptableObject
    {
        /// <summary>
        /// 버프의 고유 식별자
        /// </summary>
        [SerializeField]
        private string buffId;

        /// <summary>
        /// 버프의 이름
        /// </summary>
        [SerializeField]
        private string buffName;

        /// <summary>
        /// 버프의 설명
        /// </summary>
        [SerializeField]
        [TextArea(1, 5)]
        private string description;

        /// <summary>
        /// 버프의 타입 (버프 또는 디버프)
        /// </summary>
        [SerializeField]
        private BuffType buffType;

        /// <summary>
        /// 지속 버프 여부
        /// </summary>
        [SerializeField]
        private bool isPersistent;

        /// <summary>
        /// 버프의 지속 시간
        /// </summary>
        [SerializeField]
        private float duration;

        /// <summary>
        /// 모든 효과 만료 시 버프 제거 여부
        /// </summary>
        [SerializeField]
        private bool isRemoveWhenAllEffectsExpired;

        /// <summary>
        /// 버프가 적용되는 조건 목록
        /// </summary>
        [SerializeField]
        private List<BuffConditionBase> conditions = new List<BuffConditionBase>();

        /// <summary>
        /// 버프의 효과 목록
        /// </summary>
        [SerializeField]
        private List<BuffEffectBase> effects = new List<BuffEffectBase>();

        /// <summary>
        /// 버프의 아이콘
        [SerializeField]
        private Sprite icon;

        /// <summary>
        /// 버프의 고유 식별자 프로퍼티
        /// </summary>
        public string BuffId => buffId;

        /// <summary>
        /// 버프의 이름 프로퍼티
        /// </summary>
        public string BuffName => buffName;

        /// <summary>
        /// 버프의 설명 프로퍼티
        /// </summary>
        public string Description => description;

        /// <summary>
        /// 지속 버프 여부 프로퍼티
        /// </summary>
        public bool IsPersistent => isPersistent;

        /// <summary>
        /// 버프의 지속 시간 프로퍼티
        /// </summary>
        public float Duration => duration;

        /// <summary>
        /// 모든 효과 만료 시 버프 제거 여부 프로퍼티
        /// </summary>
        public bool IsRemoveWhenAllEffectsExpired => isRemoveWhenAllEffectsExpired;

        /// <summary>
        /// 버프가 적용되는 조건 목록 프로퍼티
        /// </summary>
        public List<BuffConditionBase> Conditions => conditions;

        /// <summary>
        /// 버프의 효과 목록 프로퍼티
        /// </summary>
        public List<BuffEffectBase> Effects => effects;

        /// <summary>
        /// 버프의 타입 프로퍼티
        /// </summary>
        public BuffType BuffType => buffType;

        /// <summary>
        /// 버프의 아이콘 프로퍼티
        /// </summary>
        public Sprite Icon => icon;
    }
}