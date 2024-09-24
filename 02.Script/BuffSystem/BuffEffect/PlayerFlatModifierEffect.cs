using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.Player;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "New Player FlatModifierEffect", menuName = "SurviveCoding/Buff System/Effects/Player Flat Modifier Effect", order = 53)]
    public class PlayerFlatModifierEffect : BuffEffectBase
    {
        [SerializeField]
        private PlayerStatType statType;
        public PlayerStatType StatType => statType;

        [SerializeField]
        private float value;
        public float Value => value;

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
                    statComponent.AddFlatModifierToStat(StatType, Value);
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
                    statComponent.RemoveFlatModifierFromStat(StatType, Value);
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