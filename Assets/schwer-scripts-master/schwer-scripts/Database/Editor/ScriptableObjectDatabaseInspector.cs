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

            var arrayProperty = new SerializedObject((TDatabase)target).GetIterator();
            // `Base`(?) to `Script`
            arrayProperty.NextVisible(true);
            // to array — relies on the first serializable property being an array (or list)
            arrayProperty.NextVisible(true);
            if (arrayProperty.isArray) {
                var listCount = arrayProperty.arraySize;
                var listName = arrayProperty.displayName;
                GUILayout.Label($"{listName} ({listCount})");

                foreach (SerializedProperty elementProperty in arrayProperty) {
                    if (elementProperty.propertyType == SerializedPropertyType.ObjectReference) {
                        using (new EditorGUI.DisabledScope(true)) {
                            EditorGUILayout.PropertyField(elementProperty, GUIContent.none);
                        }
                    }
                }
            }
            else {
                EditorGUILayout.HelpBox($"Expected first property in `{typeof(TDatabase).Name}` to be an array", MessageType.Error);
            }
        }
    }
}
