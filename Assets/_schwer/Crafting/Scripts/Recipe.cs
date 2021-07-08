using UnityEngine;

namespace Schwer.ItemSystem {
    [CreateAssetMenu(menuName = "Item System/Crafting Recipe")]
    public class Recipe : ScriptableObject {
        public Item output = default;
        [Min(1)]public int outputAmount = default;
        [Space]
        public Inventory input = new Inventory();

#if UNITY_EDITOR
        // Needed in order to allow changes to the Inventory in the editor to be saved.

        private void OnEnable() => input.OnContentsChanged += MarkDirtyIfChanged;
        private void OnDisable() => input.OnContentsChanged -= MarkDirtyIfChanged;

        private void MarkDirtyIfChanged(Item item, int count) => UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
