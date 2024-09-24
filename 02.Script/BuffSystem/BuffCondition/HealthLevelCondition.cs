using SurviveCoding.Player;
using UnityEngine;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "HealthLevelCondition", menuName = "SurviveCoding/Buff System/Conditions/HealthLevel")]
    public class HealthLevelCondition : BuffConditionBase
    {
        public HealthLevel requiredHealthLevel;

        public override bool IsValid(IBuffTarget target)
        {
            var player = target as PlayerBuffComponent;
            if (player != null)
            {
                return (player.GetComponent<Player.Player>().Tags.HealthLevel & requiredHealthLevel) != 0;
            }
            return false;
        }
    }
}