using UnityEngine;
using SurviveCoding.Player;
using System.Collections.Generic;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "New Player CurrentValueEffect", menuName = "SurviveCoding/Buff System/Effects/Player Current Value Effect", order = 55)]
    public class PlayerCurrentValueEffect : BuffEffectBase
    {
        [SerializeField]
        private PlayerStatType statType;
        public PlayerStatType StatType => statType;

        [SerializeField]
        private float value;
        public float Value => value;
        
        private float elapsedTime;
        private float lastUpdateTime;

        public override bool IsExpired => !IsPersistent && elapsedTime >= Duration;

        public override void Apply(IBuffTarget target)
        {
            var playerBuffComponent = target as PlayerBuffComponent;
            if (playerBuffComponent != null)
            {
                var statComponent = playerBuffComponent.GetComponent<PlayerStatComponent>();
                if (statComponent != null)
                {
                    statComponent.GetStat(StatType).CurrentValue += Value;
                }
            }
        }

        public override void Remove(IBuffTarget target)
        {
            // 주기적 효과는 제거 시 특별한 동작이 필요하지 않습니다.
        }

        public override void UpdateEffect(List<IBuffTarget> targets, float deltaTime)
        {
            elapsedTime += deltaTime;

            if (elapsedTime - lastUpdateTime >= Interval)
            {
                lastUpdateTime = elapsedTime;

                foreach (var target in targets)
                {
                    var playerBuffComponent = target as PlayerBuffComponent;
                    if (playerBuffComponent != null)
                    {
                        var statComponent = playerBuffComponent.GetComponent<PlayerStatComponent>();
                        if (statComponent != null)
                        {
                            statComponent.GetStat(StatType).CurrentValue += Value;
                        }
                    }
                }
            }
        }
    }
}