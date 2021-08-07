using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Schwer.ItemSystem {
    using Schwer.Database;

    public class ItemDatabase : ScriptableObjectDatabase<Item> {
        // Generated via ItemDatabaseUtility
        [SerializeField] private Item[] items;

        public override void Initialise(Item[] items) {
            this.items = FilterByID(items);
        }

        public Item GetItem(int itemID) => GetFromID(itemID, items);
    }
}
