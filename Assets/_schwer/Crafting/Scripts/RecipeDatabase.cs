using System.Collections.Generic;
using UnityEngine;

namespace Schwer.ItemSystem {
    public class RecipeDatabase : ScriptableObject {
        // Generated via RecipeDatabaseUtility
        [field:SerializeField] private List<Recipe> recipes;
        public void Initialise(List<Recipe> recipes) {
            this.recipes = recipes;
        }
    }
}
