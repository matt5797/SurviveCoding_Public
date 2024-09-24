using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "SecurityLevelCondition", menuName = "SurviveCoding/Buff System/Conditions/Security Level")]
    public class SecurityLevelCondition : BuffConditionBase
    {
        public SecurityLevel requiredSecurityLevel;

        public override bool IsValid(IBuffTarget target)
        {
            var lab = target as LabBuffComponent;
            if (lab != null)
            {
                return (lab.Lab.Tags.SecurityLevel & requiredSecurityLevel) != 0;
            }
            return false;
        }
    }
}
