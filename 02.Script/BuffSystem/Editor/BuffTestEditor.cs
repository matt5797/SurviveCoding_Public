using UnityEngine;
using UnityEditor;
using SurviveCoding.BuffSystem;
using System.Collections.Generic;
using System;

namespace SurviveCoding.Test
{
    public class BuffTestEditor : EditorWindow
    {
        private BuffManager buffManager;
        private List<IBuffTarget> buffTargets = new List<IBuffTarget>();
        private List<BuffData> buffDataList = new List<BuffData>();
        private BuffData selectedBuffData;
        private IBuffTarget selectedBuffTarget;
        private string selectedBuffId;
        private Vector2 scrollPos;

        [MenuItem("SurviveCoding/Buff Test Editor")]
        public static void ShowWindow()
        {
            GetWindow<BuffTestEditor>("Buff Test Editor");
        }

        private void OnGUI()
        {
            if (!Application.isPlaying)
                return;

            buffManager = FindObjectOfType<BuffManager>();
            if (buffManager == null)
            {
                Debug.LogError("BuffManager not found in the scene.");
                return;
            }

            buffDataList.AddRange(Resources.LoadAll<BuffData>(""));

            if (buffManager == null)
            {
                EditorGUILayout.HelpBox("BuffManager not found in the scene.", MessageType.Error);
                return;
            }

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            GUILayout.Label("Buff Creation and Application", EditorStyles.boldLabel);
            selectedBuffData = (BuffData)EditorGUILayout.ObjectField("Buff Data", selectedBuffData, typeof(BuffData), false);
            if (GUILayout.Button("Create and Apply Buff"))
            {
                if (selectedBuffData != null)
                {
                    buffManager.CreateAndApplyBuff(selectedBuffData);
                }
                else
                {
                    Debug.LogWarning("Please select a BuffData to create and apply a Buff.");
                }
            }

            GUILayout.Label("Buff Removal", EditorStyles.boldLabel);
            selectedBuffId = EditorGUILayout.TextField("Buff ID", selectedBuffId);
            if (GUILayout.Button("Remove Buff"))
            {
                if (!string.IsNullOrEmpty(selectedBuffId))
                {
                    Buff buff = buffManager.Buffs.Find(b => b.Data.BuffId == selectedBuffId);
                    if (buff != null)
                    {
                        buffManager.RemoveBuff(buff);
                    }
                    else
                    {
                        Debug.LogWarning($"Buff with ID '{selectedBuffId}' not found.");
                    }
                }
                else
                {
                    Debug.LogWarning("Please enter a Buff ID to remove.");
                }
            }

            GUILayout.Label("Registered Buff Targets", EditorStyles.boldLabel);
            buffTargets = buffManager.BuffTargets;
            if (buffTargets.Count > 0)
            {
                foreach (IBuffTarget buffTarget in buffTargets)
                {
                    EditorGUILayout.ObjectField(buffTarget.ToString(), buffTarget as UnityEngine.Object, typeof(IBuffTarget), true);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No registered Buff Targets found.", MessageType.Info);
            }

            EditorGUILayout.EndScrollView();
        }
    }
}