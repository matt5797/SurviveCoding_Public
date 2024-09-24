using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SurviveCoding.BuffSystem;

namespace SurviveCoding.UI.Character
{
    public class BuffIcon : MonoBehaviour
    {
        [SerializeField]
        private Image iconImage;

        [SerializeField]
        private TextMeshProUGUI timerText;

        private Buff buff;

        public Buff Buff
        {
            get => buff;
            set
            {
                buff = value;
                iconImage.sprite = buff.Data.Icon;
                if (buff.IsPersistent)
                {
                    timerText.gameObject.SetActive(false);
                }
                else
                {
                    timerText.text = buff.RemainingDuration.ToString("F1");
                }
            }
        }

        public void OnClick()
        {
            if (buff != null)
            {
                
            }
        }
    }
}