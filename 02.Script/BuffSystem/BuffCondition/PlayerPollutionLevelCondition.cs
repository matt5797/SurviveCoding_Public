using SurviveCoding.Player;
using UnityEngine;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "PlayerPollutionLevelCondition", menuName = "SurviveCoding/Buff System/Conditions/PlayerPollutionLevel")]
    public class PlayerPollutionLevelCondition : BuffConditionBase
    {
        public PollutionLevel requiredPollutionLevel;

        public override bool IsValid(IBuffTarget target)
        {
            var player = target as PlayerBuffComponent;
            if (player != null)
            {
                return (player.GetComponent<Player.Player>().Tags.PollutionLevel & requiredPollutionLevel) != 0;
            }
            return false;
        }
    }
}