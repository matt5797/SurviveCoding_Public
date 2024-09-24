using UnityEngine;
using UnityEngine.Events;

namespace SurviveCoding.Interact
{
    public interface IInteractable
    {
        bool IsActive { get; set; }
        bool IsVisible { get; set; }
        bool IsFocused { get; set; }
        bool IsDisabled { get; set; }
        int Priority { get; set; }

        void Interact();
    }
}