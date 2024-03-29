﻿using System;
using System.Collections.Generic;
using System.Linq;
using Schwer.ItemSystem.Demo;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Schwer.ItemSystem {
    public class CraftingManager : MonoBehaviour, IItemSlotManager {
        private static event Action<RecipeList> OnCraftingMenuRequested;
        public static void RequestCraftingMenu(RecipeList recipeList) => OnCraftingMenuRequested?.Invoke(recipeList);

        [SerializeField] private RecipeList recipeList = default;
        [SerializeField] private string openSFX = default;

        [Header("Save Data")]
        [SerializeField] private IntListSO discoveredRecipes = default;
        [SerializeField] private InventorySO _inventory = default;
        private Inventory inventory => _inventory.value;

        [Header("Signals")]
        [SerializeField] private Signal startInteraction;
        [SerializeField] private Signal finishInteraction;

        public Signal UIbusy;
        public Signal UIfree;

        [Header("Components")]
        [SerializeField] private Text nameDisplay = default;
        [SerializeField] private Button addButton = default;
        [SerializeField] private Button craftButton = default;
        [SerializeField] private Button clearButton = default;
        [SerializeField] private RecipeMenu recipeMenu = default;

        [Header("Containers")]
        [SerializeField] private GameObject inventorySlotsHolder = default;
        [SerializeField] private GameObject ingredientSlotsHolder = default;

        private Canvas canvas;
        private CanvasGroup canvasGroup;
        private List<ItemSlot> inventorySlots = new List<ItemSlot>();

        public Inventory ingredients { get; private set; } = new Inventory();
        private List<ItemSlot> ingredientSlots = new List<ItemSlot>();

        private Item selectedItem;

        public delegate void CookedDelegate();
        public event CookedDelegate cookedEvent;

        private int disabledFrame;  // Keyboard/gamepad input to close the crafting menu would also be received by `OpenCraft`, opening the menu again in the same frame 

        private void OnEnable() {
            inventory.OnContentsChanged += UpdateInventorySlots;
            ingredients.OnContentsChanged += UpdateIngredientSlots;

            UIbusy.Raise();

            Initialise();

            AudioManager.Instance.Play(openSFX);
            startInteraction.Raise();
        }

        private void OnDisable() {
            // Move any ingredients back into player inventory when closing crafting menu
            ClearIngredients();

            inventory.OnContentsChanged -= UpdateInventorySlots;
            ingredients.OnContentsChanged -= UpdateIngredientSlots;

            finishInteraction.Raise();
            UIfree.Raise();

            disabledFrame = Time.frameCount;
        }

        private void Awake() {
            canvas = GetComponent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();

            inventorySlotsHolder.GetComponentsInChildren<ItemSlot>(inventorySlots);
            ingredientSlotsHolder.GetComponentsInChildren<ItemSlot>(ingredientSlots);

            foreach (var slot in inventorySlots) {
                slot.manager = this;
            }

            OnCraftingMenuRequested += Open;
            gameObject.SetActive(false);
        }

        private void OnDestroy() => OnCraftingMenuRequested -= Open;

        private void Open(RecipeList recipeList) {
            if (disabledFrame == Time.frameCount) return;

            SetData(recipeList, discoveredRecipes, _inventory);
            gameObject.SetActive(true);
        }

        private void Initialise() {
            UpdateInventorySlots(null, 0);
            UpdateIngredientSlots(null, 0);

            ItemSlot.SelectFirstSlotIfNoneSelected(inventorySlots[0].gameObject);
        }

        // Parameters unused but necessary for Inventory.OnContentsChanged event
        private void UpdateInventorySlots(Item item, int count) => UpdateItemSlots(inventorySlots, inventory);
        // Parameters unused but necessary for Inventory.OnContentsChanged event
        private void UpdateIngredientSlots(Item item, int count) {
            UpdateItemSlots(ingredientSlots, ingredients);

            craftButton.interactable = (ingredients.Count > 0);
            clearButton.interactable = (ingredients.Count > 0);
        }

        private void UpdateItemSlots(List<ItemSlot> itemSlots, Inventory inventory) {
            for (int i = 0; i < itemSlots.Count; i++) {
                if (i < inventory.Count) {
                    var entry = inventory.ElementAt(i);
                    itemSlots[i].SetItem(entry.Key, entry.Value);
                }
                else {
                    itemSlots[i].Clear();
                }
            }
        }

        // change to on mouse over button
        public void OnItemSelected(Item item) {
            selectedItem = item;

            if (item != null) {
                nameDisplay.text = item.name;
                addButton.interactable = true;
            }
            else {
                nameDisplay.text = "";
                addButton.interactable = false;
            }
        }

        //change to on click?
        // Called by the add button's OnClick UnityEvent
        public void AddIngredient() {
            if (selectedItem != null) {
                ingredients[selectedItem]++;
                inventory[selectedItem]--;

                // Clear the current selection if item is fully depleted
                if (inventory[selectedItem] <= 0) {
                    OnItemSelected(null);
                }
            }
        }

        // Called by the clear button's OnClick UnityEvent
        public void ClearIngredients() {
            foreach (var item in ingredients) {
                inventory[item.Key] += item.Value;
            }
            ingredients.Clear();
        }

        // Called by the craft button's OnClick UnityEvent
        public void TryCraft() {
            var recipes = recipeList.recipes;
            for (int i = 0; i < recipes.Count; i++) {
                if (recipes[i].Matches(ingredients)) {
                    ingredients.Clear();
                    inventory[recipes[i].output] += recipes[i].outputAmount;

                    discoveredRecipes.Add(recipes[i].id);

                    AudioManager.Instance.Play("Achieve");
                    Debug.Log($"Crafted {recipes[i].outputAmount}x {recipes[i].output.name}!");
                    //this is where you would call a function displaying a nice achievement thing
                    if (cookedEvent != null)
                    {
                        cookedEvent();
                    }
                    
                    return;
                }
            }

            AudioManager.Instance.Play("Oink");
            Debug.Log($"The ingredients didn't yield anything...");
            //maybe add in like 'failure event ' for another notification , put the event on separate script
        }

        // Called by the recipe button's OnClick UnityEvent
        public void OpenRecipeMenu() {
            recipeMenu.SetData(recipeList, discoveredRecipes, _inventory);
            recipeMenu.Open(this);
        }

        public void Enable(bool value) {
            // Disable canvas rather than game object
            // so that ingredients pouch is persistent
            canvas.enabled = value;
            canvasGroup.interactable = value;
        }

        public void SetData(RecipeList recipeList, IntListSO discoveredRecipes, InventorySO inventorySO) {
            this.recipeList = recipeList;
            this.discoveredRecipes = discoveredRecipes;

            if (this._inventory != inventorySO) {
                if (this.isActiveAndEnabled) {
                    inventory.OnContentsChanged -= UpdateInventorySlots;
                    this._inventory = inventorySO;
                    inventory.OnContentsChanged += UpdateInventorySlots;
                    UpdateInventorySlots(null, 0);
                }
                else {
                    this._inventory = inventorySO;
                }
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
