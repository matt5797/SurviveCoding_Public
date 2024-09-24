using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "UpgradeStageCondition", menuName = "SurviveCoding/Buff System/Conditions/Upgrade Stage")]
    public class UpgradeStageCondition : BuffConditionBase
    {
        public UpgradeStage requiredUpgradeStage;

        public override bool IsValid(IBuffTarget target)
        {
            var lab = target as LabBuffComponent;
            if (lab != null)
            {
                return (lab.Lab.Tags.UpgradeStage & requiredUpgradeStage) != 0;
            }
            return false;
        }
    }
}
