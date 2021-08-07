﻿using UnityEngine;
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
                text.text = recipe.output.name;
                sprite.sprite = recipe.output.sprite;
                sprite.enabled = true;
                sprite.color = discovered ? Color.white : Color.black;
            }
            else {
                text.text = "";
                sprite.enabled = false;
                sprite.sprite = null;
            }
        }
    }
}