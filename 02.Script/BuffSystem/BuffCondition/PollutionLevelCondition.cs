using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "PollutionLevelCondition", menuName = "SurviveCoding/Buff System/Conditions/Pollution Level")]
    public class PollutionLevelCondition : BuffConditionBase
    {
        public PollutionLevel requiredPollutionLevel;

        public override bool IsValid(IBuffTarget target)
        {
            var lab = target as LabBuffComponent;
            if (lab != null)
            {
                return (lab.Lab.Tags.PollutionLevel & requiredPollutionLevel) != 0;
            }
            return false;
        }
    }
}
