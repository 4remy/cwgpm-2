using UnityEngine;
using UnityEngine.EventSystems;

namespace Schwer.ItemSystem {
    public class RecipeMenu : MonoBehaviour, IRecipeSlotManager {
        [SerializeField] private RecipeDatabase recipeDatabase = default;
        [SerializeField] private InventorySO _inventory = default;
        private Inventory inventory => _inventory.value;

        [Header("Prefabs")]
        [SerializeField] private RecipeSlot recipeSlot = default;

        private CraftingManager manager;

        private RecipeSlot[] recipeSlots;
        private Recipe selectedRecipe;

        private void Awake() => GenerateSlots();

        private void GenerateSlots() {
            var recipes = recipeDatabase.GetRecipes();
            if (recipeSlots == null) {
                recipeSlots = new RecipeSlot[recipes.Count - 1];

                for (int i = 0; i < recipeSlots.Length; i++) {
                    //! Should enforce layout in Instantiate to avoid unnecessary transform changes?
                    var slot = Instantiate(recipeSlot.gameObject, this.transform);
                    recipeSlots[i] = slot.GetComponent<RecipeSlot>();
                    recipeSlots[i].button.onClick.AddListener(OnRecipeClick);
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

        private void SelectFirstSlot() {
            // Need to set selection to null then select first slot to
            // ensure the item slot button properly highlights
            EventSystem.current.SetSelectedGameObject(null);
            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected == null || !selected.transform.IsChildOf(this.transform)) {
                EventSystem.current.SetSelectedGameObject(recipeSlots[0].gameObject);
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

            SelectFirstSlot();
        }

        public void Close() {
            this.GetComponent<Canvas>().enabled = false;

            if (manager != null) {
                manager.GetComponent<Canvas>().enabled = true;
                manager = null;
            }
        }

        public void OnRecipeSelected(Recipe recipe) {
            // if recipe is discovered
            if (true) selectedRecipe = recipe;
        }

        // Called when a recipe slot is clicked on.
        public void OnRecipeClick() {
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
