using UnityEngine;
using UnityEngine.UI;

namespace SurviveCoding.UI.Lab
{
    public class ProductionItemLine : MonoBehaviour
    {
        [SerializeField]
        private Image[] itemImages;
        
        [SerializeField]
        private Button produceButton;

        [SerializeField]
        private Image produceButtonIcon;

        [SerializeField]
        private Sprite iconProducing;

        [SerializeField]
        private Sprite iconNotProducing;

        [SerializeField]
        private Sprite iconCannotProduce;

        private LabProductionPanel labProductionPanel;

        public int ProductionLevel { get; set; }

        private void Awake()
        {
            labProductionPanel = GetComponentInParent<LabProductionPanel>();
        }

        private void Start()
        {
            produceButton.onClick.AddListener(OnProduceButtonClicked);
        }

        public void SetItemSprites(Sprite[] sprites)
        {
            for (int i = 0; i < itemImages.Length; i++)
            {
                if (i < sprites.Length)
                {
                    itemImages[i].sprite = sprites[i];
                    itemImages[i].gameObject.SetActive(true);
                }
                else
                {
                    itemImages[i].sprite = null;
                    itemImages[i].gameObject.SetActive(false);
                }
            }
        }

        public void Clear()
        {
            foreach (var itemImage in itemImages)
            {
                itemImage.sprite = null;
                itemImage.gameObject.SetActive(false);
            }
        }

        private void OnProduceButtonClicked()
        {
            labProductionPanel.OnProduceButtonClicked(ProductionLevel);
            SFX_Manager.Instance.SFX_Button();
        }

        public void SetProduceButtonIcon(ProduceState state)
        {
            switch (state)
            {
                case ProduceState.Producing:
                    produceButtonIcon.sprite = iconProducing;
                    break;
                case ProduceState.NotProducing:
                    produceButtonIcon.sprite = iconNotProducing;
                    break;
                case ProduceState.CannotProduce:
                    produceButtonIcon.sprite = iconCannotProduce;
                    break;
            }
        }
    }

    public enum ProduceState
    {
        Producing,
        NotProducing,
        CannotProduce
    }
}