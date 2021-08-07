using Schwer.Database;
using UnityEngine;

namespace Schwer.ItemSystem {
    [CreateAssetMenu(menuName = "Item System/Crafting Recipe")]
    public class Recipe : ScriptableObject, IID {
        [SerializeField] private int _id = default;
        public int id => _id;
        [Space]
        [SerializeField] private Item _output = default;
        public Item output => _output;
        [SerializeField] [Min(1)] private int _outputAmount = 1;
        public int outputAmount => _outputAmount;
        [Space]
        [SerializeField] private Inventory _input = new Inventory();
        public Inventory input => _input;

        public bool Matches(Inventory ingredients) {
            // Exit early if the number of different items do not match
            if (input.Count != ingredients.Count) return false;

            // Compare amounts for each item
            foreach (var item in ingredients.Keys) {
                if (input[item] != ingredients[item]) return false;
            }

            return true;
        }

        public bool IsSubsetOf(Inventory inventory) {
            foreach (var item in input.Keys) {
                if (inventory[item] < input[item]) return false;
            }

            return true;
        }

        // Use this for checking inventory and ingredients
        // to avoid having to create a separate inventory
        public bool IsSubsetOf(Inventory inventory1, Inventory inventory2) {
            foreach (var item in input.Keys) {
                if ((inventory1[item] + inventory2[item]) < input[item]) return false;
            }

            return true;
        }

#if UNITY_EDITOR
        // Needed in order to allow changes to the Inventory in the editor to be saved.

        private void OnEnable() => input.OnContentsChanged += MarkDirtyIfChanged;
        private void OnDisable() => input.OnContentsChanged -= MarkDirtyIfChanged;

        private void MarkDirtyIfChanged(Item item, int count) => UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
