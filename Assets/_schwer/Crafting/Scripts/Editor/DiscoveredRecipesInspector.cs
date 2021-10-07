using UnityEditor;
using UnityEngine;

namespace SchwerEditor.ItemSystem {
    using Schwer.ItemSystem;

    // Temporary script for in-editor testing only
    [CustomEditor(typeof(IntListSO))]
    public class DiscoveredRecipesInspector : Editor {
        public override void OnInspectorGUI() {
            var intListSO = (IntListSO)target;
            if (intListSO.name == "DiscoveredRecipes") {
                if (GUILayout.Button("Overwrite with all recipe IDs")) {
                    PopulateRecipeIDs(intListSO);
                }
                GUILayout.Space(5);
            }

            base.OnInspectorGUI();
        }

        private void PopulateRecipeIDs(IntListSO target) {
            var recipes = AssetsUtility.FindAllAssets<Recipe>();

            target.ints = new System.Collections.Generic.List<int>();

            for (int i = 0; i < recipes.Length; i++) {
                target.ints.Add(recipes[i].id);
            }
        }
    }
}
