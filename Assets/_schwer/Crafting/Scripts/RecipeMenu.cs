﻿using System.Collections.Generic;
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

        private void OnEnable() => SelectFirstSlot();

        private void OnDisable() {
            if (manager != null) {
                manager.Enable(true);
                manager = null;
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

        public void Open(CraftingManager manager) {
            this.manager = manager;
            if (manager != null) {
                manager.Enable(false);
            }

            this.gameObject.SetActive(true);
        }

        // Called when a recipe slot is clicked on.
        public void OnRecipeClick(Recipe recipe) {
            //! if recipe != null && discovered && manager != null
            if (recipe != null && manager != null) {
                if (recipe.IsSubsetOf(inventory, manager.ingredients)) {
                    manager.ClearIngredients();
                    foreach (var item in recipe.input) {
                        manager.ingredients[item.Key] += item.Value;
                        inventory[item.Key] -= item.Value;
                    }
                    this.gameObject.SetActive(false);
                }
                else {
                    Debug.Log("Insufficient ingredients!");
                }
            }
        }
    }
}