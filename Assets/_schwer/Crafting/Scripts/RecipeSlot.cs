using UnityEngine;
using UnityEngine.UI;

namespace Schwer.ItemSystem {
    public class RecipeSlot : MonoBehaviour {
        [Header("Components")]
        [SerializeField] private Image sprite = default;
        [SerializeField] private Text text = default;
        [SerializeField] private Button _button = default;
        public Button button => _button;

        public Recipe recipe { get; private set; }

        public void Initialise(Recipe recipe, bool discovered) {
            this.recipe = recipe;
            if (recipe != null) {
                sprite.sprite = recipe.output.sprite;
                sprite.enabled = true;

                UpdateDiscoveryStatus(discovered);
            }
            else {
                EmptySlot();
            }
        }

        private void UpdateDiscoveryStatus(bool discovered) {
            if (discovered) {
                sprite.color = Color.white;
                text.text = recipe.output.name;
            }
            else {
                sprite.color = Color.black;
                text.text = "???";
            }
        }

        private void EmptySlot() {
            text.text = "";
            sprite.enabled = false;
            sprite.sprite = null;
        }
    }
}
