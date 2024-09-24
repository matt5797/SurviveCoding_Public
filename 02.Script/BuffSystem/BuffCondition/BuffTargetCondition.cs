using UnityEngine;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "BuffTargetCondition", menuName = "SurviveCoding/Buff System/Conditions/Target")]
    public class BuffTargetCondition : BuffConditionBase
    {
        public enum TargetType
        {
            Player,
            Lab
        }

        public TargetType targetType;

        public override bool IsValid(IBuffTarget target)
        {
            switch (targetType)
            {
                case TargetType.Player:
                    return target is Player.PlayerBuffComponent;
                case TargetType.Lab:
                    return target is LabSystem.LabBuffComponent;
                default:
                    return false;
            }
        }
    }
}