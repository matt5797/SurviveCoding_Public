using TMPro;
using UnityEngine;

namespace SurviveCoding.Interact
{
    public class LabTerminal : InteractableObject
    {
        [SerializeField]
        private Canvas terminalCanvas;
        
        [SerializeField]
        private TextMeshProUGUI text;

        protected override void Awake()
        {
            base.Awake();
            terminalCanvas.enabled = false;

            //text.text = "������ ����";
        }

        public override void OnVisibleChanged(bool isVisible)
        {
            base.OnVisibleChanged(isVisible);
            terminalCanvas.enabled = isVisible;
        }
    }
}