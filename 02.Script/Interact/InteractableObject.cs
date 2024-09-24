using UnityEngine;
using UnityEngine.Events;

namespace SurviveCoding.Interact
{
    [System.Serializable]
    [RequireComponent(typeof(Collider))]
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private bool isActive = true;
        [SerializeField]
        private bool isDisabled = false;
        
        private bool isVisible = false;
        private bool isInteractable = true;
        private bool isFocused = false;

        [SerializeField]
        private int priority = 0;

        [SerializeField]
        private UnityEvent onInteraction = new UnityEvent();
        [SerializeField]
        private UnityEvent onVisible = new UnityEvent();
        [SerializeField]
        private UnityEvent onInvisible = new UnityEvent();
        [SerializeField]
        private UnityEvent onFocus = new UnityEvent();
        [SerializeField]
        private UnityEvent onActive = new UnityEvent();
        [SerializeField]
        private UnityEvent onInactive = new UnityEvent();
        [SerializeField]
        private UnityEvent onDisabled = new UnityEvent();

        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                if (isActive)
                {
                    onActive.Invoke();
                }
                else
                {
                    onInactive.Invoke();
                }
            }
        }

        public bool IsDisabled
        {
            get => isDisabled;
            set
            {
                isDisabled = value;
                if (isDisabled)
                {
                    onDisabled.Invoke();
                }
            }
        }

        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    OnVisibleChanged(isVisible);
                }
            }
        }

        public bool IsFocused
        {
            get => isFocused;
            set
            {
                isFocused = value;
                if (isFocused)
                {
                    onFocus.Invoke();
                }
            }
        }

        public int Priority
        {
            get => priority;
            set => priority = value;
        }

        public UnityEvent OnInteraction => onInteraction;
        public UnityEvent OnVisible => onVisible;
        public UnityEvent OnInvisible => onInvisible;
        public UnityEvent OnActive => onActive;
        public UnityEvent OnInactive => onInactive;
        public UnityEvent OnFocused => onFocus;
        public UnityEvent OnDisabled => onDisabled;

        protected virtual void Awake()
        {
            // 초기 상태 설정
            IsVisible = false;
        }

        public virtual void OnVisibleChanged(bool isVisible)
        {
            if (isVisible)
            {
                onVisible.Invoke();
            }
            else
            {
                onInvisible.Invoke();
            }
        }

        public virtual void Interact()
        {
            if (!IsDisabled)
            {
                OnInteraction.Invoke();
            }
        }
    }
}