using System;
using System.Linq;
using MoreMountains.InventoryEngine;
using UnityEngine;

namespace SurviveCoding.External.InventoryEngine
{
    using Inventory = MoreMountains.InventoryEngine.Inventory;

    [Serializable]
    public class Ingredient : ISerializationCallbackReceiver
    {
        [HideInInspector]
        public string Name;
        public InventoryItem Item;
        public int Quantity;
        public Sprite Icon;

        public void OnBeforeSerialize() { Name = ToString(); }
        public void OnAfterDeserialize() { }
        public override string ToString() { return (Quantity == 1 ? "" : Quantity + " ") + (Item == null ? "null" : Item.ItemName) + (Quantity > 1 ? "s" : ""); }
    }

    [Serializable]
    public class Recipe : Ingredient
    {
        public Ingredient[] Ingredients;
        public string IngredientsText => string.Join(", ", Ingredients.Select(ingredient => ingredient.Name));

        public bool ContainsIngredientsForRecipe(string playerID = "Player1")
        {
            Inventory inventory = Inventory.FindInventory(Item.TargetInventoryName, playerID);
            if (inventory == null) return false;

            return !Ingredients.Any(ingredient =>
            {
                Inventory ingredientInventory = Inventory.FindInventory(ingredient.Item.TargetInventoryName, playerID);
                if (ingredientInventory == null) return false;

                return ingredientInventory.InventoryContains(ingredient.Item.ItemID).Sum(index => ingredientInventory.Content[index].Quantity) < ingredient.Quantity;
            });
        }

        public void Craft(string playerID = "Player1", GameObject insufficientIngredientsPanel = null)
        {
            Inventory inventory = Inventory.FindInventory(Item.TargetInventoryName, playerID);
            if (inventory == null)
            {
                Debug.LogError($"Inventory '{Item.TargetInventoryName}' not found for player '{playerID}'");
                return;
            }

            if (!ContainsIngredientsForRecipe(playerID))
            {
                if (insufficientIngredientsPanel != null)
                {
                    insufficientIngredientsPanel.SetActive(true);
                }
                return;
            }

            foreach (var ingredient in Ingredients)
            {
                Inventory ingredientInventory = Inventory.FindInventory(ingredient.Item.TargetInventoryName, playerID);
                ingredientInventory.RemoveItemByID(ingredient.Item.ItemID, ingredient.Quantity);
            }

            if (inventory.AddItem(Item, Quantity))
            {
                MMInventoryEvent.Trigger(MMInventoryEventType.Pick, null, string.Empty, Item, Quantity, 0, playerID);
                return;
            }

            foreach (var ingredient in Ingredients)
            {
                Inventory ingredientInventory = Inventory.FindInventory(ingredient.Item.TargetInventoryName, playerID);
                ingredientInventory.AddItem(ingredient.Item, ingredient.Quantity);
            }
        }
    }

    [CreateAssetMenu(fileName = "New Craft", menuName = "SurviveCoding/Inventory/Craft", order = 52)]
    public class Craft : ScriptableObject
    {
        public Recipe[] Recipes;
        public GameObject InsufficientIngredientsPanel;

        public void TryCraft(int recipeIndex, string playerID = "Player1")
        {
            if (recipeIndex < 0 || recipeIndex >= Recipes.Length)
            {
                Debug.LogError("Invalid recipe index");
                return;
            }

            Recipes[recipeIndex].Craft(playerID, InsufficientIngredientsPanel);
        }
    }
}
