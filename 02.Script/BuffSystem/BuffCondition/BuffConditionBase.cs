using UnityEngine;

namespace SurviveCoding.BuffSystem
{
    /// <summary>
    /// ���� ���� ������ ��� Ŭ����
    /// ������ ����Ǳ� ���� ������ �����ϴ� �߻� Ŭ�����Դϴ�.
    /// </summary>
    public abstract class BuffConditionBase : ScriptableObject
    {
        /// <summary>
        /// ���� ����� ������ �����ϴ��� Ȯ���ϴ� �޼���
        /// </summary>
        /// <param name="target">���� ���� ���</param>
        /// <returns>���� ���� ����</returns>
        public abstract bool IsValid(IBuffTarget target);
    }
}