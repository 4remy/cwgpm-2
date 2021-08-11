﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSaveManager))]
public class GameSaveManagerInspector : Editor {
    public override void OnInspectorGUI() {
        var gsm = (GameSaveManager)target;

        EditorGUI.BeginDisabledGroup(!Application.isPlaying);
        if (GUILayout.Button("Save")) {
            gsm.SaveScriptables();
        }
        else if (GUILayout.Button("Load")) {
            gsm.LoadScriptables();
        }
        EditorGUI.EndDisabledGroup();

        GUILayout.Space(5);
        base.OnInspectorGUI();
    }
}
