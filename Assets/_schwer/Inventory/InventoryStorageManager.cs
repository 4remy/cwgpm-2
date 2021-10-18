using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Schwer.ItemSystem.Demo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Schwer.ItemSystem {
    public class InventoryStorageManager : MonoBehaviour, IItemSlotManager {
        [Header("Data")]
        [SerializeField] private InventorySO _inventory = default;
        private Inventory inventory => _inventory.value;
        [SerializeField] private InventorySO _storage = default;
        private Inventory storage => _storage.value;

        [Header("Components")]
        [SerializeField] private TMP_Text selectionDisplay = default;
        [SerializeField] private Button storeButton = default;
        [SerializeField] private Button takeButton = default;

        [Header("Containers")]
        [SerializeField] private GameObject inventorySlotsHolder = default;
        [SerializeField] private GameObject storageSlotsHolder = default;

        private ItemSlot[] inventorySlots;
        private ItemSlot[] storageSlots;

        private Item selectedItem;

        private void OnEnable() {
            inventory.OnContentsChanged += UpdateInventorySlots;
            storage.OnContentsChanged += UpdateStorageSlots;

            Initialise();
        }

        private void OnDisable() {
            inventory.OnContentsChanged -= UpdateInventorySlots;
            storage.OnContentsChanged -= UpdateStorageSlots;
        }

        private void Awake() {
            inventorySlots = inventorySlotsHolder.GetComponentsInChildren<ItemSlot>();
            storageSlots = storageSlotsHolder.GetComponentsInChildren<ItemSlot>();

            for (int i = 0; i < inventorySlots.Length; i++) {
                inventorySlots[i].manager = this;
            }

            for (int i = 0; i < storageSlots.Length; i++) {
                storageSlots[i].manager = this;
            }

            //! Open request event subscribe
            //! Disable self
        }

        private void Initialise() {
            UpdateInventorySlots(null, 0);
            UpdateStorageSlots(null, 0);

            //! Select first item slot
        }

        private void UpdateInventorySlots(Item item, int count) => UpdateItemSlots(inventorySlots, inventory);
        private void UpdateStorageSlots(Item item, int count) => UpdateItemSlots(storageSlots, storage);

        private void UpdateItemSlots(ItemSlot[] itemSlots, Inventory inventory) {
            for (int i = 0; i < itemSlots.Length; i++) {
                if (i < inventory.Count) {
                    var entry = inventory.ElementAt(i);
                    itemSlots[i].SetItem(entry.Key, entry.Value);
                }
                else {
                    itemSlots[i].Clear();
                }
            }
        }

        private void UpdateButtons() {
            storeButton.interactable = inventory[selectedItem] > 0;
            takeButton.interactable = storage[selectedItem] > 0;
        }

        public void OnItemSelected(Item item) {
            selectedItem = item;

            if (item != null) {
                selectionDisplay.text = item.name;
                UpdateButtons();
            }
            else {
                selectionDisplay.text = "";
                storeButton.interactable = false;
                takeButton.interactable = false;
            }
        }

        // Used by UI
        public void StoreItem() => Exchange(inventory, storage, selectedItem, Input.GetKey(KeyCode.LeftShift));
        public void TakeItem() => Exchange(storage, inventory, selectedItem, Input.GetKey(KeyCode.LeftShift));

        private void Exchange(Inventory from, Inventory to, Item item, bool storeAll) {
            if (storeAll) {
                var count = from[item];
                from.Remove(item);
                to[item] += count;
            }
            else {
                from[item]--;
                to[item]++;
            }

            UpdateButtons();
        }

        //! Runtime inventory data changing
        //! Enabling/disabling through game object interaction
    }
}
