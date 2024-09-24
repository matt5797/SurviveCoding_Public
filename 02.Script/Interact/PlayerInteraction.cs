using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.TopDownEngine;

namespace SurviveCoding.Interact
{
    public class PlayerInteraction : MonoBehaviour
    {
        private InputManager inputManager;
        
        private List<IInteractable> interactableObjects = new List<IInteractable>();
        private IInteractable selectedObject;

        private void Start()
        {
            if (InputManager.Instance)
                inputManager = InputManager.Instance;
        }

        private void Update()
        {
            if (inputManager.InteractButton.IsDown)
            {
                InteractWithSelectedObject();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null && interactable.IsActive)
            {
                interactableObjects.Add(interactable);
                interactable.IsVisible = true;
                SelectInteractableObject();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactableObjects.Remove(interactable);
                interactable.IsVisible = false;
                SelectInteractableObject();
            }
        }

        private void SelectInteractableObject()
        {
            IInteractable newSelectedObject = interactableObjects.OrderByDescending(x => x.Priority).FirstOrDefault();

            if (newSelectedObject != selectedObject)
            {
                if (selectedObject != null)
                {
                    selectedObject.IsFocused = false;
                }

                selectedObject = newSelectedObject;

                if (selectedObject != null)
                {
                    selectedObject.IsFocused = true;
                }
            }
        }

        public void InteractWithSelectedObject()
        {
            if (selectedObject != null)
            {
                selectedObject.Interact();
            }
        }
    }
}