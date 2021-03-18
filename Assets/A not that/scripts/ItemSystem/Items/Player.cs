using Schwer.ItemSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Schwer.ItemSystem.InventorySO _inventory = default;
    public Schwer.ItemSystem.Inventory inventory => _inventory.value;
}