using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "New Lab FlatModifierEffect", menuName = "SurviveCoding/Buff System/Effects/Lab Flat Modifier Effect", order = 52)]
    public class LabFlatModifierEffect : BuffEffectBase
    {
        [SerializeField]
        private LabStatType statType;
        public LabStatType StatType => statType;

        [SerializeField]
        private float value;
        public float Value => value;

        private float elapsedTime;

        public override bool IsExpired => !IsPersistent && elapsedTime >= Duration;

        public override void Apply(IBuffTarget target)
        {
            var labBuffComponent = target as LabBuffComponent;
            if (labBuffComponent != null)
            {
                var statComponent = labBuffComponent.Lab.GetStatComponent();
                if (statComponent != null)
                {
                    statComponent.AddFlatModifierToStat(StatType, Value);
                }
            }
        }

        public override void Remove(IBuffTarget target)
        {
            var labBuffComponent = target as LabBuffComponent;
            if (labBuffComponent != null)
            {
                var statComponent = labBuffComponent.Lab.GetStatComponent();
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