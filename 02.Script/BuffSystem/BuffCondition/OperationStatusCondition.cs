using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "OperationStatusCondition", menuName = "SurviveCoding/Buff System/Conditions/Operation Status")]
    public class OperationStatusCondition : BuffConditionBase
    {
        public OperationStatus requiredOperationStatus;

        public override bool IsValid(IBuffTarget target)
        {
            var lab = target as LabBuffComponent;
            if (lab != null)
            {
                return (lab.Lab.Tags.OperationStatus & requiredOperationStatus) != 0;
            }
            return false;
        }
    }
}
