using UnityEngine;

namespace Schwer.ItemSystem {
    [CreateAssetMenu(menuName = "Item System/Crafting Recipe")]
    public class Recipe : ScriptableObject {
        public Item output = default;
        [Min(1)]public int outputAmount = default;
        [Space]
        public Inventory input = new Inventory();
    }
}
