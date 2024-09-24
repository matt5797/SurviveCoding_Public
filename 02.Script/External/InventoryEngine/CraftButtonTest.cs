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
        private Button externalButton; // 외부 버튼을 추가합니다.

        private Recipe currentRecipe; // 현재 선택된 레시피를 저장할 변수

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

        // 레시피 세부 사항을 보여주고 현재 레시피를 설정하는 함수
        private void ShowRecipeDetails(Recipe recipe)
        {
            craftDetails.ShowRecipeDetails(recipe);
            currentRecipe = recipe;
        }

        // 외부 버튼이 호출할 조합 함수
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
