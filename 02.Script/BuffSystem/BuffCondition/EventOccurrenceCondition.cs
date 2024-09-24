using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;

namespace SurviveCoding.BuffSystem
{
    [CreateAssetMenu(fileName = "EventOccurrenceCondition", menuName = "SurviveCoding/Buff System/Conditions/Event Occurrence")]
    public class EventOccurrenceCondition : BuffConditionBase
    {
        public EventOccurrence requiredEventOccurrence;

        public override bool IsValid(IBuffTarget target)
        {
            var lab = target as LabBuffComponent;
            if (lab != null)
            {
                return (lab.Lab.Tags.EventOccurrence & requiredEventOccurrence) != 0;
            }
            return false;
        }
    }
}
