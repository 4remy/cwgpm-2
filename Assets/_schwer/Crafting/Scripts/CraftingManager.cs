using System.Collections.Generic;
using System.Linq;
using Schwer.ItemSystem.Demo;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Schwer.ItemSystem
{
    public class CraftingManager : MonoBehaviour, IItemSlotManager {
        [SerializeField] private InventorySO _inventory = default;
        private Inventory inventory => _inventory.value;

        [Header("Components")]
        [SerializeField] private Text nameDisplay = default;
        [SerializeField] private GameObject addButton = default;
        [SerializeField] private GameObject clearButton = default;

        [Header("Containers")]
        [SerializeField] private GameObject inventorySlotsHolder = default;
        [SerializeField] private GameObject ingredientSlotsHolder = default;

        private List<ItemSlot> inventorySlots = new List<ItemSlot>();

        private Inventory ingredients = new Inventory();
        private List<ItemSlot> ingredientSlots = new List<ItemSlot>();

        private Item selectedItem;

        private void OnEnable() {
            inventory.OnContentsChanged += UpdateInventorySlots;
            ingredients.OnContentsChanged += UpdateIngredientSlots;

            Initialise();
        }

        private void OnDisable() {
            inventory.OnContentsChanged -= UpdateInventorySlots;
            ingredients.OnContentsChanged -= UpdateIngredientSlots;
        }

        private void Awake() {
            inventorySlotsHolder.GetComponentsInChildren<ItemSlot>(inventorySlots);
            ingredientSlotsHolder.GetComponentsInChildren<ItemSlot>(ingredientSlots);

            foreach (var slot in inventorySlots) {
                slot.manager = this;
            }
        }

        private void Initialise() {
            UpdateInventorySlots(null, 0);
            UpdateIngredientSlots(null, 0);

            SelectFirstSlot();
        }

        private void SelectFirstSlot() {
            // Need to set selection to null then select first slot to
            // ensure the item slot button properly highlights
            EventSystem.current.SetSelectedGameObject(null);
            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected == null || !selected.transform.IsChildOf(inventorySlotsHolder.transform)) {
                EventSystem.current.SetSelectedGameObject(inventorySlots[0].gameObject);
            }
        }

        // Parameters unused but necessary for Inventory.OnContentsChanged event
        private void UpdateInventorySlots(Item item, int count) => UpdateItemSlots(inventorySlots, inventory);
        // Parameters unused but necessary for Inventory.OnContentsChanged event
        private void UpdateIngredientSlots(Item item, int count) => UpdateItemSlots(ingredientSlots, ingredients);

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

        public void OnItemSelected(Item item) {
            selectedItem = item;

            if (item != null) {
                nameDisplay.text = item.name;
                addButton.SetActive(true);
            }
            else {
                nameDisplay.text = "";
                addButton.SetActive(false);
            }
        }

        // Called by the 'add' button's OnClick UnityEvent
        public void AddIngredient() {
            if (selectedItem != null) {
                ingredients[selectedItem]++;
                inventory[selectedItem]--;

                // Clear the current selection if item is fully depleted
                if (inventory[selectedItem] <= 0) {
                    OnItemSelected(null);
                    SelectFirstSlot();
                }
            }
        }

        public void ClearIngredients() {
            foreach (var item in ingredients) {
                inventory[item.Key] += item.Value;
            }
            ingredients.Clear();
        }
    }
}
