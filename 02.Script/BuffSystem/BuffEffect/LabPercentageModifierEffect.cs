using UnityEngine;
using SurviveCoding.LabSystem;
using System;
using System.Collections.Generic;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "New Lab PercentageModifierEffect", menuName = "SurviveCoding/Buff System/Effects/Lab Percentage Modifier Effect", order = 51)]
    public class LabPercentageModifierEffect : BuffEffectBase
    {
        [SerializeField]
        private LabStatType statType;
        public LabStatType StatType => statType;

        [SerializeField]
        private float percentage;
        public float Percentage => percentage;

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
                    statComponent.AddPercentModifierToStat(StatType, Percentage);
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