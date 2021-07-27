using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Schwer.ItemSystem {
    using Schwer.Database;

    public class ItemDatabase : ScriptableObjectDatabase<Item> {
        // Generated via ItemDatabaseUtility
        [SerializeField] private Item[] items;

        public override void Initialise(Item[] items) {
            this.items = FilterItemsByID(items);
        }

        public Item GetItem(int itemID) {
            Item result = null;
            foreach (var item in items) {
                if (item == null) {
                    Debug.LogWarning("The database contains a null entry. Update the ItemDatabase to fix (refer to the readme for help).");
                    continue;
                }

                if (item.id == itemID) {
                    result = item;
                }
            }
            if (result == null) { Debug.LogWarning("Item with ID '" + itemID + "' was not found in the database."); }
            return result;
        }

        private Item[] FilterItemsByID(Item[] items) {
            var filteredItems = new List<Item>();
            var filteredIDs = new List<int>();

            for (int i = 0; i < items.Length; i++) {
                if (filteredIDs.Contains(items[i].id)) {
                    var sharedID = filteredItems[filteredIDs.IndexOf(items[i].id)].name;
                    Debug.LogWarning($"'{items[i].name}' was excluded from {this.name} because it shares its ID ({items[i].id}) with '{sharedID}'.");
                }
                else {
                    filteredItems.Add(items[i]);
                    filteredIDs.Add(items[i].id);
                }
            }
            return filteredItems.OrderBy(i => i.id).ToArray();
        }
    }
}
