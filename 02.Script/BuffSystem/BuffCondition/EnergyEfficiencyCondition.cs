using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "EnergyEfficiencyCondition", menuName = "SurviveCoding/Buff System/Conditions/Energy Efficiency")]
    public class EnergyEfficiencyCondition : BuffConditionBase
    {
        public EnergyEfficiency requiredEnergyEfficiency;

        public override bool IsValid(IBuffTarget target)
        {
            var lab = target as LabBuffComponent;
            if (lab != null)
            {
                return (lab.Lab.Tags.EnergyEfficiency & requiredEnergyEfficiency) != 0;
            }
            return false;
        }
    }
}
