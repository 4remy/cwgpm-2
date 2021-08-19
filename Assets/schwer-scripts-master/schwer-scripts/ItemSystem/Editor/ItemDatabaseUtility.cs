using UnityEditor;

namespace SchwerEditor.ItemSystem {
    using SchwerEditor.Database;
    using Schwer.ItemSystem;

    [CustomEditor(typeof(ItemDatabase))]
    public class ItemDatabaseInspector : ScriptableObjectDatabaseInspector<ItemDatabase, Item> {
        [MenuItem("Item System/Generate Item Database", false, -2), MenuItem("Assets/Create/Item System/Item Database", false, -11)]
        public static void GenerateDatabase() {
            ScriptableObjectDatabaseUtility<ItemDatabase, Item>.GenerateDatabase();
        }
    }
}
