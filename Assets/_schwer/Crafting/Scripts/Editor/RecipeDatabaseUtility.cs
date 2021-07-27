using UnityEditor;

namespace SchwerEditor.ItemSystem {
    using SchwerEditor.Database;
    using Schwer.ItemSystem;

    [CustomEditor(typeof(RecipeDatabase))]
    public class RecipeDatabaseInspector : ScriptableObjectDatabaseInspector<RecipeDatabase, Recipe> {
        [MenuItem("Item System/Generate Recipe Database", false, -2), MenuItem("Assets/Create/Item System/Recipe Database", false, -11)]
        public static void GenerateDatabase() {
            ScriptableObjectDatabaseUtility<RecipeDatabase, Recipe>.GenerateDatabase();
        }
    }
}
