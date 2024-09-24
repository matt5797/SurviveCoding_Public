using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.LabSystem;
using TMPro;

namespace SurviveCoding.UI.Lab
{
    public class LabStatePanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text descriptionText;

        [SerializeField]
        [TextArea]
        private string defaultText;

        public void SetTargetLab(LabSystem.Lab lab)
        {
            UpdateLabStatePanel();
        }

        private void UpdateLabStatePanel()
        {
            descriptionText.text = defaultText;
        }

        public void UpdateDescription(string desctription)
        {
            descriptionText.text = desctription;
        }
    }
}