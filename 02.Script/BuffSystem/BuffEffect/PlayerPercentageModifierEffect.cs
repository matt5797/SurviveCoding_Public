using UnityEngine;
using SurviveCoding.Player;
using System.Collections.Generic;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "New Player PercentageModifierEffect", menuName = "SurviveCoding/Buff System/Effects/Player Percentage Modifier Effect", order = 54)]
    public class PlayerPercentageModifierEffect : BuffEffectBase
    {
        [SerializeField]
        private PlayerStatType statType;
        public PlayerStatType StatType => statType;

        [SerializeField]
        private float percentage;
        public float Percentage => percentage;

        private float elapsedTime;

        public override bool IsExpired => !IsPersistent && elapsedTime >= Duration;

        public override void Apply(IBuffTarget target)
        {
            var playerBuffComponent = target as PlayerBuffComponent;
            if (playerBuffComponent != null)
            {
                var statComponent = playerBuffComponent.GetComponent<PlayerStatComponent>();
                if (statComponent != null)
                {
                    statComponent.AddPercentModifierToStat(StatType, Percentage);
                }
            }
        }

        public override void Remove(IBuffTarget target)
        {
            var playerBuffComponent = target as PlayerBuffComponent;
            if (playerBuffComponent != null)
            {
                var statComponent = playerBuffComponent.GetComponent<PlayerStatComponent>();
                if (statComponent != null)
                {
                    statComponent.RemovePercentModifierFromStat(StatType, Percentage);
                }
            }
        }

        public override void UpdateEffect(List<IBuffTarget> targets, float deltaTime)
        {
            if (!IsPersistent)
            {
                elapsedTime += deltaTime;
            }
        }
    }
}