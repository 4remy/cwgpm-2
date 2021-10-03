using System.Collections.Generic;
using UnityEngine;

namespace Schwer.ItemSystem {
    [CreateAssetMenu(menuName = "Item System/Crafting Recipe List")]
    public class RecipeList : ScriptableObject {
        [SerializeField] private List<Recipe> _recipes = new List<Recipe>();
        public List<Recipe> recipes => _recipes;
    }
}
