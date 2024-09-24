using MoreMountains.InventoryEngine;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace SurviveCoding.External.InventoryEngine
{
    public class CraftButtonTest : MonoBehaviour
    {
        [SerializeField]
        private Craft craft;
        [SerializeField]
        private GameObject _craftingButtonPrefab;

        [SerializeField]
        private CraftDetails craftDetails;

        [SerializeField]
        private Button externalButton; // �ܺ� ��ư�� �߰��մϴ�.

        private Recipe currentRecipe; // ���� ���õ� �����Ǹ� ������ ����

        private void Start()
        {
            foreach (Recipe recipe in craft.Recipes)
            {
                var craftingButton = Instantiate(_craftingButtonPrefab, transform);
                craftingButton.transform.GetChild(0).GetComponent<Image>().sprite = recipe.Item.Icon;
                Button buttonComponent = craftingButton.GetComponentInChildren<Button>();
                if (buttonComponent != null)
                {
                    buttonComponent.onClick.AddListener(() => ShowRecipeDetails(recipe));
                }
            }

            if (externalButton != null)
            {
                externalButton.onClick.AddListener(CraftCurrentRecipe);
            }
        }

        // ������ ���� ������ �����ְ� ���� �����Ǹ� �����ϴ� �Լ�
        private void ShowRecipeDetails(Recipe recipe)
        {
            craftDetails.ShowRecipeDetails(recipe);
            currentRecipe = recipe;
        }

        // �ܺ� ��ư�� ȣ���� ���� �Լ�
        public void CraftCurrentRecipe()
        {
            if (currentRecipe != null)
            {
                currentRecipe.Craft();
                Debug.Log("Crafted: " + currentRecipe.Item.ItemName);
            }
            else
            {
                Debug.Log("No recipe selected.");
            }
        }
    }
}
