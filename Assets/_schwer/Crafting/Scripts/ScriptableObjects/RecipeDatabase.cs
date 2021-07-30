using System.Collections.ObjectModel;
using UnityEngine;

namespace Schwer.ItemSystem {
    using Schwer.Database;

    public class RecipeDatabase : ScriptableObjectDatabase<Recipe> {
        // Generated via RecipeDatabaseUtility
        [SerializeField] private Recipe[] recipes;

        public override void Initialise(Recipe[] recipes) {
            this.recipes = recipes;
        }

        public ReadOnlyCollection<Recipe> GetRecipes() => System.Array.AsReadOnly(recipes);
    }
}
