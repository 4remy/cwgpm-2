using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FloatValue : ScriptableObject
{
    /// <summary>
    /// Default value for new/reset saves. Do not use this anywhere else.
    /// </summary>
    [Tooltip("Default value for new/reset saves")] public float initialValue;
    [Tooltip("Value to use in game")] public float RuntimeValue;
}
