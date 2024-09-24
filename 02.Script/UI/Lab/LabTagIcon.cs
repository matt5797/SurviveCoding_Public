using System;
using System.Collections;
using System.Collections.Generic;
using SurviveCoding.LabSystem;
using UnityEngine;
using UnityEngine.UI;

namespace SurviveCoding.UI.Lab
{
    using Lab = LabSystem.Lab;
    
    public class LabTagIcon : MonoBehaviour
    {
        [SerializeField]
        private Lab targetLab;

        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private Image iconImage;

        [SerializeField]
        private TagColorMapping[] tagColorMappings;

        private Dictionary<string, Color> colorDictionary = new Dictionary<string, Color>();

        private void Awake()
        {
            // �±� ���� ���� ��ųʸ� �ʱ�ȭ
            foreach (var mapping in tagColorMappings)
            {
                colorDictionary[mapping.tagValue] = mapping.color;
            }
        }

        private void OnEnable()
        {
            if (targetLab != null)
            {
                // Lab�� �±� ���� �̺�Ʈ ����
                targetLab.OnTagsChanged += UpdateTagIcon;
                UpdateTagIcon();
            }
        }

        private void OnDisable()
        {
            if (targetLab != null)
            {
                // Lab�� �±� ���� �̺�Ʈ ���� ����
                targetLab.OnTagsChanged -= UpdateTagIcon;
            }
        }

        public void SetTargetLab(Lab lab)
        {
            if (targetLab != null)
            {
                targetLab.OnTagsChanged -= UpdateTagIcon;
            }

            targetLab = lab;

            if (targetLab != null)
            {
                targetLab.OnTagsChanged += UpdateTagIcon;
                UpdateTagIcon();
            }
        }

        private void UpdateTagIcon()
        {
            if (targetLab != null && iconImage != null && backgroundImage != null)
            {
                var tagValues = targetLab.Tags.GetTagValues();

                foreach (var mapping in tagColorMappings)
                {
                    if (tagValues.TryGetValue(mapping.tagName, out Enum tagValue))
                    {
                        string tagValueString = tagValue.ToString();
                        if (colorDictionary.TryGetValue(tagValueString, out Color color))
                        {
                            iconImage.color = color;
                            backgroundImage.color = color;
                            return;
                        }
                    }
                }

                // ���ε� �±� ���� ���� ��� �⺻ �������� ����
                iconImage.color = Color.white;
                backgroundImage.color = Color.white;
            }
        }
    }

    [Serializable]
    public struct TagColorMapping
    {
        public string tagName;
        public string tagValue;
        public Color color;
    }
}