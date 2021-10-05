using UnityEngine;
using UnityEngine.UI;

namespace Schwer.ItemSystem {
    public class RecipeSlot : MonoBehaviour {
        [Header("Components")]
        [SerializeField] private Image sprite = default;
        [SerializeField] private Text text = default;
        [SerializeField] private Button button = default;

        public Recipe recipe { get; private set; }

        public void Initialise(Recipe recipe, bool discovered, RecipeMenu menu) {
            this.recipe = recipe;
            if (recipe != null) {
                button.onClick.AddListener(() => menu.OnRecipeClick(recipe));

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

            button.onClick.RemoveAllListeners();
        }
    }
}
