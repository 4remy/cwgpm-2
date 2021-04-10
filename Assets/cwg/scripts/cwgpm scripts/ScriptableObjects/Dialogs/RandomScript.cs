using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RandomScript : MonoBehaviour
{
    [HideInInspector] // HideInInspector makes sure the default inspector won't show these fields.
    public bool StartTemp;

    [HideInInspector]
    public InputField iField;

    [HideInInspector]
    public GameObject Template;

    // ... Update(), Awake(), etc
}

#if UNITY_EDITOR
[CustomEditor(typeof(RandomScript))]
public class RandomScript_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        RandomScript script = (RandomScript)target;

        // draw checkbox for the bool
        script.StartTemp = EditorGUILayout.Toggle("Start Temp", script.StartTemp);
        if (script.StartTemp) // if bool is true, show other fields
        {
            script.iField = EditorGUILayout.ObjectField("I Field", script.iField, typeof(InputField), true) as InputField;
            script.Template = EditorGUILayout.ObjectField("Template", script.Template, typeof(GameObject), true) as GameObject;
        }
    }
}
#endif
