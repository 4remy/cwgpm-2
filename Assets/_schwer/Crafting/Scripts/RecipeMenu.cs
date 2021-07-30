using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Schwer.ItemSystem {
    public class RecipeMenu : MonoBehaviour {
        [SerializeField] private RecipeDatabase recipeDatabase = default;
        [SerializeField] private InventorySO _inventory = default;
        private Inventory inventory => _inventory.value;

        private CraftingManager manager;

        private List<RecipeSlot> recipeSlots = new List<RecipeSlot>();

        private void Awake() {
            GetComponentsInChildren<RecipeSlot>(recipeSlots);

            var recipes = recipeDatabase.GetRecipes();
            for (int i = 0; i < recipeSlots.Count; i++) {
                var recipe = (i < recipes.Count) ? recipes[i] : null;
                recipeSlots[i].Initialise(recipe, true); //!
                recipeSlots[i].button.onClick.AddListener(() => OnRecipeClick(recipe));
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

        // Called when a recipe slot is clicked on.
        public void OnRecipeClick(Recipe recipe) {
            //! if recipe != null && discovered && manager != null
            if (recipe != null && manager != null) {
                if (recipe.IsSubsetOf(inventory, manager.ingredients)) {
                    foreach (var item in recipe.input) {
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
