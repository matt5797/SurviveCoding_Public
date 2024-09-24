using System;
using UnityEngine;
using SurviveCoding.BuffSystem;

namespace SurviveCoding.Player
{
    [RequireComponent(typeof(PlayerStatComponent))]
    [RequireComponent(typeof(PlayerBuffComponent))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerTags tags = new PlayerTags();

        [SerializeField] private PlayerStatComponent statComponent;
        [SerializeField] private PlayerBuffComponent buffComponent;
        
        public event Action OnTagsChanged;

        public static Player Instance { get; private set; }

        public PlayerTags Tags
        {
            get => tags;
            set
            {
                tags = value;
                TagsChanged();
            }
        }

        public PlayerStatComponent StatComponent => statComponent;
        public PlayerBuffComponent BuffComponent => buffComponent;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            if (statComponent == null)
            {
                statComponent = GetComponent<PlayerStatComponent>();
            }
            if (buffComponent == null)
            {
                buffComponent = GetComponent<PlayerBuffComponent>();
            }

            tags.OnTagsChanged += TagsChanged;
        }

        public void ChangeTagValue<T>(string tagName, T value)
        {
            var tagValues = tags.GetTagValues();
            if (tagValues.ContainsKey(tagName))
            {
                tags.SetTagValue(tagName, (Enum)(object)value);
                TagsChanged();
            }
        }

        private void TagsChanged()
        {
            OnTagsChanged?.Invoke();
        }

        public PlayerStatComponent GetStatComponent()
        {
            return statComponent;
        }

        public PlayerBuffComponent GetBuffComponent()
        {
            return buffComponent;
        }
    }
}