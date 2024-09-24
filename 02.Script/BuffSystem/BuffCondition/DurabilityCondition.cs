using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "DurabilityCondition", menuName = "SurviveCoding/Buff System/Conditions/Durability")]
    public class DurabilityCondition : BuffConditionBase
    {
        public Durability requiredDurability;

        public override bool IsValid(IBuffTarget target)
        {
            var lab = target as LabBuffComponent;
            if (lab != null)
            {
                return (lab.Lab.Tags.Durability & requiredDurability) != 0;
            }
            return false;
        }
    }
}
