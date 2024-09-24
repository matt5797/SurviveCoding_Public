using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "ProductionEfficiencyCondition", menuName = "SurviveCoding/Buff System/Conditions/Production Efficiency")]
    public class ProductionEfficiencyCondition : BuffConditionBase
    {
        public ProductionEfficiency requiredProductionEfficiency;

        public override bool IsValid(IBuffTarget target)
        {
            var lab = target as LabBuffComponent;
            if (lab != null)
            {
                return (lab.Lab.Tags.ProductionEfficiency & requiredProductionEfficiency) != 0;
            }
            return false;
        }
    }
}
