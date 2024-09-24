using SurviveCoding.Player;
using UnityEngine;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "HungerLevelCondition", menuName = "SurviveCoding/Buff System/Conditions/HungerLevel")]
    public class HungerLevelCondition : BuffConditionBase
    {
        public HungerLevel requiredHungerLevel;

        public override bool IsValid(IBuffTarget target)
        {
            var player = target as PlayerBuffComponent;
            if (player != null)
            {
                return (player.GetComponent<Player.Player>().Tags.HungerLevel & requiredHungerLevel) != 0;
            }
            return false;
        }
    }
}