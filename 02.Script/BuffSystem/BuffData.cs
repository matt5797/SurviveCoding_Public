using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.BuffSystem
{
    /// <summary>
    /// ������ Ÿ���� ��Ÿ���� ������
    /// </summary>
    public enum BuffType
    {
        Buff,   // ����
        Debuff,  // �����
    }

    /// <summary>
    /// ������ �⺻ �����͸� �����ϴ� ScriptableObject Ŭ����
    /// ������ �Ӽ��� ȿ��, ���� ���� ������ �� �ֽ��ϴ�.
    /// </summary>
    [CreateAssetMenu(fileName = "New BuffData", menuName = "SurviveCoding/Buff System/Buff Data", order = 51)]
    public class BuffData : ScriptableObject
    {
        /// <summary>
        /// ������ ���� �ĺ���
        /// </summary>
        [SerializeField]
        private string buffId;

        /// <summary>
        /// ������ �̸�
        /// </summary>
        [SerializeField]
        private string buffName;

        /// <summary>
        /// ������ ����
        /// </summary>
        [SerializeField]
        [TextArea(1, 5)]
        private string description;

        /// <summary>
        /// ������ Ÿ�� (���� �Ǵ� �����)
        /// </summary>
        [SerializeField]
        private BuffType buffType;

        /// <summary>
        /// ���� ���� ����
        /// </summary>
        [SerializeField]
        private bool isPersistent;

        /// <summary>
        /// ������ ���� �ð�
        /// </summary>
        [SerializeField]
        private float duration;

        /// <summary>
        /// ��� ȿ�� ���� �� ���� ���� ����
        /// </summary>
        [SerializeField]
        private bool isRemoveWhenAllEffectsExpired;

        /// <summary>
        /// ������ ����Ǵ� ���� ���
        /// </summary>
        [SerializeField]
        private List<BuffConditionBase> conditions = new List<BuffConditionBase>();

        /// <summary>
        /// ������ ȿ�� ���
        /// </summary>
        [SerializeField]
        private List<BuffEffectBase> effects = new List<BuffEffectBase>();

        /// <summary>
        /// ������ ������
        [SerializeField]
        private Sprite icon;

        /// <summary>
        /// ������ ���� �ĺ��� ������Ƽ
        /// </summary>
        public string BuffId => buffId;

        /// <summary>
        /// ������ �̸� ������Ƽ
        /// </summary>
        public string BuffName => buffName;

        /// <summary>
        /// ������ ���� ������Ƽ
        /// </summary>
        public string Description => description;

        /// <summary>
        /// ���� ���� ���� ������Ƽ
        /// </summary>
        public bool IsPersistent => isPersistent;

        /// <summary>
        /// ������ ���� �ð� ������Ƽ
        /// </summary>
        public float Duration => duration;

        /// <summary>
        /// ��� ȿ�� ���� �� ���� ���� ���� ������Ƽ
        /// </summary>
        public bool IsRemoveWhenAllEffectsExpired => isRemoveWhenAllEffectsExpired;

        /// <summary>
        /// ������ ����Ǵ� ���� ��� ������Ƽ
        /// </summary>
        public List<BuffConditionBase> Conditions => conditions;

        /// <summary>
        /// ������ ȿ�� ��� ������Ƽ
        /// </summary>
        public List<BuffEffectBase> Effects => effects;

        /// <summary>
        /// ������ Ÿ�� ������Ƽ
        /// </summary>
        public BuffType BuffType => buffType;

        /// <summary>
        /// ������ ������ ������Ƽ
        /// </summary>
        public Sprite Icon => icon;
    }
}