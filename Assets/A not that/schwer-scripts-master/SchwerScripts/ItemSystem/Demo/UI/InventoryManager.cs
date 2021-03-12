using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Schwer.ItemSystem.Demo {
    public class InventoryManager : MonoBehaviour {
        [SerializeField] private InventorySO _inventory = default;
        private Inventory inventory => _inventory.value;

        [Header("Item Display components")]
        [SerializeField] private Text nameDisplay = default;
        [SerializeField] private Text descriptionDisplay = default;
        [SerializeField] private GameObject useButton;

        private List<ItemSlot> itemSlots = new List<ItemSlot>();

        private void OnEnable() {
            inventory.OnContentsChanged += UpdateSlots;

            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected == null || !selected.transform.IsChildOf(this.transform)) {
                EventSystem.current.SetSelectedGameObject(itemSlots[0].gameObject);
            }

            Initialise();
        }

        private void OnDisable() => inventory.OnContentsChanged -= UpdateSlots;

        private void Awake() {
            GetComponentsInChildren<ItemSlot>(itemSlots);

            foreach (var slot in itemSlots) {
                slot.manager = this;
            }
        }

        private void Initialise() {
            UpdateDisplay(null);
            if (inventory != null) {
                UpdateSlots();
            }
        }

        public void UpdateSlots() => UpdateSlots(null, 0);
        private void UpdateSlots(Item item, int count, bool buttonActive)
        {
            //
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (i < inventory.Count)
                {
                    var entry = inventory.ElementAt(i);
                    itemSlots[i].SetItem(entry.Key, entry.Value);
                }
                else
                {
                    itemSlots[i].Clear();
                }
                if (buttonActive)
                {
                    useButton.SetActive(true);
                }
                else
                {
                    useButton.SetActive(false);
                }
            }

            var current = EventSystem.current.currentSelectedGameObject?.GetComponent<ItemSlot>();
            if (current != null)
            {
                UpdateDisplay(current.item);
            }
        }

        public void UpdateDisplay(bool isButtonusable, Item item)
        {
            if (item != null)
            {
                nameDisplay.text = item.name;
                descriptionDisplay.text = item.description;
                useButton.SetActive(isButtonUsable);
            }
            else
            {
                nameDisplay.text = "";
                descriptionDisplay.text = "";
            }
        }

        public void UseButtonPressed()
        {
            if (currentItem)
            {
                currentItem.Use();
                //clear all of the inventory slot
                //refill all of the slots to update them
                if (currentItem.numberHeld == 0)
                {
                    UpdateDisplay("", false);
                    //are those arguments correct
                }
            }
        }
    }
}
