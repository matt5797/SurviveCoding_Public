using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MoreMountains.Tools;
using TMPro;
using System.Linq;
using MoreMountains.InventoryEngine;



namespace SurviveCoding.External.InventoryEngine
{
    public class CraftDetails : MonoBehaviour
    {
        [SerializeField]
        private GameObject craftDetails;

        public Image RecipeIcon;
        public Image IngridientIcon;
        public Image IngrideientIcon2;
        /// the title container object
        public TextMeshProUGUI Title;
        /// the short description container object
        public TextMeshProUGUI Description;
        /// the quantity container object
        public TextMeshProUGUI IngridientQuantity;
        public TextMeshProUGUI IngridientQuantity2;

        public void ShowRecipeDetails(Recipe recipe)
        {
            RecipeIcon.sprite = recipe.Item.Icon;
            IngridientIcon.sprite = recipe.Ingredients[0].Item.Icon;
            IngrideientIcon2.sprite = recipe.Ingredients[1].Item.Icon;
            Title.text = recipe.Item.ItemName;
            Description.text = recipe.Item.Description;
            IngridientQuantity.text = recipe.Ingredients[0].Quantity.ToString();
            IngridientQuantity2.text = recipe.Ingredients[1].Quantity.ToString();

            craftDetails.SetActive(true);
        }


    }
}
