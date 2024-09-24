using UnityEngine;
using UnityEngine.UI;
using SurviveCoding.Player;
using TMPro;

namespace SurviveCoding.UI.Character
{
    public class PlayerHPBar : MonoBehaviour
    {
        private Player.Player player;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Image fillImage;
        [SerializeField] private Color normalColor = Color.green;
        [SerializeField] private Color warningColor = Color.yellow;
        [SerializeField] private Color dangerColor = Color.red;
        [SerializeField] private float warningThreshold = 0.5f;
        [SerializeField] private float dangerThreshold = 0.25f;
        [SerializeField] private TextMeshProUGUI hpText;

        private void Start()
        {
            player = Player.Player.Instance;
            if (player != null)
            {
                UpdateHPBar(player.StatComponent.GetStat(PlayerStatType.Health).CurrentValue);
                UpdateHPText(player.StatComponent.GetStat(PlayerStatType.Health).CurrentValue);
                player.StatComponent.GetStat(PlayerStatType.Health).OnValueChanged += UpdateHPBar;
                player.StatComponent.GetStat(PlayerStatType.Health).OnValueChanged += UpdateHPText;
            }
        }

        private void UpdateHPBar(float value)
        {
            hpSlider.value = value / player.StatComponent.GetStat(PlayerStatType.Health).MaxValue;

            if (hpSlider.value <= dangerThreshold)
            {
                fillImage.color = dangerColor;
            }
            else if (hpSlider.value <= warningThreshold)
            {
                fillImage.color = warningColor;
            }
            else
            {
                fillImage.color = normalColor;
            }
        }

        private void UpdateHPText(float value)
        {
            hpText.text = $"{(int)value} / {player.StatComponent.GetStat(PlayerStatType.Health).MaxValue}";
        }
    }
}