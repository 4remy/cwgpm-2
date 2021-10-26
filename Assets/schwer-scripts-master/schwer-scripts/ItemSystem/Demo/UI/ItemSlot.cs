using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Schwer.ItemSystem.Demo {
    public class ItemSlot : MonoBehaviour, ISelectHandler {
        [Header("Components")]
        [SerializeField] private Image sprite = default;
        [SerializeField] private Text count = default;

        public IItemSlotManager manager { get; set; }
        public Item item { get; private set; }

        public void SetItem(Item item, int itemCount) {
            this.item = item;
            if (item != null) {
                sprite.sprite = item.sprite;
                sprite.enabled = true;
                count.text = "x" + itemCount;
            }
            else {
                Clear();
            }
        }

        public void Clear() {
            item = null;
            sprite.enabled = false;
            sprite.sprite = null;
            count.text = "";
        }

        public void OnSelect(BaseEventData eventData) => manager?.OnItemSelected(item);

        public static void SelectFirstSlotIfNoneSelected(GameObject firstSlot) {
            // Need to set selection to null then select first slot to
            // ensure the item slot button properly highlights
            EventSystem.current.SetSelectedGameObject(null);
            var selected = EventSystem.current.currentSelectedGameObject;
            if (selected == null || !selected.transform.IsChildOf(firstSlot.transform.parent)) {
                EventSystem.current.SetSelectedGameObject(firstSlot);
                // Above line doesn't reliably call OnSelect correctly, ∴ manually call
                firstSlot.GetComponent<ItemSlot>().OnSelect(null);  // Update selection text
                firstSlot.GetComponent<Button>().OnSelect(null);    // Button highlight
            }
        }
    }

    public interface IItemSlotManager {
        void OnItemSelected(Item item);
    }
}
