using System.Collections.Generic;
using UnityEngine;

namespace SurviveCoding.BuffSystem
{
    public class BuffProvider : MonoBehaviour
    {
        [SerializeField]
        private bool applyBuffsOnEnable = true;

        [SerializeField]
        private List<BuffData> buffDataList = new List<BuffData>();

        private List<Buff> activeBuffs = new List<Buff>();

        private void OnEnable()
        {
            if (applyBuffsOnEnable)
            {
                ApplyBuffs();
            }
        }

        private void OnDisable()
        {
            RemoveBuffs();
        }

        public void ApplyBuffs()
        {
            foreach (var buffData in buffDataList)
            {
                Buff newBuff = BuffManager.Instance.CreateAndApplyBuff(buffData);
                if (newBuff != null)
                {
                    activeBuffs.Add(newBuff);
                }
            }
        }

        public void RemoveBuffs()
        {
            foreach (var buff in activeBuffs)
            {
                BuffManager.Instance.RemoveBuff(buff);
            }
            activeBuffs.Clear();
        }

        public void AddBuffData(BuffData buffData)
        {
            if (!buffDataList.Contains(buffData))
            {
                buffDataList.Add(buffData);
            }
        }

        public void RemoveBuffData(BuffData buffData)
        {
            buffDataList.Remove(buffData);
        }

        public void ClearBuffDataList()
        {
            buffDataList.Clear();
        }
    }
}