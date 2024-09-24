using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "ResearchFieldCondition", menuName = "SurviveCoding/Buff System/Conditions/Research Field")]
    public class ResearchFieldCondition : BuffConditionBase
    {
        public ResearchField requiredField;

        public override bool IsValid(IBuffTarget target)
        {
            var lab = target as LabBuffComponent;
            if (lab != null)
            {
                return (lab.Lab.Tags.ResearchField & requiredField) != 0;
            }
            return false;
        }
    }
}
