using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Int List")]
public class IntListSO : ScriptableObject {
    [SerializeField] private List<int> _ints = new List<int>();
    public List<int> ints { get => _ints; set => _ints = value; }

    public bool Contains(int item) => ints.Contains(item);

    public bool Add(int item) {
        if (!ints.Contains(item)) {
            ints.Add(item);
            return true;
        }

        return false;
    }
}
