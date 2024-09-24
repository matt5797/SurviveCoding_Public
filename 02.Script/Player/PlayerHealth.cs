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
            stat = statComponent.GetStat(PlayerStatType.Health); // stat �ʱ�ȭ
            stat.OnValueChanged += UpdateHealthUI;
            UpdateHealthUI(stat.CurrentValue); // �ʱⰪ ����
            hungerStat = statComponent.GetStat(PlayerStatType.Hunger);


            hungerStat = statComponent.GetStat(PlayerStatType.Hunger);
            hungerSlider.maxValue = hungerStat.MaxValue;
            hungerStat.OnValueChanged += UpdateHungerSlider;
            UpdateHungerSlider(hungerStat.CurrentValue); // �ʱⰪ ����

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

    // Hunger �̺�Ʈ �ڵ鷯 �޼���
    private void UpdateHungerSlider(float newValue)
    {
        hungerSlider.value = newValue;
    }

    // �̺�Ʈ �ڵ鷯 �޼���
    private void UpdateHealthUI(float newValue)
    {
        mainHealthText.text = newValue.ToString() + "/" + stat.MaxValue.ToString();
        healthText.text = newValue.ToString() + "/" + stat.MaxValue.ToString();
        mainSlider.value = newValue;
        Debug.Log(mainSlider.value + ":madins" + newValue + "newval");
        slider.value = newValue;
        if (newValue == 0 && !isDead)
        {
            isDead = true; // �÷��̾ �׾����� ǥ��
            Debug.Log("dddddddddddddddddddd");
            manager.SetDeathScreen(true);
        }

    }

    private void OnDestroy()
    {
        // �̺�Ʈ ���� ����
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
