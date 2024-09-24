using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SurviveCoding.Player;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;

namespace SurviveCoding.UI.Character
{
    public class PlayerTagIcon : MonoBehaviour
    {
        [SerializeField]
        private Player.Player player;
        
        [SerializeField] private Image iconImage;
        [SerializeField] private PlayerStatType statType;
        [SerializeField] private Sprite[] iconSprites; // Enum 값 순서로 배열
        
        private void Start()
        {
            player = Player.Player.Instance;

            if (player != null)
            {
                player.OnTagsChanged += UpdateIconSprite;
            }
            
            UpdateIconSprite();
        }
        
        private void UpdateIconSprite()
        {
            int iconIndex = 0;
            switch (statType)
            {
                case PlayerStatType.Health:
                    iconIndex = (int)player.Tags.HealthLevel;
                    break;
                case PlayerStatType.Hunger:
                    iconIndex = (int)player.Tags.HungerLevel;
                    break;
                case PlayerStatType.Pollution:
                    iconIndex = (int)player.Tags.PollutionLevel;
                    break;
                default:
                    Debug.LogError("Unsupported PlayerStatType in PlayerTagIcon.");
                    return;
            }

            iconIndex = iconIndex==0 ? 0 : (int)Mathf.Log(iconIndex, 2) + 1;

            iconImage.sprite = iconSprites[iconIndex];
        }
    }
}