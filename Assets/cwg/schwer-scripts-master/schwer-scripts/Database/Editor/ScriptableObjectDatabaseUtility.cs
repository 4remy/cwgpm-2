using UnityEditor;
using UnityEngine;

namespace SchwerEditor.Database {
    using Schwer.Database;

    // [CustomEditor(typeof(TYPE))]
    public class ScriptableObjectDatabaseInspector<TDatabase, TElement> : Editor
        where TDatabase : ScriptableObjectDatabase<TElement>
        where TElement : ScriptableObject {

        public override void OnInspectorGUI() {
            if (GUILayout.Button($"Regenerate {typeof(TDatabase).Name}")) {
                ScriptableObjectDatabaseUtility<TDatabase, TElement>.GenerateDatabase();
            }
            GUILayout.Space(5);

            var listProperty = new SerializedObject((TDatabase)target).GetIterator();
            // `Base`(?) to `Script`
            listProperty.NextVisible(true);
            // to list
            listProperty.NextVisible(true);
            if (listProperty.propertyType == SerializedPropertyType.Generic) {
                // Use Copy() to avoid unwanted iterating.
                var listCount = listProperty.arraySize;
                var listName = listProperty.displayName;
                GUILayout.Label($"{listName} ({listCount})");

                foreach (SerializedProperty elementProperty in listProperty) {
                    if (elementProperty.propertyType == SerializedPropertyType.ObjectReference) {
                        using (new EditorGUI.DisabledScope(true)) {
                            EditorGUILayout.PropertyField(elementProperty, GUIContent.none);
                        }
                    }
                }
            }
            else {
                EditorGUILayout.HelpBox($"Expected first property in `{typeof(TDatabase).Name} to be of type {SerializedPropertyType.Generic.ToString()}` but got {listProperty.propertyType.ToString()} instead.", MessageType.Error);
            }
        }
    }

    public static class ScriptableObjectDatabaseUtility<TDatabase, TElement>
        where TDatabase : ScriptableObjectDatabase<TElement>
        where TElement : ScriptableObject {

        // [MenuItem("Generate Database", false, -2), MenuItem("Assets/Create/Database", false, -11)]
        public static void GenerateDatabase() {
            var db = GetDatabase();
            if (db == null) return;

            db.Initialise(AssetsUtility.FindAllAssets<TElement>());
            EditorUtility.SetDirty(db);

            AssetsUtility.SaveRefreshAndFocus();
            Selection.activeObject = db;
        }

        private static TDatabase GetDatabase() {
            var databases = AssetsUtility.FindAllAssets<TDatabase>();

            TDatabase db = null;
            if (databases.Length < 1) {
                Debug.Log($"Creating a new {typeof(TDatabase).Name} since none exist.");
                db = ScriptableObjectUtility.CreateAsset<TDatabase>();
            }
            else if (databases.Length > 1) {
                Debug.LogError($"Multiple `{typeof(TDatabase).Name}` exist. Please delete the extra(s) and try again.");
            }
            else {
                db = databases[0];
            }

            return db;
        }
    }
}
