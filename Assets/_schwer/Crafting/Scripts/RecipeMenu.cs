using UnityEngine;
using UnityEngine.UI;

namespace Schwer.ItemSystem {
    public class RecipeMenu : MonoBehaviour, IRecipeSlotManager {
        [SerializeField] private RecipeDatabase recipeDatabase = default;
        [SerializeField] private InventorySO _inventory = default;
        private Inventory inventory => _inventory.value;

        [Header("Prefabs")]
        [SerializeField] private RecipeSlot recipeSlot = default;

        [Header("Components")]
        [SerializeField] private Button chooseButton = default;

        private CraftingManager manager;

        private RecipeSlot[] recipeSlots;
        private Recipe selectedRecipe;

        private void GenerateSlots() {
            var recipes = recipeDatabase.GetRecipes();
            if (recipeSlots == null) {
                recipeSlots = new RecipeSlot[recipes.Count - 1];

                for (int i = 0; i < recipeSlots.Length; i++) {
                    //! Should enforce layout in Instantiate to avoid unnecessary transform changes?
                    var slot = Instantiate(recipeSlot.gameObject, this.transform);
                    recipeSlots[i] = slot.GetComponent<RecipeSlot>();
                }
            }

            LayOutSlots();
        }

        private void LayOutSlots() {
            //! width, height, spacing, rows, columns
            for (int i = 0; i < recipeSlots.Length; i++) {
                var transform = (RectTransform)recipeSlots[i].transform;
            }
        }

        private void OnEnable() => Open(null);
        private void OnDisable() => Close();

        public void Open(CraftingManager manager) {
            this.manager = manager;
            if (manager != null) {
                manager.GetComponent<Canvas>().enabled = false;
            }
            
            this.GetComponent<Canvas>().enabled = true;
        }

        public void Close() {
            this.GetComponent<Canvas>().enabled = false;

            if (manager != null) {
                manager.GetComponent<Canvas>().enabled = true;
                manager = null;
            }
        }

        public void OnRecipeSelected(Recipe recipe) => UpdateSelection(recipe);

        public void UpdateSelection(Recipe recipe) {
            selectedRecipe = recipe;

            if (recipe != null) {
                if (chooseButton != null) chooseButton.interactable = true;
            }
            else {
                if (chooseButton != null) chooseButton.interactable = false;
            }
        }

        // Called by the choose button's OnClick UnityEvent
        public void ChooseButtonPressed() {
            if (selectedRecipe != null && manager != null) {
                if (selectedRecipe.IsSubsetOf(inventory, manager.ingredients)) {
                    foreach (var item in selectedRecipe.input) {
                        manager.ingredients[item.Key] += item.Value;
                        inventory[item.Key] -= item.Value;
                    }
                    Close();
                }
                else {
                    Debug.Log("Insufficient ingredients!");
                }
            }
        }
    }
}
