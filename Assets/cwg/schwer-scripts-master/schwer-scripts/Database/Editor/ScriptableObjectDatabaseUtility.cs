﻿using UnityEditor;
using UnityEngine;

namespace SchwerEditor.Database {
    using Schwer.Database;

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