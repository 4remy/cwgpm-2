using UnityEngine;

[CreateAssetMenu]
//[System.Serializable]
public class BoolValue : ScriptableObject
{
    /// <summary>
    /// Default value for new/reset saves. Do not use this anywhere else.
    /// </summary>
    [Tooltip("Default value for new/reset saves")] public bool initialValue;
    [Tooltip("Value to use in game")] public bool RuntimeValue;
}
