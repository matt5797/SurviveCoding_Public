using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.Player
{
    public enum PlayerStatType
    {
        Health,
        Hunger,
        Pollution,
        HungerIncrease,
        PollutionIncrease
    }

    public class PlayerStatComponent : MonoBehaviour
    {
        [SerializeField] private float updateInterval = 1.0f;

        [SerializeField] private PlayerStat health;
        [SerializeField] private PlayerStat hunger;
        [SerializeField] private PlayerStat pollution;
        [SerializeField] private PlayerStat hungerIncrease;
        [SerializeField] private PlayerStat pollutionIncrease;

        private Dictionary<PlayerStatType, PlayerStat> statDictionary = new Dictionary<PlayerStatType, PlayerStat>();

        private void Awake()
        {
            health.Initialize();
            hunger.Initialize();
            pollution.Initialize();
            hungerIncrease.Initialize();
            pollutionIncrease.Initialize();

            statDictionary.Add(PlayerStatType.Health, health);
            statDictionary.Add(PlayerStatType.Hunger, hunger);
            statDictionary.Add(PlayerStatType.Pollution, pollution);
            statDictionary.Add(PlayerStatType.HungerIncrease, hungerIncrease);
            statDictionary.Add(PlayerStatType.PollutionIncrease, pollutionIncrease);

            health.OnValueChanged += OnHealthChanged;
            hunger.OnValueChanged += OnHungerChanged;
            pollution.OnValueChanged += OnPollutionChanged;

            StartCoroutine(UpdateStats());
        }

        public PlayerStat GetStat(PlayerStatType statType)
        {
            if (statDictionary.TryGetValue(statType, out var stat))
            {
                return stat;
            }
            else
            {
                Debug.LogError($"Stat '{statType}' not found in PlayerStatComponent.");
                return null;
            }
        }

        public void AddPercentModifierToStat(PlayerStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.AddPercentModifier(value));
        }

        public void RemovePercentModifierFromStat(PlayerStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.RemovePercentModifier(value));
        }

        public void AddFlatModifierToStat(PlayerStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.AddFlatModifier(value));
        }

        public void RemoveFlatModifierFromStat(PlayerStatType statType, float value)
        {
            ModifyStat(statType, stat => stat.RemoveFlatModifier(value));
        }

        private void ModifyStat(PlayerStatType statType, Action<PlayerStat> modifyAction)
        {
            if (statDictionary.TryGetValue(statType, out var stat))
            {
                modifyAction(stat);
            }
            else
            {
                throw new ArgumentException($"Stat '{statType}' not found in PlayerStatComponent.");
            }
        }

        public void ResetStatModifiers(PlayerStatType statType)
        {
            if (statDictionary.TryGetValue(statType, out var stat))
            {
                stat.ResetModifiers();
            }
        }

        private IEnumerator UpdateStats()
        {
            while (true)
            {
                // 외부 환경에 따른 공복도와 오염도 증가치 적용
                float hungerIncreaseValue = GetStat(PlayerStatType.HungerIncrease).CalculateValue();
                float pollutionIncreaseValue = GetStat(PlayerStatType.PollutionIncrease).CalculateValue();

                GetStat(PlayerStatType.Hunger).CurrentValue += hungerIncreaseValue;
                GetStat(PlayerStatType.Pollution).CurrentValue += pollutionIncreaseValue;

                yield return new WaitForSeconds(updateInterval);
            }
        }

        private void OnHealthChanged(float value)
        {
            PlayerTags tags = GetComponent<Player>().Tags;
            HealthLevel newHealthLevel = HealthLevel.None;
            if (value < 30f)
                newHealthLevel = HealthLevel.Low;
            else if (value < 70f)
                newHealthLevel = HealthLevel.Medium;
            else
                newHealthLevel = HealthLevel.High;

            if (newHealthLevel != tags.HealthLevel)
                tags.HealthLevel = newHealthLevel;
        }

        private void OnHungerChanged(float value)
        {
            PlayerTags tags = GetComponent<Player>().Tags;
            HungerLevel newHungerLevel = HungerLevel.None;
            if (value < 30f)
                newHungerLevel = HungerLevel.Low;
            else if (value < 70f)
                newHungerLevel = HungerLevel.Medium;
            else
                newHungerLevel = HungerLevel.High;

            if (newHungerLevel != tags.HungerLevel)
                tags.HungerLevel = newHungerLevel;
        }

        private void OnPollutionChanged(float value)
        {
            PlayerTags tags = GetComponent<Player>().Tags;
            PollutionLevel newPollutionLevel = PollutionLevel.None;
            if (value < 30f)
                newPollutionLevel = PollutionLevel.Low;
            else if (value < 70f)
                newPollutionLevel = PollutionLevel.Medium;
            else
                newPollutionLevel = PollutionLevel.High;

            if (newPollutionLevel != tags.PollutionLevel)
                tags.PollutionLevel = newPollutionLevel;
        }
    }
}