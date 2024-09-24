using UnityEngine;
using UnityEditor;
using SurviveCoding.LabSystem;
using SurviveCoding.BuffSystem;

namespace SurviveCoding.Test
{
#if UNITY_EDITOR
    public class LabTestEditor : EditorWindow
    {
        private Lab[] labList;
        private int selectedLabIndex;
        private bool isEventRegistered = false;
        private bool showLabTags = true;
        private bool showLabStats = true;
        private int selectedProductionLevel = 0;

        [MenuItem("SurviveCoding/Lab Test Editor")]
        public static void ShowWindow()
        {
            GetWindow<LabTestEditor>("Lab Test Editor");
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            UpdateLabList();
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            UnregisterEvents();
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                UpdateLabList();
                RegisterEvents();
            }
            else if (state == PlayModeStateChange.ExitingPlayMode)
            {
                UnregisterEvents();
            }
        }

        private void OnGUI()
        {
            if (!Application.isPlaying)
                return;

            selectedLabIndex = EditorGUILayout.Popup("Selected Lab", selectedLabIndex, GetLabNames());
            Lab selectedLab = GetSelectedLab();

            if (selectedLab == null)
            {
                UnregisterEvents();
                return;
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Register Lab Events"))
            {
                RegisterEvents();
            }
            if (GUILayout.Button("Unregister Lab Events"))
            {
                UnregisterEvents();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Toggle Lab Activation"))
            {
                selectedLab.IsActive = !selectedLab.IsActive;
            }

            EditorGUILayout.Space();

            showLabTags = EditorGUILayout.Foldout(showLabTags, "Lab Tags");
            if (showLabTags)
            {
                EditorGUI.indentLevel++;
                ChangeTagValue("ResearchField", (ResearchField)EditorGUILayout.EnumFlagsField("Research Field", selectedLab.Tags.ResearchField));
                ChangeTagValue("SecurityLevel", (SecurityLevel)EditorGUILayout.EnumFlagsField("Security Level", selectedLab.Tags.SecurityLevel));
                ChangeTagValue("OperationStatus", (OperationStatus)EditorGUILayout.EnumFlagsField("Operation Status", selectedLab.Tags.OperationStatus));
                ChangeTagValue("UpgradeStage", (UpgradeStage)EditorGUILayout.EnumFlagsField("Upgrade Stage", selectedLab.Tags.UpgradeStage));
                ChangeTagValue("EnergyEfficiency", (EnergyEfficiency)EditorGUILayout.EnumFlagsField("Energy Efficiency", selectedLab.Tags.EnergyEfficiency));
                ChangeTagValue("ProductionEfficiency", (ProductionEfficiency)EditorGUILayout.EnumFlagsField("Production Efficiency", selectedLab.Tags.ProductionEfficiency));
                ChangeTagValue("Durability", (Durability)EditorGUILayout.EnumFlagsField("Durability", selectedLab.Tags.Durability));
                ChangeTagValue("PollutionLevel", (PollutionLevel)EditorGUILayout.EnumFlagsField("Pollution Level", selectedLab.Tags.PollutionLevel));
                ChangeTagValue("EventOccurrence", (EventOccurrence)EditorGUILayout.EnumFlagsField("Event Occurrence", selectedLab.Tags.EventOccurrence));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            showLabStats = EditorGUILayout.Foldout(showLabStats, "Lab Stats");
            if (showLabStats)
            {
                EditorGUI.indentLevel++;
                LabStatComponent statComponent = selectedLab.GetStatComponent();
                ChangeStatValue(LabStatType.EnergyConsumption, EditorGUILayout.FloatField("Energy Consumption", statComponent.GetStat(LabStatType.EnergyConsumption).CurrentValue));
                ChangeStatValue(LabStatType.Durability, EditorGUILayout.FloatField("Durability", statComponent.GetStat(LabStatType.Durability).CurrentValue));
                ChangeStatValue(LabStatType.PollutionLevel, EditorGUILayout.FloatField("Pollution Level", statComponent.GetStat(LabStatType.PollutionLevel).CurrentValue));
                ChangeStatValue(LabStatType.EnergyEfficiency, EditorGUILayout.FloatField("Energy Efficiency", statComponent.GetStat(LabStatType.EnergyEfficiency).CurrentValue));
                ChangeStatValue(LabStatType.ProductionEfficiency, EditorGUILayout.FloatField("Production Efficiency", statComponent.GetStat(LabStatType.ProductionEfficiency).CurrentValue));
                ChangeStatValue(LabStatType.MaterialProductionEfficiency, EditorGUILayout.FloatField("Material Production Efficiency", statComponent.GetStat(LabStatType.MaterialProductionEfficiency).CurrentValue));
                ChangeStatValue(LabStatType.RareMaterialProductionEfficiency, EditorGUILayout.FloatField("Rare Material Production Efficiency", statComponent.GetStat(LabStatType.RareMaterialProductionEfficiency).CurrentValue));
                ChangeStatValue(LabStatType.PollutionIncrease, EditorGUILayout.FloatField("Pollution Increase", statComponent.GetStat(LabStatType.PollutionIncrease).CurrentValue));
                ChangeStatValue(LabStatType.DurabilityDecrease, EditorGUILayout.FloatField("Durability Decrease", statComponent.GetStat(LabStatType.DurabilityDecrease).CurrentValue));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Attempt Upgrade"))
            {
                selectedLab.AttemptUpgrade();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Production", EditorStyles.boldLabel);
            
            if (selectedLab != null)
            {
                LabUpgradeComponent upgradeComponent = selectedLab.GetUpgradeComponent();
                int maxProductionLevel = upgradeComponent.CurrentLevel;

                selectedProductionLevel = EditorGUILayout.IntSlider("Production Level", selectedProductionLevel, 0, maxProductionLevel);

                if (GUILayout.Button("Set Production Level"))
                {
                    selectedLab.GetProductionComponent().SetSelectedProductionLevel(selectedProductionLevel);
                }
            }
            
            if (GUILayout.Button("Produce Items"))
            {
                selectedLab.GetProductionComponent().UpdateProduction();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
        }

        private void UpdateLabList()
        {
            labList = FindObjectsOfType<Lab>();
        }

        private string[] GetLabNames()
        {
            string[] labNames = new string[labList.Length];
            for (int i = 0; i < labList.Length; i++)
            {
                if (labList[i]!=null)
                    labNames[i] = labList[i].name;
            }
            return labNames;
        }

        private Lab GetSelectedLab()
        {
            return selectedLabIndex >= 0 && selectedLabIndex < labList.Length ? labList[selectedLabIndex] : null;
        }

        private void ChangeTagValue<T>(string tagName, T value)
        {
            Lab selectedLab = GetSelectedLab();
            if (selectedLab != null)
            {
                var tagValues = selectedLab.Tags.GetTagValues();
                if (tagValues.TryGetValue(tagName, out var currentValue))
                {
                    if (!Equals(currentValue, value))
                    {
                        selectedLab.ChangeTagValue(tagName, value);
                    }
                }
            }
        }

        private void ChangeStatValue(LabStatType statType, float value)
        {
            Lab selectedLab = GetSelectedLab();
            if (selectedLab != null)
            {
                LabStat stat = selectedLab.GetStatComponent().GetStat(statType);
                if (stat.CurrentValue != value)
                {
                    stat.CurrentValue = value;
                }
            }
        }

        private void RegisterEvents()
        {
            Lab selectedLab = GetSelectedLab();
            if (!isEventRegistered && selectedLab != null)
            {
                selectedLab.OnLabActivated += OnLabActivated;
                selectedLab.OnLabDeactivated += OnLabDeactivated;
                selectedLab.GetBuffComponent().OnBuffApplied += OnBuffApplied;
                selectedLab.GetBuffComponent().OnBuffRemoved += OnBuffRemoved;
                selectedLab.GetUpgradeComponent().OnUpgradeSucceeded += OnUpgradeSucceeded;
                isEventRegistered = true;
                Debug.Log("Registered Lab Events");
            }
        }

        private void UnregisterEvents()
        {
            Lab selectedLab = GetSelectedLab();
            if (isEventRegistered && selectedLab != null)
            {
                selectedLab.OnLabActivated -= OnLabActivated;
                selectedLab.OnLabDeactivated -= OnLabDeactivated;
                selectedLab.GetBuffComponent().OnBuffApplied -= OnBuffApplied;
                selectedLab.GetBuffComponent().OnBuffRemoved -= OnBuffRemoved;
                selectedLab.GetUpgradeComponent().OnUpgradeSucceeded -= OnUpgradeSucceeded;
                isEventRegistered = false;
                Debug.Log("Unregistered Lab Events");
            }
        }

        private void OnLabActivated()
        {
            Lab selectedLab = GetSelectedLab();
            if (selectedLab != null)
            {
                Debug.Log($"{selectedLab.name} activated");
            }
        }

        private void OnLabDeactivated()
        {
            Lab selectedLab = GetSelectedLab();
            if (selectedLab != null)
            {
                Debug.Log($"{selectedLab.name} deactivated");
            }
        }

        private void OnBuffApplied(Buff buff)
        {
            Lab selectedLab = GetSelectedLab();
            if (selectedLab != null)
            {
                Debug.Log($"Buff {buff.Data.BuffName} applied to {selectedLab.name}");
            }
        }

        private void OnBuffRemoved(Buff buff)
        {
            Lab selectedLab = GetSelectedLab();
            if (selectedLab != null)
            {
                Debug.Log($"Buff {buff.Data.BuffName} removed from {selectedLab.name}");
            }
        }

        private void OnUpgradeSucceeded(int level)
        {
            Lab selectedLab = GetSelectedLab();
            if (selectedLab != null)
            {
                Debug.Log($"{selectedLab.name} upgraded to level {level}");
            }
        }
    }
#endif
}