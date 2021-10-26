using System;
using System.Linq;
using Schwer.ItemSystem.Demo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Schwer.ItemSystem {
    public class InventoryStorageManager : MonoBehaviour, IItemSlotManager {
        private static event Action<Inventory, Inventory> OnInventoryStorageMenuRequested;
        public static void Request(Inventory player, Inventory storage) => OnInventoryStorageMenuRequested?.Invoke(player, storage);

        private Inventory inventory;
        private Inventory storage;

        [Header("Signals")]
        [SerializeField] private Signal startInteraction;
        [SerializeField] private Signal finishInteraction;

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

        private int disabledFrame;
        // Keyboard/gamepad input to close the crafting menu would also be
        // received by `StorageBin`, opening the menu again in the same frame 

        private void OnEnable() {
            // Expected to be active and enabled *after* data has been set
            UpdateInventorySlots();
            UpdateStorageSlots();
            //! Select first item slot
            UpdateButtons();
            
            startInteraction.Raise();
        }

        private void OnDisable() {
            // Unsubscribe from events
            if (inventory != null) inventory.OnContentsChanged -= UpdateInventorySlots;
            if (storage != null) storage.OnContentsChanged -= UpdateStorageSlots;
            // Remove references
            inventory = null;
            storage = null;

            finishInteraction.Raise();
            disabledFrame = Time.frameCount;
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

            OnInventoryStorageMenuRequested += Open;
            gameObject.SetActive(false);
        }

        private void OnDestroy() => OnInventoryStorageMenuRequested -= Open;

        private void Open(Inventory player, Inventory storage) {
            if (disabledFrame == Time.frameCount) return;

            this.inventory = SetData(this.inventory, player, UpdateInventorySlots);
            this.storage = SetData(this.storage, storage, UpdateStorageSlots);
            gameObject.SetActive(true); // Refer to OnEnable()
        }

        private Inventory SetData(Inventory previous, Inventory incoming, Action<Item, int> action) {
            if (previous != incoming) {
                if (previous != null) previous.OnContentsChanged -= action;
                if (incoming != null) incoming.OnContentsChanged += action;
                return incoming;
            }
            else {
                return previous;
            }
        }

        private void UpdateInventorySlots() => UpdateInventorySlots(null, 0);
        private void UpdateInventorySlots(Item item, int count) => UpdateItemSlots(inventorySlots, inventory);
        private void UpdateStorageSlots() => UpdateStorageSlots(null, 0);
        private void UpdateStorageSlots(Item item, int count) => UpdateItemSlots(storageSlots, storage);

        private void UpdateItemSlots(ItemSlot[] itemSlots, Inventory inventory) {
            for (int i = 0; i < itemSlots.Length; i++) {
                if (inventory != null && i < inventory.Count) {
                    var entry = inventory.ElementAt(i);
                    itemSlots[i].SetItem(entry.Key, entry.Value);
                }
                else {
                    itemSlots[i].Clear();
                }
            }
        }

        private void UpdateButtons() {
            storeButton.interactable = inventory != null && selectedItem != null && inventory[selectedItem] > 0;
            takeButton.interactable = storage != null && selectedItem != null && storage[selectedItem] > 0;
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

        // Used by UI buttons
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

        //! Enabling/disabling through game object interaction
    }
}
