using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.Player;
using TMPro;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public PlayerStatComponent statComponent;
    public PlayerStat stat;
    public TextMeshProUGUI mainHealthText;
    public TextMeshProUGUI healthText;

    public Slider mainSlider;
    public Slider slider;

    public Slider radLevelSlider;
    public Slider hungerSlider;
    public PlayerStat hungerStat;
    public PlayerStat radLevelStat;

 /*   public GameObject gameover;
    public GameObject deathSplash;*/

    public GUIManagerChild manager;

    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        if (statComponent != null)
        {
            stat = statComponent.GetStat(PlayerStatType.Health); // stat 초기화
            stat.OnValueChanged += UpdateHealthUI;
            UpdateHealthUI(stat.CurrentValue); // 초기값 설정
            hungerStat = statComponent.GetStat(PlayerStatType.Hunger);


            hungerStat = statComponent.GetStat(PlayerStatType.Hunger);
            hungerSlider.maxValue = hungerStat.MaxValue;
            hungerStat.OnValueChanged += UpdateHungerSlider;
            UpdateHungerSlider(hungerStat.CurrentValue); // 초기값 설정

            radLevelStat = statComponent.GetStat(PlayerStatType.Pollution);
            radLevelSlider.maxValue = radLevelStat.MaxValue;
            radLevelStat.OnValueChanged += UpdateradLevelSlider;
            UpdateradLevelSlider(radLevelStat.CurrentValue); //

        }
        else
        {
            Debug.LogError("PlayerStatComponent is not assigned.");
        }
    }

    public void IsDead()
    {
        isDead = false;
    }
    private void UpdateradLevelSlider(float newValue)
    {
        radLevelSlider.value = newValue;
    }

    // Hunger 이벤트 핸들러 메서드
    private void UpdateHungerSlider(float newValue)
    {
        hungerSlider.value = newValue;
    }

    // 이벤트 핸들러 메서드
    private void UpdateHealthUI(float newValue)
    {
        mainHealthText.text = newValue.ToString() + "/" + stat.MaxValue.ToString();
        healthText.text = newValue.ToString() + "/" + stat.MaxValue.ToString();
        mainSlider.value = newValue;
        Debug.Log(mainSlider.value + ":madins" + newValue + "newval");
        slider.value = newValue;
        if (newValue == 0 && !isDead)
        {
            isDead = true; // 플레이어가 죽었음을 표시
            Debug.Log("dddddddddddddddddddd");
            manager.SetDeathScreen(true);
        }

    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (stat != null)
        {
            stat.OnValueChanged -= UpdateHealthUI;
        }
    }
    // Update is called once per frame
    void Update()
    {/*
        mainHealthText.text = stat.BaseValue.ToString();
        healthText.text = stat.BaseValue.ToString();*/
    }
}
