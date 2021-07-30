using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Schwer.ItemSystem
{
    public class RecipeSlot : MonoBehaviour, ISelectHandler {
        [Header("Components")]
        [SerializeField] private Image sprite = default;
        [SerializeField] private Button _button = default;
        public Button button => _button;

        public IRecipeSlotManager manager { get; set; }
        public Recipe recipe { get; private set; }

        public void Initialise(Recipe recipe, bool discovered) {
            this.recipe = recipe;
            if (recipe != null) {
                sprite.sprite = recipe.output.sprite;
                sprite.enabled = true;
                sprite.color = discovered ? Color.white : Color.black;
            }
            else {
                sprite.enabled = false;
                sprite.sprite = null;
            }
        }

        public void OnSelect(BaseEventData eventData) => manager?.OnRecipeSelected(recipe);
    }

    public interface IRecipeSlotManager {
        void OnRecipeSelected(Recipe recipe);
    }
}
