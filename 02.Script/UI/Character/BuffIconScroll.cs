using SurviveCoding.UI.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurviveCoding.BuffSystem;
using UnityEngine.UI;
using SurviveCoding.Player;

namespace SurviveCoding.UI.Character
{
    using Player = Player.Player;
    
    public class BuffIconScroll : MonoBehaviour
    {
        [SerializeField] private PlayerBuffComponent playerBuffComponent;
        
        [SerializeField] private RectTransform BuffIconContent;
        [SerializeField] private BuffIcon buffPrefab;
        [SerializeField] private BuffIcon debuffPrefab;
        [SerializeField] private BuffInfo buffInfo;

        private Dictionary<Buff, BuffIcon> buffIconDictionary = new Dictionary<Buff, BuffIcon>();

        private void Awake()
        {
            playerBuffComponent = Player.Instance.BuffComponent;
            playerBuffComponent.OnBuffApplied += AddBuffIcon;
            playerBuffComponent.OnBuffRemoved += RemoveBuffIcon;

            Initialize();
        }

        public void AddBuffIcon(Buff buff)
        {
            if (!buffIconDictionary.ContainsKey(buff))
            {
                BuffIcon prefab = buff.Data.BuffType == BuffType.Buff ? buffPrefab : debuffPrefab;
                BuffIcon buffIcon = Instantiate(prefab, BuffIconContent);
                buffIcon.Buff = buff;
                Button button = buffIcon.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(() => ShowBuffInfo(buff));
                    button.onClick.AddListener(() => SFX_Manager.Instance.SFX_Button());
                }
                buffIconDictionary.Add(buff, buffIcon);
            }
        }

        public void RemoveBuffIcon(Buff buff)
        {
            if (buffIconDictionary.TryGetValue(buff, out var buffIcon))
            {
                buffIconDictionary.Remove(buff);
                Destroy(buffIcon.gameObject);
            }
        }

        public void Initialize()
        {
            foreach (var buff in playerBuffComponent.Buffs)
            {
                AddBuffIcon(buff);
            }
        }

        public void ShowBuffInfo(Buff buff)
        {
            //buffInfo.SetBuffInfo(buff.Data.BuffName, buff.Data.Description, buff.Data.BuffType == BuffType.Buff);
            buffInfo.SetBuffInfo(buff);
            buffInfo.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            playerBuffComponent.OnBuffApplied -= AddBuffIcon;
            playerBuffComponent.OnBuffRemoved -= RemoveBuffIcon;
        }
    }
}