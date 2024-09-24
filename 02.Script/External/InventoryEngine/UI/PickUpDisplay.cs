using MoreMountains.InventoryEngine;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SurviveCoding.External.InventoryEngine
{

    public class PickUpDisplay : MonoBehaviour
    {
        [SerializeField] private Image Icon;
        [SerializeField] private TMP_Text Name;
        [SerializeField] private TMP_Text Quantity;

        public void Display(InventoryItem item, int quantity)
        {
            Icon.sprite = item.Icon;
            Name.text = item.ItemName;
            Quantity.text = quantity.ToString();
        }

        public void AddQuantity(int quantity) => Quantity.text = (int.Parse(Quantity.text) + quantity).ToString();
    }
}
